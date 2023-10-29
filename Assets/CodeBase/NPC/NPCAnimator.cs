using System;
using CodeBase.Abilities;
using CodeBase.Logic.Animation;
using UnityEngine;

namespace CodeBase.NPC
{
  public class NPCAnimator : MonoBehaviour, IAnimationStateReader
  {
    [SerializeField] private Animator _animator;
    private static readonly int IdleHash = Animator.StringToHash("Idle");
    private static readonly int CirculationCauldronHash = Animator.StringToHash("Cauldron");
    private static readonly int WhipHash = Animator.StringToHash("Whip");
    private static readonly int CauldronHash = Animator.StringToHash("Cauldron");
    private static readonly int IsWalkHash = Animator.StringToHash("IsWalk");
    private static readonly int IsAgroHsh = Animator.StringToHash("IsAgro");
    public event Action<NPCAnimationState> StateEntered;
    public event Action<NPCAnimationState> StateExited;
    public NPCAnimationState State { get; private set; }
    public void EnteredState(int stateHash)
    {
      State = StateFor(stateHash);
      StateEntered?.Invoke(State);
    }
    public void ExitedState(int stateHash) => 
      StateExited?.Invoke(StateFor(stateHash));

    public void SetIdle() => 
      _animator.SetTrigger(IdleHash);
    public void SetCauldron() => 
      _animator.SetTrigger(CauldronHash);
    public void SetWhip() => 
      _animator.SetTrigger(WhipHash);
    public void SetWalk(bool walk) => 
      _animator.SetBool(IsWalkHash, walk);
    public void SetAgro(bool agro) => 
      _animator.SetBool(IsAgroHsh, agro);
    
    private void PlayCirculationCauldron() => 
      _animator.SetTrigger(CirculationCauldronHash);
    private void PlayWhip() => 
      _animator.SetTrigger(WhipHash);
    
    private NPCAnimationState StateFor(int stateHash)
    {
      if (stateHash == IdleHash)
        return NPCAnimationState.Ideal;
      else if (stateHash == CirculationCauldronHash)
        return NPCAnimationState.CirculationCauldron;
      return NPCAnimationState.Unknown;
    }

    public void PlayTorture(AbilityID abilityID)
    {
      switch (abilityID)
      {
        case AbilityID.Cauldron:
          PlayCirculationCauldron();
          break;
        case AbilityID.Stitch:
          PlayWhip();
          break;
        default:
          Debug.LogError("Non exist animation");
          break;
      }
    }
  }
}