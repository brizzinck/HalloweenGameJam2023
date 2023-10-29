using System;
using CodeBase.Services.StaticData;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Elements
{
  public abstract class UpgradeItemBuy : MonoBehaviour
  {
    [SerializeField] private Button _buyBtn;
    [SerializeField] protected BuyData[] buyData;
    [SerializeField] private TextMeshProUGUI _costText;
    [SerializeField] private TextMeshProUGUI _upgradeScoreText;
    [SerializeField] protected TextMeshProUGUI currentScoreText;
    protected int currentUpgrade;
    protected IStaticDataService staticData;

    [Serializable]
    public class BuyData
    {
      public int UpgradeToScore;
      public int Cost;
    }

    public void Construct(IStaticDataService staticData)
    {
      this.staticData = staticData;
      CalculateUpgrade();
      _buyBtn.onClick.AddListener(CheckBuyUpgrade);
      UpdateText();
    }
    
    private void CheckBuyUpgrade()
    {
      if (currentUpgrade < buyData.Length && staticData.GameTempData.CountSoul >= buyData[currentUpgrade].Cost)
      {
        staticData.GameTempData.ChangeSoul(-buyData[currentUpgrade].Cost);
        BuyUpgrade();
        CalculateUpgrade();
        UpdateText();
      }
    }

    protected virtual void UpdateText()
    {
      if (currentUpgrade < buyData.Length)
      {
        _costText.text = buyData[currentUpgrade].Cost.ToString();
        _upgradeScoreText.text = buyData[currentUpgrade].UpgradeToScore.ToString();
      }
      else
      {
        _costText.text = "Макс.";
        _upgradeScoreText.text = "Макс.";
      }
    }

    protected abstract void BuyUpgrade();
    protected abstract void CalculateUpgrade();
  }
}