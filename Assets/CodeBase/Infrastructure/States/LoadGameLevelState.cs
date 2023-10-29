using System.Threading.Tasks;
using CodeBase.CameraLogic;
using CodeBase.Infrastructure.Factory.GameFactory;
using CodeBase.Infrastructure.Scene;
using CodeBase.InteractiveObjects.Logic;
using CodeBase.Logic.Scene;
using CodeBase.NPC;
using CodeBase.Services.Audio;
using CodeBase.Services.GameLoopService;
using CodeBase.Services.GameScoreService;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.StaticData;
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
    private readonly IGameScoreService _gameScoreService;
    private readonly IGameTimer _gameTimer;
    private readonly IAudioPlayer _audioPlayer;

    public LoadGameLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain,
      IGameFactory gameFactory, IPersistentProgressService progressService, IStaticDataService staticDataService,
      IUIFactory uiFactory, IGameScoreService gameScoreService, IGameTimer gameTimer, IAudioPlayer audioPlayer)
    {
      _stateMachine = gameStateMachine;
      _sceneLoader = sceneLoader;
      _loadingCurtain = loadingCurtain;
      _gameFactory = gameFactory;
      _progressService = progressService;
      _staticData = staticDataService;
      _uiFactory = uiFactory;
      _gameScoreService = gameScoreService;
      _gameTimer = gameTimer;
      _audioPlayer = audioPlayer;
    }
    
    public void Enter(string sceneName)
    {
      _loadingCurtain.Show();
      _gameFactory.Cleanup();
      _gameFactory.WarmUp();
      _sceneLoader.Load(sceneName, OnLoaded);
    }

    public void Exit() =>
      _loadingCurtain.Hide();

    private async void OnLoaded()
    {
      await InitUIRoot();
      await InitGameHud();
      await InitAbilityUI();
      await InitGameWorld();
      await InitGuideUI();
      await _audioPlayer.CreateAudio();
      InformProgressReaders();
      RefreshGameData();
      _stateMachine.Enter<GameLoopState>();
    }

    private void RefreshGameData()
    {
      _gameTimer.CurrentTime = _staticData.GameTempData.TimeToEnd;
      _gameScoreService.RefreshScore();
    }

    private async Task InitUIRoot() =>
      await _uiFactory.CreateUIRoot();

    private async Task InitGameHud() =>
      await _uiFactory.CreateGameHud();
    private async Task InitGuideUI() =>
      await _uiFactory.CreateGameGuideUI();

    private async Task InitAbilityUI() =>
      await _uiFactory.CreateAbilityUI();

    private void InformProgressReaders()
    {
      foreach (ISavedProgressReader progressReader in _gameFactory.ProgressReaders)
        progressReader.LoadProgress(_progressService.Progress);
    }

    private async Task InitGameWorld()
    {
      LevelGameStaticData levelGameData = LevelStaticData();
      await InitHud();
      GameObject hero = await InitHero();
      await InitInteractiveSpawners(levelGameData);
      await InitNPCSpawners(levelGameData, hero);
      await InitGameLoop();
    }

    private async Task InitHud()
    {
      GameObject hud = await _gameFactory.CreateHud();
    }

    private async Task InitGameLoop()
    {
      GameLoop.GameLoop hud = await _gameFactory.CreateGameLoop();
    }

    private async Task<GameObject> InitHero()
    {
      GameObject hero = await _gameFactory.CreateHero();
      CameraFollow(hero);
      return hero;
    }

    private async Task InitInteractiveSpawners(LevelGameStaticData levelGameStaticData)
    {
      foreach (InteractiveSpawnStaticData spawnerData in levelGameStaticData.InteractiveSpawnMarker)
        await _gameFactory.CreateInteractiveSpawner(spawnerData.Id, spawnerData.Position, spawnerData.InteractiveID);
    }

    private async Task InitNPCSpawners(LevelGameStaticData levelGameStaticData, GameObject hero)
    {
      foreach (SpawnMarkerNPCStaticData spawnerData in levelGameStaticData.NPCSpawnMarker)
        await _gameFactory.CreateNPCSpawner(spawnerData.NpcId, spawnerData.Id, spawnerData.Position, hero);
    }

    private void CameraFollow(GameObject hero) =>
      Camera.main.GetComponent<CameraFollow>().Follow(hero);

    private LevelGameStaticData LevelStaticData() =>
      _staticData.ForGameLevel(SceneManager.GetActiveScene().name);
  }
}