using System;
using CodeBase.Infrastructure.States;
using CodeBase.Services.GameLoopService;
using CodeBase.Services.GameScoreService;
using CodeBase.Services.Input;
using CodeBase.Services.InteractiveObject;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.StaticData;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Elements
{
  public class ActorGameHud : MonoBehaviour
  {
    [SerializeField] private Slider _slider;
    [SerializeField] private GameObject _brakingObj;
    [SerializeField] private Slider _sliderBraking;
    [SerializeField] private GameObject _pressF;
    [SerializeField] private TextMeshProUGUI _pressEText;
    [SerializeField][Multiline(2)] private string _activeEText;
    [SerializeField][Multiline(2)] private string _disableEText;
    [SerializeField] private TextMeshProUGUI _timeText;
    private IGameStateMachine _stateMachine;
    private IPersistentProgressService _progressService;
    private IGameScoreService _gameScoreService;
    private IDisplayInputService _displayInputService;
    private IGameTimer _gameTimer;
    private IInteractive _interactive;

    private void OnDestroy()
    {
      _gameScoreService.ChangeHappyScore -= UpdateDisplayScore;
      _displayInputService.PressF -= DisplayF;
      _displayInputService.PressE -= DisplayE;
      _gameTimer.OnUpdateTime -= UpdateTimeText;
      _interactive.OnBraking -= UpdateSliderBraking;
    }
    
    public void Construct(IGameStateMachine stateMachine, IPersistentProgressService progressService,
      IGameScoreService gameScoreService, IDisplayInputService displayInputService, IGameTimer gameTimer,
      IInteractive interactive)
    {
      _stateMachine = stateMachine;
      _progressService = progressService;
      _gameScoreService = gameScoreService;
      _displayInputService = displayInputService;
      _gameTimer = gameTimer;
      _interactive = interactive;
      _gameScoreService.ChangeHappyScore += UpdateDisplayScore;
      _displayInputService.PressE += DisplayE;
      _displayInputService.PressF += DisplayF;
      _gameTimer.OnUpdateTime += UpdateTimeText;
      _interactive.OnBraking += UpdateSliderBraking;
      _slider.value = _gameScoreService.HappyScore;
    }

    private void UpdateSliderBraking(bool active, float start, float end)
    {
      _brakingObj.SetActive(active);
      _sliderBraking.value = start / end;
    }

    private void UpdateTimeText(float time) => 
      _timeText.text = time.ToString("0");

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