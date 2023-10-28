using System;
using UnityEngine;

namespace CodeBase.NPC
{
  [Serializable]
  public class SpawnMarkerNPCStaticData
  {
    public NPCId NpcId;
    public string Id;
    public Vector3 Position;

    public SpawnMarkerNPCStaticData(NPCId npcId, string id, Vector3 position)
    {
      NpcId = npcId;
      Id = id;
      Position = position;
    }
  }
}