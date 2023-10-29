using UnityEngine;

namespace CodeBase.StaticData
{
  [CreateAssetMenu(fileName = "NPCSkinsBodyData", menuName = "StaticData/NPCSkinsBodyData")]
  public class DefaultNPCSkinsBodyData : ScriptableObject
  {
    public Sprite Head;
    public Sprite FrontEye;
    public Sprite BackEye;
    public Sprite Body;
    public Sprite Arm;
    public Sprite Leg;
  }
}