using System;
using CodeBase.Constants;
using CodeBase.Infrastructure.States;
using UnityEngine;

namespace CodeBase.Services.GameLoopService
{
  public class GameTimer : IGameTimer
  {
    public event Action<float> OnUpdateTime;
    public event Action OnEndGame;
    public float CurrentTime { get; set; }
    
    private readonly IGameStateMachine _stateMachine;
    public GameTimer(IGameStateMachine stateMachine)
    {
      _stateMachine = stateMachine;
    }

    public void UpdateTimeToZero()
    {
      if (CurrentTime > 0)
      {
        CurrentTime -= Time.deltaTime;
        if (CurrentTime <= 0)
        {
          CurrentTime = 0;
          _stateMachine.Enter<LoadEndMenuState, string>(ConstantsValue.EndMenu);
          OnEndGame?.Invoke();
        }
        OnUpdateTime?.Invoke(CurrentTime);
      }
    }
  }
}