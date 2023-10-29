using System;

namespace CodeBase.Services.Input
{
  public interface IDisplayInputService : IService
  {
    public void OnActionF(bool display);
    public void OnActionE(bool display);
    public event Action<bool> PressF;
    public event Action<bool> PressE;
  }
}