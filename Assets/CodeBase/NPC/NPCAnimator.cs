using System;
using CodeBase.Abilities;
using CodeBase.Logic.Animation;
using UnityEngine;

namespace CodeBase.NPC
{
  public class NPCAnimator : MonoBehaviour, IAnimationStateReader
  {
    [SerializeField] private Animator _animator;
    private static readonly int IdealHash = Animator.StringToHash("Ideal");
    private static readonly int CirculationCauldronHash = Animator.StringToHash("CirculationCauldron");
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

    public void PlayIdeal() => 
      _animator.SetTrigger(IdealHash);
    
    public void PlayCirculationCauldron() => 
      _animator.SetTrigger(CirculationCauldronHash);
    
    private NPCAnimationState StateFor(int stateHash)
    {
      if (stateHash == IdealHash)
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
        default:
          Debug.LogError("Non exist animation");
          break;
      }
    }
  }
}