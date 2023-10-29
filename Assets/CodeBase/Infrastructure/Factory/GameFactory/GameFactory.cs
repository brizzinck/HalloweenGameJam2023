using System.Collections.Generic;
using System.Threading.Tasks;
using CodeBase.Abilities;
using CodeBase.Hero;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.InteractiveObjects.Base;
using CodeBase.InteractiveObjects.Logic;
using CodeBase.NPC;
using CodeBase.Services.Audio;
using CodeBase.Services.GameLoopService;
using CodeBase.Services.GameScoreService;
using CodeBase.Services.Input;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.StaticData;
using CodeBase.Services.StaticData.Interactive;
using CodeBase.Services.StaticData.NPC;
using CodeBase.StaticData;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure.Factory.GameFactory
{
  public class GameFactory : IGameFactory
  {
    public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
    public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();
    private readonly IAssetProvider _assets;
    private readonly IStaticDataService _staticData;
    private readonly IInputService _inputService;
    private readonly IGameScoreService _gameScoreService;
    private readonly IDisplayInputService _displayInputService;
    private readonly IGameTimer _gameTimer;
    private GameObject _hero;

    public GameFactory(IAssetProvider assets, IStaticDataService staticData, IInputService inputService,
      IGameScoreService gameScoreService, IDisplayInputService displayInputService, IGameTimer gameTimer)
    {
      _assets = assets;
      _staticData = staticData;
      _inputService = inputService;
      _gameScoreService = gameScoreService;
      _displayInputService = displayInputService;
      _gameTimer = gameTimer;
    }

    public void Cleanup()
    {
      ProgressReaders.Clear();
      ProgressWriters.Clear();
    }

    public Task WarmUp()
    {
      return null;
    }

    public async Task<GameObject> CreateHud()
    {
      GameObject hud = await InstantiateRegisteredAsync(AssetAddress.Hud);
      return hud;
    }

    public async Task<GameObject> CreateHero()
    {
      GameObject hero = await InstantiateRegisteredAsync(AssetAddress.Hero);
      hero.GetComponent<HeroMove>().Construct(_staticData, _inputService);
      hero.transform.position = _staticData.ForGameLevel(SceneManager.GetActiveScene().name).HeroSpawnPoint;
      _hero = hero;
      return hero;
    }

    public async Task CreateInteractiveSpawner(string spawnerId, Vector3 at, InteractiveID interactiveId)
    {
      GameObject prefab = await _assets.Load<GameObject>(AssetAddress.InteractiveSpawner);
      InteractiveSpawnMarker spawner = InstantiateRegistered(prefab, at)
        .GetComponent<InteractiveSpawnMarker>();
      spawner.Construct(this, interactiveId);
      spawner.InteractiveID = interactiveId;
      spawner.UniqueId = spawnerId;
    }

    public async Task CreateNPCSpawner(NPCId spawnerDataNpcId, string spawnerId, Vector3 at, GameObject hero)
    {
      GameObject prefab = await _assets.Load<GameObject>(AssetAddress.NPCSpawner);
      SpawnMarkerNPC spawner = InstantiateRegistered(prefab, at)
        .GetComponent<SpawnMarkerNPC>();
      spawner.Construct(this, hero, spawnerDataNpcId, spawnerId);
    }

    public async Task CreateAbility(AbilityID abilityID, IGameScoreService gameScoreService,
      IStaticDataService staticDataService)
    {
      AbilityStaticData ability = _staticData.ForAbilities(abilityID);
      GameObject prefab = await _assets.Load<GameObject>(ability.PrefabReference);
      GameObject instance = Object.Instantiate(prefab, _hero.transform.position, Quaternion.identity);
      instance.GetComponent<BaseAbility>().Construct(_gameScoreService, _staticData);
    }

    public async Task<GameObject> CreateInteractiveObject(InteractiveID id, Transform parent)
    {
      InteractiveStaticData interactiveStaticData = _staticData.ForInteractiveObjects(id);
      GameObject prefab = await _assets.Load<GameObject>(interactiveStaticData.PrefabReference);
      GameObject interactive = Object.Instantiate(prefab, parent.position, Quaternion.identity, parent);
      interactive.GetComponent<BaseInteractiveObject>()
        .Constructor(_inputService, _gameScoreService, _displayInputService);
      return interactive;
    }

    public async Task<GameLoop.GameLoop> CreateGameLoop()
    {
      GameObject prefab = await _assets.Load<GameObject>(AssetAddress.GameLoop);
      GameLoop.GameLoop gameLoop = InstantiateRegistered(prefab)
        .GetComponent<GameLoop.GameLoop>();
      gameLoop.Construct(_gameTimer);
      return gameLoop;
    }

    public async Task<AudioSource> CreateAudioSource(IAudioPlayer audioPlayer)
    {
      GameObject prefab = await _assets.Load<GameObject>(AssetAddress.AudioSource);
      AudioSource audioSource = InstantiateRegistered(prefab)
        .GetComponent<AudioSource>();
      audioPlayer.SetAudioSource(audioSource);
      audioPlayer.ChangeVolume(audioPlayer.Volume);
      audioPlayer.PlayCurrentScene();
      return audioSource;
    }

    public async Task<GameObject> CreateNPC(Transform parent, GameObject hero, NPCId npcId = NPCId.Random)
    {
      NPCStaticData npcStaticData;
      if (npcId == NPCId.Random)
        npcStaticData = _staticData.ForRandomNPC();
      else
        npcStaticData = _staticData.ForIdNPC(npcId);
      GameObject prefab = await _assets.Load<GameObject>(npcStaticData.PrefabReference);
      GameObject npc = Object.Instantiate(prefab, parent.position, Quaternion.identity, parent);
      npc.GetComponent<NPCAgroZone>().Construct(hero);
      npc.GetComponent<NPCScore>().Construct(_gameScoreService);
      if (npc.TryGetComponent(out NPCSkinSetter npcSkinSetter))
        npcSkinSetter.BaseConstruct(_staticData.ForDefaultSkinNPC());
      return npc;
    }

    private GameObject InstantiateRegistered(GameObject prefab, Vector3 at)
    {
      GameObject gameObject = Object.Instantiate(prefab, at, Quaternion.identity);
      RegisterProgressWatchers(gameObject);
      return gameObject;
    }

    private GameObject InstantiateRegistered(GameObject prefab)
    {
      GameObject gameObject = Object.Instantiate(prefab);
      RegisterProgressWatchers(gameObject);

      return gameObject;
    }

    private async Task<GameObject> InstantiateRegisteredAsync(string prefabPath)
    {
      GameObject gameObject = await _assets.Instantiate(address: prefabPath);
      RegisterProgressWatchers(gameObject);

      return gameObject;
    }

    private async Task<GameObject> InstantiateRegisteredAsync(string prefabPath, Vector3 at)
    {
      GameObject gameObject = await _assets.Instantiate(address: prefabPath, at: at);
      RegisterProgressWatchers(gameObject);
      return gameObject;
    }

    private void RegisterProgressWatchers(GameObject gameObject)
    {
      foreach (ISavedProgressReader progressReader in gameObject.GetComponentsInChildren<ISavedProgressReader>())
        Register(progressReader);
    }

    private void Register(ISavedProgressReader progressReader)
    {
      if (progressReader is ISavedProgress progressWriter)
        ProgressWriters.Add(progressWriter);
      ProgressReaders.Add(progressReader);
    }
  }
}