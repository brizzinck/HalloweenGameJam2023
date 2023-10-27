using CodeBase.Infrastructure.Scene;
using CodeBase.Logic.Scene;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
  public class MainMenuState : IState
  {
    private const string MainMenuLevel = "MainMenu";

    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly LoadingCurtain _loadingCurtain;

    public MainMenuState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain)
    {
      _stateMachine = stateMachine;
      _sceneLoader = sceneLoader;
      _loadingCurtain = loadingCurtain;
    }

    public void Exit()
    {

    }

    public void Enter() => 
      _sceneLoader.Load(MainMenuLevel, onLoaded: OnLoaded);

        private void OnLoaded()
        {
            _loadingCurtain.Hide(); 
        }
    }
}