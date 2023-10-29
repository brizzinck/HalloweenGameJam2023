namespace CodeBase.UI.Elements
{
  public class UpgradeHeroSpeed : UpgradeItemBuy
  {
    protected override void BuyUpgrade() => 
      staticData.GameTempData.SpeedHero = buyData[currentUpgrade].UpgradeToScore;

    protected override void UpdateText()
    {
      base.UpdateText();
      currentScoreText.text = staticData.GameTempData.SpeedHero.ToString();
    }

    protected override void CalculateUpgrade()
    {
      currentUpgrade = 0;
      foreach (BuyData data in buyData)
      {
        if (staticData.GameTempData.SpeedHero >= data.UpgradeToScore)
          currentUpgrade++;
        else
          break;
      }
    }
  }
}