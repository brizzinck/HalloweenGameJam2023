using System;
using CodeBase.Constants;
using CodeBase.Infrastructure.States;
using CodeBase.Services.StaticData;
using UnityEngine;

namespace CodeBase.Services.GameLoopService
{
  public class GameTimer : IGameTimer
  {
    public event Action<float> OnUpdateTime;
    public event Action OnEndGame;
    public float CurrentTime { get; set; }
    
    private readonly IGameStateMachine _stateMachine;
    private readonly IStaticDataService _staticData;

    public GameTimer(IGameStateMachine stateMachine, IStaticDataService staticData)
    {
      _stateMachine = stateMachine;
      _staticData = staticData;
      CurrentTime = staticData.GameTempData.TimeToEnd;
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