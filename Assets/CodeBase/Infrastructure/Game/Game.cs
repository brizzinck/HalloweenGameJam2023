using CodeBase.Infrastructure.Scene;
using CodeBase.Infrastructure.States;
using CodeBase.Logic;
using CodeBase.Logic.Scene;
using CodeBase.Services;

namespace CodeBase.Infrastructure.Game
{
  public class Game
  {
    public GameStateMachine StateMachine;

    public Game(ICoroutineRunner coroutineRunner, LoadingCurtain loadingCurtain, LoadingCurtain endGameCurtain) => 
      StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), loadingCurtain, AllServices.Container, endGameCurtain);
  }
}