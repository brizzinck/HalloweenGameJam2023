using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Factory.GameFactory;
using CodeBase.Infrastructure.Scene;
using CodeBase.Services;
using CodeBase.Services.GameScoreService;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.StaticData;
using CodeBase.Services.Randomizer;
using CodeBase.Services.SaveLoad;
using CodeBase.UI.Services.Factory;
using CodeBase.UI.Services.Windows;
using CodeBase.Services.Input;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
  public class BootstrapState : IState
  {
    private const string Initial = "Initial";
    private const string MenuLevel = "EntryMenu";
    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly AllServices _services;

    public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services)
    {
      _stateMachine = stateMachine;
      _sceneLoader = sceneLoader;
      _services = services;

      RegisterServices();
    }

    public void Enter() =>
      _sceneLoader.Load(Initial, onLoaded: EnterLoadLevel);

    public void Exit()
    {
    }

    private void RegisterServices()
    {
      _services.RegisterSingle<IInputService>(
        implementation: InputService());
      _services.RegisterSingle<IDisplayInputService>(
        implementation: new DisplayInputService());
      _services.RegisterSingle<IGameScoreService>(
        implementation: new GameScoreService());
      _services.RegisterSingle<IRandomService>(
        implementation: new RandomService());
      _services.RegisterSingle<IGameStateMachine>(
        implementation: _stateMachine);
      _services.RegisterSingle<IPersistentProgressService>(
        implementation: new PersistentProgressService());
      RegisterAssetProvider();
      RegisterStaticDataService();
      _services.RegisterSingle<IGameFactory>(new GameFactory(
        assets: _services.Single<IAssetProvider>(),
        staticData: _services.Single<IStaticDataService>(),
        inputService: _services.Single<IInputService>(),
        gameScoreService: _services.Single<IGameScoreService>(),
        displayInputService: _services.Single<IDisplayInputService>()));
      _services.RegisterSingle<IUIFactory>(new UIFactory(
        stateMachine: _stateMachine,
        assets: _services.Single<IAssetProvider>(),
        staticData: _services.Single<IStaticDataService>(),
        progressService:_services.Single<IPersistentProgressService>(),
        gameScoreService: _services.Single<IGameScoreService>(),
        gameFactory: _services.Single<IGameFactory>(),
        inputService: _services.Single<IInputService>(),
        displayInputService: _services.Single<IDisplayInputService>()));
      _services.RegisterSingle<IWindowService>(new WindowService(
        uiFactory:_services.Single<IUIFactory>()));
      _services.RegisterSingle<ISaveLoadService>(new SaveLoadService(
        progressService: _services.Single<IPersistentProgressService>(),
        gameFactory: _services.Single<IGameFactory>()));
    }

    private void RegisterAssetProvider()
    {
      AssetProvider assetProvider = new AssetProvider();
      assetProvider.Initialize();
      _services.RegisterSingle<IAssetProvider>(assetProvider);
    }
    
    private void RegisterStaticDataService()
    {
      IStaticDataService staticData = new StaticDataService();
      staticData.Load();
      _services.RegisterSingle(staticData);
    }
    private void EnterLoadLevel() =>
      _stateMachine.Enter<LoadMenuLevelState, string>(MenuLevel);
    private static IInputService InputService() =>
      Application.isEditor
        ? new StandaloneInputService()
        : new MobileInputService();
  }
}