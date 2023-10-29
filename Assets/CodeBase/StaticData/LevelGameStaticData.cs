using System.Collections.Generic;
using CodeBase.InteractiveObjects.Logic;
using CodeBase.NPC;
using UnityEngine;

namespace CodeBase.StaticData
{
  [CreateAssetMenu(fileName = "LevelGameStaticData", menuName = "StaticData/LevelGameStaticData")]
  public class LevelGameStaticData : LevelStaticData
  {
    public Vector3 HeroSpawnPoint;
    public List<InteractiveSpawnStaticData> InteractiveSpawnMarker;
    public List<SpawnMarkerNPCStaticData> NPCSpawnMarker;
  }
}