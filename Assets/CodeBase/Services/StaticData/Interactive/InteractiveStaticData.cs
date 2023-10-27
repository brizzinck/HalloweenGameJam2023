using CodeBase.InteractiveObjects.Logic;
using UnityEngine.AddressableAssets;
using UnityEngine;

namespace CodeBase.Services.StaticData.Interactive
{
  [CreateAssetMenu(fileName = "InteractiveObjectData", menuName = "StaticData/InteractiveObject")]
  public class InteractiveStaticData : ScriptableObject
  {
    public InteractiveID InteractiveID;
    public AssetReferenceGameObject PrefabReference;
  }
}