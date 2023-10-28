using CodeBase.Abilities;
using UnityEngine.AddressableAssets;
using UnityEngine;

namespace CodeBase.Services.StaticData
{
  [CreateAssetMenu(fileName = "AbilityStaticData", menuName = "StaticData/AbilityStaticData")]
  public class AbilityStaticData : ScriptableObject
  {
    public AbilityID AbilityID;
    public AssetReferenceGameObject PrefabReference;
  }
}