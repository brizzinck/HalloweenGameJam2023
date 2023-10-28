using System.Collections.Generic;
using CodeBase.InteractiveObjects.Logic;
using CodeBase.NPC;
using UnityEngine;

namespace CodeBase.StaticData
{
  [CreateAssetMenu(fileName = "LevelData", menuName = "StaticData/Level")]
  public class LevelStaticData : ScriptableObject
  {
    public string LevelKey;
    public Vector3 HeroSpawnPoint;
    public List<InteractiveSpawnStaticData> InteractiveSpawnMarker;
    public List<SpawnMarkerNPCStaticData> NPCSpawnMarker;
  }
}