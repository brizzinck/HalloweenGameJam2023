using System;
using UnityEngine;

namespace CodeBase.NPC
{
  [Serializable]
  public class SpawnMarkerNPCStaticData
  {
    public string Id;
    public Vector3 Position;

    public SpawnMarkerNPCStaticData(string id, Vector3 position)
    {
      Id = id;
      Position = position;
    }
  }
}