using UnityEngine;

namespace CodeBase.Services.Input
{
  public interface IInputService : IService
  {
    Vector2Int Axis { get; }

    bool PressInteractiveButton();
    bool PressAbilityButton();
  }
}