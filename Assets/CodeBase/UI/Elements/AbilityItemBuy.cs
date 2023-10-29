using CodeBase.Abilities;
using CodeBase.Services.StaticData;
using TMPro;
using UnityEngine;

namespace CodeBase.UI.Elements
{
  public class AbilityItemBuy : MonoBehaviour
  {
    [SerializeField] private int _cost;
    [SerializeField] private TextMeshProUGUI _costText;
    [SerializeField] private TextMeshProUGUI _existText;
    [SerializeField] private AbilityID _abilityID;
    private IStaticDataService _staticData;
    public void Construct(IStaticDataService staticData)
    {
      _staticData = staticData;
      _costText.text = _cost.ToString();
      
      if (!_staticData.ForAvailableAbilities(_abilityID))
        BuyAbility();
      else
        UpdateToBuy();
    }

    private void BuyAbility()
    {
      if (_staticData.GameTempData.CountSoul >= _cost)
      {
        _staticData.GameTempData.AvailableAbilityIds.Add(_abilityID);
        UpdateToBuy();
      }
      else
        UpdateToNoBuy();
    }

    private void UpdateToBuy()
    {
      _costText.gameObject.SetActive(false);
      _existText.gameObject.SetActive(true);
    }
    private void UpdateToNoBuy()
    {
      _costText.gameObject.SetActive(true);
      _existText.gameObject.SetActive(false);
    }
  }
}