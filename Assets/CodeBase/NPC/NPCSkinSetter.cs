using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.NPC
{
  public class NPCSkinSetter : MonoBehaviour
  {
    [SerializeField] private SpriteRenderer _head;
    [SerializeField] private SpriteRenderer[] _frontEyes;
    [SerializeField] private SpriteRenderer[] _backEyes;
    [SerializeField] private SpriteRenderer _body;
    [SerializeField] private SpriteRenderer[] _arms;
    [SerializeField] private SpriteRenderer[] _legs;
    
    public virtual void BaseConstruct(DefaultNPCSkinsBodyData skinsBodyData)
    {
      _head.sprite = skinsBodyData.Head;
      foreach (SpriteRenderer eye in _frontEyes) 
        eye.sprite = skinsBodyData.FrontEye;
      foreach (SpriteRenderer eye in _backEyes) 
        eye.sprite = skinsBodyData.BackEye;
      foreach (SpriteRenderer arm in _arms) 
        arm.sprite = skinsBodyData.Arm;
      foreach (SpriteRenderer leg in _legs) 
        leg.sprite = skinsBodyData.Leg;
      _body.sprite = skinsBodyData.Body;
    }
  }
}