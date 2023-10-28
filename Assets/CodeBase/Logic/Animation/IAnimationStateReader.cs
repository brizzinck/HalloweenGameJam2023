using System;
using CodeBase.NPC;

namespace CodeBase.Logic.Animation
{
  public interface IAnimationStateReader
  {
    public event Action<NPCAnimationState> StateEntered;
    public event Action<NPCAnimationState> StateExited;
    void EnteredState(int stateHash);
    void ExitedState(int stateHash);
    NPCAnimationState State { get; }
  }
}