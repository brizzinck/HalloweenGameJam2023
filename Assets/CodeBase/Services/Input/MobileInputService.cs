using UnityEngine;

namespace CodeBase.Services.Input
{
  public class MobileInputService : InputService
  {
    public override Vector2Int Axis => new Vector2Int((int)SimpleInputAxis().x, (int)SimpleInputAxis().x);
  }
}