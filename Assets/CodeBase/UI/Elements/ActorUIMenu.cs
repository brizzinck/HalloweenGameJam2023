using CodeBase.Infrastructure.States;
using CodeBase.Services.Audio;
using CodeBase.Services.PersistentProgress;
using UnityEngine;
using UnityEngine.UI;

public class ActorUIMenu : MonoBehaviour
{
  [SerializeField] private Button _playBtn;
  [SerializeField] private Slider _sliderVolume;
  private IGameStateMachine _stateMachine;
  private IPersistentProgressService _progressService;
  private IAudioPlayer _audioPlayer;
  
  public void Construct(IGameStateMachine stateMachine, IPersistentProgressService progressService, IAudioPlayer audioPlayer)
  {
    _stateMachine = stateMachine;
    _progressService = progressService;
    _audioPlayer = audioPlayer;
    _sliderVolume.SetValueWithoutNotify(_audioPlayer.Volume);
    _sliderVolume.onValueChanged.AddListener(_audioPlayer.ChangeVolume);
    InitialButtons();
  }
  
  private void InitialButtons()
  {
    _playBtn.onClick.AddListener(() =>
      _stateMachine.Enter<LoadProgressState>());
  }
}