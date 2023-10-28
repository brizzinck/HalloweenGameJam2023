using CodeBase.Infrastructure.Factory.GameFactory;
using CodeBase.Services.GameScoreService;
using CodeBase.Services.Input;
using CodeBase.Services.StaticData;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Abilities
{
  public class AbilityUIElement : MonoBehaviour
  {
    [SerializeField] private AbilityID _abilityID;
    [SerializeField] private Button _createAbilityBtn;
    [SerializeField] private float _cooldown;
    [SerializeField] private Image _fillImageCooldown;
    private float _currentCooldown;
    private IGameFactory _gameFactory;
    private IGameScoreService _gameScoreService;
    private IStaticDataService _staticData;

    private void Update()
    {
      if (_currentCooldown <= _cooldown)
      {
        _currentCooldown += Time.deltaTime;
        UpdateFill();
      }
    }

    public void Construct(IGameFactory gameFactory, IGameScoreService gameScoreService, IStaticDataService staticData)
    {
      _gameScoreService = gameScoreService;
      _gameFactory = gameFactory;
      _staticData = staticData;
      _currentCooldown = _cooldown;
      UpdateFill();
      _createAbilityBtn.onClick.AddListener(SpawnAbility);
    }

    private void SpawnAbility()
    {
      if (_currentCooldown >= _cooldown)
      {
        _gameFactory.CreateAbility(_abilityID, _gameScoreService, _staticData);
        _currentCooldown = 0;
        UpdateFill();
      }
    }

    private void UpdateFill() =>
      _fillImageCooldown.fillAmount = _currentCooldown / _cooldown;
  }
}