using UnityEngine;

namespace CodeBase.Services.StaticData
{
  [CreateAssetMenu(fileName = "GameStaticData", menuName = "StaticData/GameStaticData")]
  public class GameStaticData : ScriptableObject
  {
    public GameTempData GameTempData;
  }
}