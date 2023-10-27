using UnityEngine;

namespace CodeBase.Extensions
{
  public static class VectorExtensions
  {
    public static Vector3 AddY(this Vector3 vector, float y)
    {
      vector.y = y;
      return vector;
    }
    public static Vector3 OnlyToX(this Vector3 vector) => 
      new Vector3(vector.x, 0, 0);
    public static Vector3 OnlyToY(this Vector3 vector) => 
      new Vector3(0, vector.y, 0);
  }
}