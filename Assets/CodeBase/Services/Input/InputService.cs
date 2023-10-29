using UnityEngine;

namespace CodeBase.Services.Input
{
  public abstract class InputService : IInputService
  {
    protected const string Horizontal = "Horizontal";
    protected const string Vertical = "Vertical";
    private const string InteractiveButton = "Interactive";
    private const string Abilities = "Abilities";
    private const string Guide = "Guide";
    
    public abstract Vector2Int Axis { get; }

    public bool PressInteractiveButton() => 
      SimpleInput.GetButtonDown(InteractiveButton);
    
    public bool PressAbilityButton() => 
      SimpleInput.GetButtonDown(Abilities);

    public bool PressGuideButton() => 
      SimpleInput.GetButtonDown(Guide);

    protected static Vector2Int SimpleInputAxisRaw() => 
      new Vector2Int((int)SimpleInput.GetAxisRaw(Horizontal), (int)SimpleInput.GetAxisRaw(Vertical));
  }
}