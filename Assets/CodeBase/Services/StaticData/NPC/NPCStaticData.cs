using CodeBase.NPC;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.Services.StaticData.NPC
{
  [CreateAssetMenu(fileName = "NPCStaticData", menuName = "StaticData/NPC")]
  public class NPCStaticData : ScriptableObject
  {
    public NPCId NpcId;
    public AssetReferenceGameObject PrefabReference;
  }
}