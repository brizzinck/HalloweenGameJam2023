using System;
using CodeBase.Hero;
using CodeBase.Services.Input;
using UnityEngine;

namespace CodeBase.InteractiveObjects.Base
{
  public class InteractiveDetector : MonoBehaviour
  {
    [SerializeField] private BaseInteractiveObject _interactiveObject;
    public Action HeroPress;
    private IInputService _inputService;
    private IDisplayInputService _displayInputService;
    private bool _heroEnter;

    public void Constructor(IInputService inputService, IDisplayInputService displayInputService)
    {
      _inputService = inputService;
      _displayInputService = displayInputService;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
      if (other.TryGetComponent(out HeroMove heroMove))
      {
        _heroEnter = true;
        if (!_interactiveObject.IsDestroy)
          _displayInputService.OnActionF(_heroEnter);
        _interactiveObject.HeroMove = heroMove;
      }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
      if (other.TryGetComponent(out HeroMove _))
      {
        _heroEnter = false;
        _displayInputService.OnActionF(_heroEnter);
        _interactiveObject.HeroMove = null;
      }
    }

    private void Update()
    {
      if (_heroEnter && _inputService.PressInteractiveButton())
      {
        HeroPress?.Invoke();
        _displayInputService.OnActionF(false);
      }
    }
  }
}