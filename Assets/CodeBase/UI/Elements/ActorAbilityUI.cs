using CodeBase.Abilities;
using CodeBase.Infrastructure.Factory.GameFactory;
using CodeBase.Infrastructure.States;
using CodeBase.Services.GameScoreService;
using CodeBase.Services.PersistentProgress;
using UnityEngine;

namespace CodeBase.UI.Elements
{
  public class ActorAbilityUI : MonoBehaviour
  {
    [SerializeField] private AbilityUIElement[] _abilityUIElements;
    private IGameStateMachine _stateMachine;
    private IPersistentProgressService _progressService;
    private IGameScoreService _gameScoreService;
    private IGameFactory _gameFactory;

    public void Construct(IGameStateMachine stateMachine, IPersistentProgressService progressService,
      IGameScoreService gameScoreService, IGameFactory gameFactory)
    {
      _stateMachine = stateMachine;
      _progressService = progressService;
      _gameScoreService = gameScoreService;
      _gameFactory = gameFactory;
      foreach (AbilityUIElement element in _abilityUIElements) 
        element.Construct(_gameFactory, _gameScoreService);
    }
  }
}