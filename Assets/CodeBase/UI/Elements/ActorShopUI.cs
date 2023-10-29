using System;
using CodeBase.Services.StaticData;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Elements
{
  public class ActorShopUI : MonoBehaviour
  {
    [SerializeField] private Button _closeShop;
    [SerializeField] private TextMeshProUGUI _countSoulText;
    [SerializeField] private TextMeshProUGUI _countRatigText;
    [SerializeField] private AbilityItemBuy[] _abilityItems;
    [SerializeField] private UpgradeItemBuy[] _upgradeItems;
    private IStaticDataService _staticData;

    private void Awake() => 
      gameObject.SetActive(false);

    private void OnDestroy()
    {
      _staticData.GameTempData.OnChangeSoul -= UpdateSoul;
      _staticData.GameTempData.OnChangeRating -= UpdateRating;
    }

    public void Construct(IStaticDataService staticData)
    {
      _staticData = staticData;
      _closeShop.onClick.AddListener(() => gameObject.SetActive(false));
      
      UpdateSoul(_staticData.GameTempData.CountSoul);
      UpdateRating(_staticData.GameTempData.CountRating);
      
      _staticData.GameTempData.OnChangeSoul += UpdateSoul;
      _staticData.GameTempData.OnChangeRating += UpdateRating;
      
      foreach (AbilityItemBuy abilityItemBuy in _abilityItems) 
        abilityItemBuy.Construct(staticData);
      foreach (UpgradeItemBuy upgradeItem in _upgradeItems) 
        upgradeItem.Construct(staticData);
    }

    private void UpdateRating(int rating) => 
      _countRatigText.text = rating.ToString();

    private void UpdateSoul(int soul) => 
      _countSoulText.text = soul.ToString();
  }
}