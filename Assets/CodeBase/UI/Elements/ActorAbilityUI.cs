using System;
using CodeBase.Abilities;
using CodeBase.Infrastructure.Factory.GameFactory;
using CodeBase.Infrastructure.States;
using CodeBase.Services.GameScoreService;
using CodeBase.Services.Input;
using CodeBase.Services.PersistentProgress;
using UnityEngine;

namespace CodeBase.UI.Elements
{
  public class ActorAbilityUI : MonoBehaviour
  {
    [SerializeField] private GameObject _abilitiesPanel;
    [SerializeField] private AbilityUIElement[] _abilityUIElements;
    private IGameStateMachine _stateMachine;
    private IPersistentProgressService _progressService;
    private IGameScoreService _gameScoreService;
    private IGameFactory _gameFactory;
    private IInputService _inputService;
    
    public void Construct(IGameStateMachine stateMachine, IPersistentProgressService progressService,
      IGameScoreService gameScoreService, IGameFactory gameFactory, IInputService inputService)
    {
      _stateMachine = stateMachine;
      _progressService = progressService;
      _gameScoreService = gameScoreService;
      _gameFactory = gameFactory;
      _inputService = inputService;
      foreach (AbilityUIElement element in _abilityUIElements) 
        element.Construct(_gameFactory, _gameScoreService);
    }
    
    private void Update()
    {
      if (_inputService != null && _inputService.PressAbilityButton()) 
        _abilitiesPanel.SetActive(!_abilitiesPanel.activeSelf);
    }
  }
}