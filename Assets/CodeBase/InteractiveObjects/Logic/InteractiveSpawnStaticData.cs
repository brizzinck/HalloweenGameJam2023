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

    public InteractiveSpawnStaticData(string id, InteractiveID interactiveID, Vector3 position)
    {
      Id = id;
      InteractiveID = interactiveID;
      Position = position;
    }
  }
}