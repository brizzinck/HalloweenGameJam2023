using CodeBase.NPC;
using UnityEngine;

namespace CodeBase.Abilities
{
  public class Stitch : BaseAbility
  {
    [SerializeField] private Animator _animator;
    private static readonly int StickHash = Animator.StringToHash("Stick");
    protected override void EnterTorture(NPCWithSoul npcWithSoul)
    {
      base.EnterTorture(npcWithSoul);
      _animator.SetTrigger(StickHash);
    }
  }
}