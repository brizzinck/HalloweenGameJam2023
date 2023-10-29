using System;
using CodeBase.Services.GameLoopService;
using UnityEngine;

namespace CodeBase.GameLoop
{
  public class GameLoop : MonoBehaviour
  {
    [SerializeField] private float _timeToPlay;
    private IGameTimer _gameTimer;

    public void Construct(IGameTimer gameTimer)
    {
      _gameTimer = gameTimer;
      _gameTimer.CurrentTime = _timeToPlay;
      _gameTimer.UpdateTimeToZero();
    }

    private void Update()
    {
      _gameTimer.UpdateTimeToZero();
    }
  }
}