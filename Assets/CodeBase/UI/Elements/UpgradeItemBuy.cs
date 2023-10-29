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
    [SerializeField] private GameObject[] _offObject;
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
      if (currentUpgrade < buyData.Length && staticData.GameTempData.CountRating >= buyData[currentUpgrade].Cost)
      {
        staticData.GameTempData.ChangeRating(-buyData[currentUpgrade].Cost);
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
        foreach (GameObject obj in _offObject)
          obj.gameObject.SetActive(false);
    }

    protected abstract void BuyUpgrade();
    protected abstract void CalculateUpgrade();
  }
}