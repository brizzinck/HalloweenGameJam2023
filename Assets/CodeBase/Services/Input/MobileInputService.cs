using UnityEngine;

namespace CodeBase.Services.Input
{
  public class MobileInputService : InputService
  {
    public override Vector2Int Axis => SimpleInputAxisRaw();
  }
}