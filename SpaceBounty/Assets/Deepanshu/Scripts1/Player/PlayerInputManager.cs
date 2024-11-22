using UnityEngine;
public class PlayerInputManager : MonoBehaviour
{
  public enum InputType
  {
    PlayerOne,
  }
  public static IMovement GetInputcontrols(InputType inputType)
  {
    return inputType switch
    {
      InputType.PlayerOne => new MovementControls(),
    };
  }
}
