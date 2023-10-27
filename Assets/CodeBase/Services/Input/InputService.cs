using UnityEngine;

namespace CodeBase.Services.Input
{
  public abstract class InputService : IInputService
  {
    protected const string Horizontal = "Horizontal";
    protected const string Vertical = "Vertical";
    private const string InteractiveButton = "Interactive";

    public abstract Vector2 Axis { get; }

    public bool PressInteractiveButton() => 
      SimpleInput.GetButtonDown(InteractiveButton);

    protected static Vector2 SimpleInputAxis() => 
      new Vector2(SimpleInput.GetAxis(Horizontal), SimpleInput.GetAxis(Vertical));
  }
}