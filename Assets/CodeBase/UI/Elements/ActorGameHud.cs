using System;
using CodeBase.Infrastructure.States;
using CodeBase.Services.GameScoreService;
using CodeBase.Services.Input;
using CodeBase.Services.PersistentProgress;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Elements
{
  public class ActorGameHud : MonoBehaviour
  {
    [SerializeField] private Slider _slider;
    [SerializeField] private GameObject _pressF;
    [SerializeField] private TextMeshProUGUI _pressEText;
    [SerializeField][Multiline(2)] private string _activeEText;
    [SerializeField][Multiline(2)] private string _disableEText;
    private IGameStateMachine _stateMachine;
    private IPersistentProgressService _progressService;
    private IGameScoreService _gameScoreService;
    private IDisplayInputService _displayInputService;

    private void OnDestroy()
    {
      _gameScoreService.ChangeHappyScore -= UpdateDisplayScore;
      _displayInputService.PressF -= DisplayF;
      _displayInputService.PressE -= DisplayE;
    }

    public void Construct(IGameStateMachine stateMachine, IPersistentProgressService progressService,
      IGameScoreService gameScoreService, IDisplayInputService displayInputService)
    {
      _stateMachine = stateMachine;
      _progressService = progressService;
      _gameScoreService = gameScoreService;
      _displayInputService = displayInputService;
      _gameScoreService.ChangeHappyScore += UpdateDisplayScore;
      _displayInputService.PressE += DisplayE;
      _displayInputService.PressF += DisplayF;
      UpdateDisplayScore(_gameScoreService.HappyScore);
    }

    private void DisplayF(bool display) => 
      _pressF.SetActive(display);

    private void DisplayE(bool display)
    {
      if (display)
        _pressEText.text = _activeEText;
      else
        _pressEText.text = _disableEText;
    }

    private void UpdateDisplayScore(int score) => 
      _slider.DOValue(score, 0.75f);
  }
}