using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.Factory.GameFactory;
using CodeBase.Infrastructure.Scene;
using CodeBase.Logic.Scene;
using CodeBase.Services;
using CodeBase.Services.Audio;
using CodeBase.Services.GameLoopService;
using CodeBase.Services.GameScoreService;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.SaveLoad;
using CodeBase.Services.StaticData;
using CodeBase.UI.Services.Factory;

namespace CodeBase.Infrastructure.States
{
  public class GameStateMachine : IGameStateMachine
  {
    private Dictionary<Type, IExitableState> _states;
    private IExitableState _activeState;

    public GameStateMachine(SceneLoader sceneLoader, LoadingCurtain loadingCurtain, AllServices services,
      LoadingCurtain endGameCurtain, LoadingCurtain startCurtain)
    {
      _states = new Dictionary<Type, IExitableState>
      {
        [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, services),

        [typeof(LoadGameLevelState)] = new LoadGameLevelState(this, sceneLoader, loadingCurtain,
          services.Single<IGameFactory>(),
          services.Single<IPersistentProgressService>(),
          services.Single<IStaticDataService>(),
          services.Single<IUIFactory>(),
          services.Single<IGameScoreService>(),
          services.Single<IGameTimer>(),
          services.Single<IAudioPlayer>()),

        [typeof(LoadMenuLevelState)] = new LoadMenuLevelState(
          this,
          sceneLoader,
          startCurtain,
          services.Single<IPersistentProgressService>(),
          services.Single<IStaticDataService>(), services.Single<IUIFactory>(),
          services.Single<IAudioPlayer>()),

        [typeof(LoadProgressState)] = new LoadProgressState(this, services.Single<IPersistentProgressService>(),
          services.Single<ISaveLoadService>()),

        [typeof(LoadEndMenuState)] =
          new LoadEndMenuState(this, endGameCurtain, sceneLoader, services.Single<IUIFactory>(),
            services.Single<IAudioPlayer>()),

        [typeof(GameLoopState)] = new GameLoopState(this),

        [typeof(MenuStayLevelState)] = new MenuStayLevelState(this)
      };
    }

    public void Enter<TState>() where TState : class, IState
    {
      IState state = ChangeState<TState>();
      state.Enter();
    }

    public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
    {
      TState state = ChangeState<TState>();
      state.Enter(payload);
    }

    private TState ChangeState<TState>() where TState : class, IExitableState
    {
      _activeState?.Exit();

      TState state = GetState<TState>();
      _activeState = state;

      return state;
    }

    private TState GetState<TState>() where TState : class, IExitableState =>
      _states[typeof(TState)] as TState;
  }
}