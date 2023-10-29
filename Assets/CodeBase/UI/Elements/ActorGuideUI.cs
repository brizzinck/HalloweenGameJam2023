using CodeBase.Services.Input;
using UnityEngine;

namespace CodeBase.UI.Elements
{
  public class ActorGuideUI : MonoBehaviour
  {
    [SerializeField] private GameObject _guidePanel;
    private IInputService _inputService;

    public void Construct(IInputService inputService)
    {
      _inputService = inputService;
    }

    private void Start() => 
      _guidePanel.gameObject.SetActive(false);

    private void Update()
    {
      if (_inputService.PressGuideButton()) 
        _guidePanel.SetActive(!_guidePanel.activeSelf);
    }
  }
}