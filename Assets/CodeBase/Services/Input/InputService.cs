using UnityEngine;

namespace CodeBase.Services.Input
{
  public abstract class InputService : IInputService
  {
    protected const string Horizontal = "Horizontal";
    protected const string Vertical = "Vertical";
    private const string InteractiveButton = "Interactive";
    private const string Abilities = "Abilities";
    
    public abstract Vector2Int Axis { get; }

    public bool PressInteractiveButton() => 
      SimpleInput.GetButtonDown(InteractiveButton);
    
    public bool PressAbilityButton() => 
      SimpleInput.GetButtonDown(Abilities);

    protected static Vector2 SimpleInputAxis() => 
      new Vector2(SimpleInput.GetAxisRaw(Horizontal), SimpleInput.GetAxisRaw(Vertical));
  }
}