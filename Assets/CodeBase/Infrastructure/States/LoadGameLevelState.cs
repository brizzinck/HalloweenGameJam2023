using System.Threading.Tasks;
using CodeBase.CameraLogic;
using CodeBase.Infrastructure.Factory.GameFactory;
using CodeBase.Infrastructure.Scene;
using CodeBase.InteractiveObjects.Logic;
using CodeBase.Logic.Scene;
using CodeBase.NPC;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.StaticData;
using CodeBase.Services.StaticData.NPC;
using CodeBase.StaticData;
using CodeBase.UI.Services.Factory;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure.States
{
  public class LoadGameLevelState : IPayloadedState<string>
  {
    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly LoadingCurtain _loadingCurtain;
    private readonly IGameFactory _gameFactory;
    private readonly IPersistentProgressService _progressService;
    private readonly IStaticDataService _staticData;
    private readonly IUIFactory _uiFactory;

    public LoadGameLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain, IGameFactory gameFactory, IPersistentProgressService progressService, IStaticDataService staticDataService, IUIFactory uiFactory)
    {
      _stateMachine = gameStateMachine;
      _sceneLoader = sceneLoader;
      _loadingCurtain = loadingCurtain;
      _gameFactory = gameFactory;
      _progressService = progressService;
      _staticData = staticDataService;
      _uiFactory = uiFactory;
    }

    public void Enter(string sceneName)
    {
      _loadingCurtain.Show();
      _gameFactory.Cleanup();
      _gameFactory.WarmpUp();
      _sceneLoader.Load(sceneName, OnLoaded);
    }

    public void Exit() =>
      _loadingCurtain.Hide();

    private async void OnLoaded()
    {
      await InitUIRoot();
      await InitGameWorld();
      InformProgressReaders();
      _stateMachine.Enter<GameLoopState>();
    }

    private async Task InitUIRoot() => 
      await _uiFactory.CreateUIRoot();

    private void InformProgressReaders()
    {
      foreach (ISavedProgressReader progressReader in _gameFactory.ProgressReaders)
        progressReader.LoadProgress(_progressService.Progress);
    }

    private async Task InitGameWorld()
    {
      LevelStaticData levelData = LevelStaticData();
      await InitHud();
      GameObject hero = await InitHero();
      await InitInteractiveSpawners(levelData);
      await InitNPCSpawners(levelData, hero);
    }
    private async Task InitHud()
    {
      GameObject hud = await _gameFactory.CreateHud();
    }
    private async Task<GameObject> InitHero()
    {
      GameObject hero = await _gameFactory.CreateHero();
      CameraFollow(hero);
      return hero;
    }
    private async Task InitInteractiveSpawners(LevelStaticData levelStaticData)
    {
      foreach (InteractiveSpawnStaticData spawnerData in levelStaticData.InteractiveSpawnMarker)
        await _gameFactory.CreateInteractiveSpawner(spawnerData.Id, spawnerData.Position, spawnerData.InteractiveID);
    }
    private async Task InitNPCSpawners(LevelStaticData levelStaticData, GameObject hero)
    {
      foreach (SpawnMarkerNPCStaticData spawnerData in levelStaticData.NPCSpawnMarker)
        await _gameFactory.CreateNPCSpawner(spawnerData.NpcId, spawnerData.Id, spawnerData.Position, hero);
    }
    private void CameraFollow(GameObject hero) =>
      Camera.main.GetComponent<CameraFollow>().Follow(hero);
    private LevelStaticData LevelStaticData() => 
      _staticData.ForLevel(SceneManager.GetActiveScene().name);
  }
}