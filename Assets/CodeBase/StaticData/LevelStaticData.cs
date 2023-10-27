using System.Collections.Generic;
using CodeBase.InteractiveObjects.Logic;
using UnityEngine;

namespace CodeBase.StaticData
{
  [CreateAssetMenu(fileName = "LevelData", menuName = "Static Data/Level")]
  public class LevelStaticData : ScriptableObject
  {
    public string LevelKey;
    public List<InteractiveSpawnStaticData> InteractiveSpawnMarker;
  }
}