using CodeBase.Infrastructure.Scene;
using CodeBase.Infrastructure.States;
using CodeBase.Logic;
using CodeBase.Logic.Scene;
using UnityEngine;

namespace CodeBase.Infrastructure.Game
{
  public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
  {
    public LoadingCurtain CurtainPrefab;
    public LoadingCurtain EndGameCurtain;
    private Game _game;

    private void Awake()
    {
      _game = new Game(this, Instantiate(CurtainPrefab), Instantiate(EndGameCurtain));
      _game.StateMachine.Enter<BootstrapState>();
      DontDestroyOnLoad(this);
    }
  }
}