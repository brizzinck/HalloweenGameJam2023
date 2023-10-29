using CodeBase.Constants;
using CodeBase.Infrastructure.States;
using CodeBase.Services.StaticData;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Elements
{
  public class ActorEndMenuUI : MonoBehaviour
  {
    [SerializeField] private Button _loadGameBtn;
    [SerializeField] private Button _showShop;
    [SerializeField] private TextMeshProUGUI _countSoulText;
    [SerializeField] private TextMeshProUGUI _countRatigText;
    private IGameStateMachine _stateMachine;
    private IStaticDataService _staticData;

    private void OnDestroy()
    {
      _staticData.GameTempData.OnChangeSoul -= UpdateSoul;
      _staticData.GameTempData.OnChangeRating -= UpdateRating;
    }

    public void Construct(IGameStateMachine stateMachine, GameObject shop, IStaticDataService staticData)
    {
      _stateMachine = stateMachine;
      _staticData = staticData;
      
      UpdateSoul(_staticData.GameTempData.CountSoul);
      UpdateRating(_staticData.GameTempData.CountRating);
      _staticData.GameTempData.OnChangeSoul += UpdateSoul;
      _staticData.GameTempData.OnChangeRating += UpdateRating;
      
      _showShop.onClick.AddListener(() => shop.SetActive(true));
      _loadGameBtn.onClick.AddListener(() => 
        _stateMachine.Enter<LoadGameLevelState, string>(ConstantsValue.MainGame));
    }
    
    private void UpdateRating(int rating) => 
      _countRatigText.text = rating.ToString();

    private void UpdateSoul(int soul) => 
      _countSoulText.text = soul.ToString();
  }
}