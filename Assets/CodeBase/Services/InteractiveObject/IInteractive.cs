using System;

namespace CodeBase.Services.InteractiveObject
{
  public interface IInteractive : IService
  {
    public event Action<bool, float, float> OnBraking;
    void EventActionBraking(bool active, float start, float end);
  }
}