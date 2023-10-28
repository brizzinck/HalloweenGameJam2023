using System;

namespace CodeBase.Services.Input
{
  public class DisplayInputService : IDisplayInputService
  {
    public event Action<bool> PressF;

    public event Action<bool> PressE;

    public void OnActionE(bool display) => 
      PressE?.Invoke(display);

    public void OnActionF(bool display) => 
      PressF?.Invoke(display);
  }
}