using System;

namespace CodeBase.Services.GameLoopService
{
  public interface IGameTimer : IService
  {
    event Action<float> OnUpdateTime;
    float CurrentTime { get; set; }
    void UpdateTimeToZero();
    event Action OnEndGame;
  }
}