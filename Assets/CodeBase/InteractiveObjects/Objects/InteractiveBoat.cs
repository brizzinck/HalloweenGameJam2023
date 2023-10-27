using CodeBase.InteractiveObjects.Base;
using UnityEngine;

public class InteractiveBoat : BaseInteractiveObject
{
  protected override void OpenInteractiveWindow()
  {
    Debug.LogError("PressInteractiveButton => f/e");
  }
}
