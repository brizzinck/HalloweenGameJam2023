using CodeBase.Infrastructure.Scene;
using CodeBase.Logic.Scene;

namespace CodeBase.Infrastructure.States
{
  public class LoadEndMenuState : IPayloadedState<string>
  {
    private readonly IGameStateMachine _stateMachine;
    private readonly LoadingCurtain _endGameCurtain;
    private readonly SceneLoader _sceneLoader;

    public LoadEndMenuState(IGameStateMachine stateMachine, LoadingCurtain endGameCurtain, SceneLoader sceneLoader)
    {
      _stateMachine = stateMachine;
      _endGameCurtain = endGameCurtain;
      _sceneLoader = sceneLoader;
    }

    public void Enter(string payload = "Menu")
    {
      _sceneLoader.Load(payload, OnLoaded);
      _endGameCurtain.Show();
    }

    private async void OnLoaded()
    {
      _stateMachine.Enter<MenuStayLevelState>();
    }

    public void Exit() =>
      _endGameCurtain.Hide();
  }
}