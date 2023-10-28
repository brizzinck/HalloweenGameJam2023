using CodeBase.Infrastructure.States;
using CodeBase.Services.GameScoreService;
using CodeBase.Services.PersistentProgress;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Elements
{
  public class ActorGameHud : MonoBehaviour
  {
    [SerializeField] private Slider _slider;
    private IGameStateMachine _stateMachine;
    private IPersistentProgressService _progressService;
    private IGameScoreService _gameScoreService;
    
    public void Construct(IGameStateMachine stateMachine, IPersistentProgressService progressService, IGameScoreService gameScoreService)
    {
      _stateMachine = stateMachine;
      _progressService = progressService;
      _gameScoreService = gameScoreService;
      _gameScoreService.ChangeHappyScore += UpdateDisplayScore;
      UpdateDisplayScore(_gameScoreService.HappyScore);
    }

    private void OnDestroy()
    {
      _gameScoreService.ChangeHappyScore -= UpdateDisplayScore;
    }
    

    private void UpdateDisplayScore(int score) => 
      _slider.DOValue(score, 0.75f);
  }
}