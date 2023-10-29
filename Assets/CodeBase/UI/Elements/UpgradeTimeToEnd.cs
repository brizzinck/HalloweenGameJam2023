namespace CodeBase.UI.Elements
{
  public class UpgradeTimeToEnd : UpgradeItemBuy
  {
    protected override void BuyUpgrade() => 
      staticData.GameTempData.TimeToEnd = buyData[currentUpgrade].UpgradeToScore;

    protected override void UpdateText()
    {
      base.UpdateText();
      currentScoreText.text = staticData.GameTempData.TimeToEnd.ToString();
    }
    protected override void CalculateUpgrade()
    {
      currentUpgrade = 0;
      foreach (BuyData data in buyData)
      {
        if (staticData.GameTempData.TimeToEnd >= data.UpgradeToScore)
          currentUpgrade++;
        else
          break;
      }
    }
  }
}