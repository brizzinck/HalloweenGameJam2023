using UnityEngine;

namespace CodeBase.Services.Input
{
  public class StandaloneInputService : InputService
  {
    public override Vector2Int Axis
    {
      get
      {
        Vector2 axis = SimpleInputAxisRaw();
        if (axis == Vector2.zero) 
          axis = UnityAxis();
        return new Vector2Int((int)axis.x, (int)axis.y);
      }
    }

    private static Vector2Int UnityAxis() => 
      new Vector2Int((int)UnityEngine.Input.GetAxisRaw(Horizontal), (int)UnityEngine.Input.GetAxisRaw(Vertical));
  }
}