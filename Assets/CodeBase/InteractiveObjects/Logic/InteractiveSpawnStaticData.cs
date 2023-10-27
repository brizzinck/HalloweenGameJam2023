using System;
using UnityEngine;

namespace CodeBase.InteractiveObjects.Logic
{
  [Serializable]
  public class InteractiveSpawnStaticData 
  {
    public string Id;
    public InteractiveID InteractiveID;
    public Vector3 Position;

    public InteractiveSpawnStaticData(string id, InteractiveID monsterTypeId, Vector3 position)
    {
      Id = id;
      InteractiveID = monsterTypeId;
      Position = position;
    }
  }
}