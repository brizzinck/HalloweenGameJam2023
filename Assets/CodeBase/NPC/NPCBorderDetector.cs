using System;
using UnityEngine;

namespace CodeBase.NPC
{
  public class NPCBorderDetector : MonoBehaviour
  {
    public Action<Vector3> EntryObstacle;
    private const string ObstacleTag = "Obstacle";
    private void OnTriggerEnter2D(Collider2D other) => 
      CheckObstacle(other);
    private void CheckObstacle(Collider2D other)
    {
      if (other.CompareTag(ObstacleTag)) 
        EntryObstacle?.Invoke(Vector3.one);
    }
  }
}