using System;

namespace CodeBase.Services.InteractiveObject
{
  public class Interactive : IInteractive
  {
    public void EventActionBraking(bool active, float start, float end) => 
      OnBraking?.Invoke(active, start, end);
    public event Action<bool, float, float> OnBraking;
  }
}