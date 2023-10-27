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
    public void Constructor(IInputService inputService)
    {
      _inputService = inputService;
    }
    private void OnTriggerStay(Collider other)
    {
      if (other.TryGetComponent(out HeroMove _))
      {
        if (_inputService.PressInteractiveButton())
        {
          HeroEnter?.Invoke();
        }
      }
    }
  }
}