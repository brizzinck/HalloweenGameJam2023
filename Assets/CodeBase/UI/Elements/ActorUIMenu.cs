using CodeBase.Infrastructure.States;
using CodeBase.Services.PersistentProgress;
using UnityEngine;
using UnityEngine.UI;

public class ActorUIMenu : MonoBehaviour
{
  [SerializeField] private Button _playBtn;
  private IGameStateMachine _stateMachine;
  private IPersistentProgressService _progressService;
  public void Construct(IGameStateMachine stateMachine, IPersistentProgressService progressService)
  {
    _stateMachine = stateMachine;
    _progressService = progressService;
    InitialButtons();
  }

  private void InitialButtons()
  {
    _playBtn.onClick.AddListener(() =>
      _stateMachine.Enter<LoadProgressState>());
  }
}