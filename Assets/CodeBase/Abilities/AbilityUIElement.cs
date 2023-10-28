using CodeBase.Infrastructure.Factory.GameFactory;
using CodeBase.Services.GameScoreService;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Abilities
{
  public class AbilityUIElement : MonoBehaviour
  {
    [SerializeField] private AbilityID _abilityID;
    [SerializeField] private Button _createAbilityBtn;
    private IGameFactory _gameFactory;
    private IGameScoreService _gameScoreService;

    public void Construct(IGameFactory gameFactory, IGameScoreService gameScoreService)
    {
      _gameScoreService = gameScoreService;
      _gameFactory = gameFactory;
      _createAbilityBtn.onClick.AddListener(() => _gameFactory.CreateAbility(_abilityID, _gameScoreService));
    }
  }
}