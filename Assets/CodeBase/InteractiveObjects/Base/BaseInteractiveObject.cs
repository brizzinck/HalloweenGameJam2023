using System;
using CodeBase.InteractiveObjects.Logic;
using CodeBase.Services.Input;
using UnityEngine;

namespace CodeBase.InteractiveObjects.Base
{
  public abstract class BaseInteractiveObject : MonoBehaviour
  {
    [SerializeField] private InteractiveID _interactiveID;
    [SerializeField] private InteractiveDetector _interactiveDetector;

    private void Awake() =>
      OnAwake();
    private void OnDestroy() =>
      OnDestroyAction();

    public virtual void Constructor(IInputService inputService) => 
      _interactiveDetector.Constructor(inputService);

    protected virtual void OnAwake() => 
      _interactiveDetector.HeroEnter += OpenInteractiveWindow;
    protected virtual void OnDestroyAction() => 
      _interactiveDetector.HeroEnter -= OpenInteractiveWindow;
    protected abstract void OpenInteractiveWindow();
  }
}