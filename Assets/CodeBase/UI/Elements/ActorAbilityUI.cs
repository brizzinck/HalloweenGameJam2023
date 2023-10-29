using CodeBase.Abilities;
using CodeBase.Infrastructure.Factory.GameFactory;
using CodeBase.Infrastructure.States;
using CodeBase.Services.GameScoreService;
using CodeBase.Services.Input;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.StaticData;
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
    private IStaticDataService _staticData;
    private IDisplayInputService _displayInputService;

    public void Construct(IGameStateMachine stateMachine, IPersistentProgressService progressService,
      IGameScoreService gameScoreService, IGameFactory gameFactory, IInputService inputService, IStaticDataService staticData,
      IDisplayInputService displayInputService)
    {
      _stateMachine = stateMachine;
      _progressService = progressService;
      _gameScoreService = gameScoreService;
      _gameFactory = gameFactory;
      _inputService = inputService;
      _staticData = staticData;
      _displayInputService = displayInputService;
      foreach (AbilityUIElement element in _abilityUIElements) 
        element.Construct(_gameFactory, _gameScoreService, _staticData);
      _displayInputService.OnActionE(_abilitiesPanel.activeSelf);
    }
    private void Update()
    {
      if (_inputService != null && _inputService.PressAbilityButton())
      {
        _abilitiesPanel.SetActive(!_abilitiesPanel.activeSelf);
        _displayInputService.OnActionE(_abilitiesPanel.activeSelf);
      }
    }
  }
}