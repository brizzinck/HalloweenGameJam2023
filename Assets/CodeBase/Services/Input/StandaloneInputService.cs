using UnityEngine;

namespace CodeBase.Services.Input
{
  public class StandaloneInputService : InputService
  {
    public override Vector2Int Axis
    {
      get
      {
        Vector2 axis = SimpleInputAxis();
        if (axis == Vector2.zero) 
          axis = UnityAxis();
        return new Vector2Int((int)axis.x, (int)axis.y);
      }
    }

    private static Vector2 UnityAxis() => 
      new Vector2(UnityEngine.Input.GetAxisRaw(Horizontal), UnityEngine.Input.GetAxisRaw(Vertical));
  }
}