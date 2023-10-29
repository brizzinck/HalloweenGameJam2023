using CodeBase.Abilities;
using CodeBase.Services.StaticData;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace CodeBase.UI.Elements
{
  public class AbilityItemBuy : MonoBehaviour
  {
    [SerializeField] private Button _buyBtn;
    [SerializeField] private int _cost;
    [SerializeField] private TextMeshProUGUI _costText;
    [SerializeField] private AbilityID _abilityID;
    private IStaticDataService _staticData;
    public void Construct(IStaticDataService staticData)
    {
      _staticData = staticData;
      _costText.text = _cost.ToString();
      _buyBtn.onClick.AddListener(BuyAbility);
      if (_staticData.ForAvailableAbilities(_abilityID)) 
        UpdateToBuy();
    }

    private void BuyAbility()
    {
      if (_staticData.GameTempData.CountSoul >= _cost)
      {
        _staticData.GameTempData.ChangeSoul(-_cost);
        _staticData.GameTempData.AvailableAbilityIds.Add(_abilityID);
        UpdateToBuy();
      }
    }

    private void UpdateToBuy()
    {
      _costText.text = "Уже куплено!";
      _buyBtn.interactable = false;
    }
  }
}