using CodeBase.Infrastructure.Scene;
using CodeBase.Logic.Scene;
using CodeBase.Services.Audio;
using CodeBase.UI.Services.Factory;

namespace CodeBase.Infrastructure.States
{
  public class LoadEndMenuState : IPayloadedState<string>
  {
    private readonly IGameStateMachine _stateMachine;
    private readonly LoadingCurtain _endGameCurtain;
    private readonly SceneLoader _sceneLoader;
    private readonly IUIFactory _uiFactory;
    private readonly IAudioPlayer _audioPlayer;

    public LoadEndMenuState(IGameStateMachine stateMachine, LoadingCurtain endGameCurtain, SceneLoader sceneLoader,
      IUIFactory uiFactory, IAudioPlayer audioPlayer)
    {
      _stateMachine = stateMachine;
      _endGameCurtain = endGameCurtain;
      _sceneLoader = sceneLoader;
      _uiFactory = uiFactory;
      _audioPlayer = audioPlayer;
    }

    public void Enter(string payload = "Menu")
    {
      _sceneLoader.Load(payload, OnLoaded);
      _endGameCurtain.Show();
    }

    private async void OnLoaded()
    {
      _stateMachine.Enter<MenuStayLevelState>();
      await _uiFactory.CreateEndUI();
      await _audioPlayer.CreateAudio();
      _audioPlayer.PlayCurrentScene();
    }

    public void Exit() =>
      _endGameCurtain.Hide();
  }
}