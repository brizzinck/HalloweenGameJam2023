using CodeBase.Infrastructure.Factory.GameFactory;
using CodeBase.Infrastructure.Scene;
using CodeBase.Logic.Scene;
using CodeBase.Services.Audio;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.StaticData;
using CodeBase.UI.Services.Factory;

namespace CodeBase.Infrastructure.States
{
  public class LoadMenuLevelState : IPayloadedState<string>
  {
    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly LoadingCurtain _loadingCurtain;
    private readonly IPersistentProgressService _progressService;
    private readonly IStaticDataService _staticData;
    private readonly IUIFactory _uiFactory;
    private readonly IAudioPlayer _audioPlayer;

    public LoadMenuLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain,
      IPersistentProgressService progressService, IStaticDataService staticDataService,
      IUIFactory uiFactory, IAudioPlayer audioPlayer)
    {
      _stateMachine = gameStateMachine;
      _sceneLoader = sceneLoader;
      _loadingCurtain = loadingCurtain;
      _progressService = progressService;
      _staticData = staticDataService;
      _uiFactory = uiFactory;
      _audioPlayer = audioPlayer;
    }

    public void Enter(string sceneName)
    {
      _loadingCurtain.Show();
      _sceneLoader.Load(sceneName, OnLoaded);
    }

    private async void OnLoaded()
    {
      await _uiFactory.CreateMenuUI();
      await _audioPlayer.CreateAudio();
      _loadingCurtain.Hide();
      _stateMachine.Enter<MenuStayLevelState>();
    }

    public void Exit()
    {
    }
  }
}