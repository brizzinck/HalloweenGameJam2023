using System;
using CodeBase.Services.GameLoopService;
using UnityEngine;

namespace CodeBase.GameLoop
{
  public class GameLoop : MonoBehaviour
  {
    private IGameTimer _gameTimer;

    public void Construct(IGameTimer gameTimer)
    {
      _gameTimer = gameTimer;
      _gameTimer.UpdateTimeToZero();
    }

    private void Update()
    {
      _gameTimer.UpdateTimeToZero();
    }
  }
}