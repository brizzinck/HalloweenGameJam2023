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
    public static Vector3 WithToX(this Vector3 vector, float x) => 
      new Vector3(x, vector.y, vector.y);
    public static Vector3 WithToZ(this Vector3 vector, float Z) => 
      new Vector3(vector.x, vector.y, Z);
  }
}