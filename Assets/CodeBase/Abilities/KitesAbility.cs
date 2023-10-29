using CodeBase.NPC;
using UnityEngine;

namespace CodeBase.Abilities
{
  public class KitesAbility : BaseAbility
  {
    [SerializeField] private Animator KitesAnimator;
    [SerializeField] private GameObject[] FlyAnimator;
    private static readonly int IsKitesHash = Animator.StringToHash("IsKites");
    protected override void EnterTorture(NPCWithSoul npcWithSoul)
    {
      base.EnterTorture(npcWithSoul);
      KitesAnimator.SetTrigger(IsKitesHash);
      foreach (GameObject animator in FlyAnimator) 
        animator.SetActive(true);
    }

    protected override void ExitTorture()
    {
      base.ExitTorture();
      foreach (GameObject animator in FlyAnimator) 
        animator.SetActive(true);
    }
  }
}