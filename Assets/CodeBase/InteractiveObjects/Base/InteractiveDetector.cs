using System;
using CodeBase.Hero;
using CodeBase.Services.Input;
using UnityEngine;

namespace CodeBase.InteractiveObjects.Base
{
  public class InteractiveDetector : MonoBehaviour
  {
    public Action HeroEnter;
    private IInputService _inputService;
    private bool _checkPress;

    public void Constructor(IInputService inputService) =>
      _inputService = inputService;

    private void OnTriggerEnter2D(Collider2D other)
    {
      if (other.TryGetComponent(out HeroMove _))
        _checkPress = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
      if (other.TryGetComponent(out HeroMove _))
        _checkPress = false;
    }

    private void Update()
    {
      if (_checkPress && _inputService.PressInteractiveButton())
        HeroEnter?.Invoke();
    }
  }
}