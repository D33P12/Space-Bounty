using UnityEngine;
public class PlayerMovementInput : MonoBehaviour
{
  [SerializeField]
  PlayerInputManager.InputType _inputType = PlayerInputManager.InputType.PlayerOne;
  public IMovement MovementControls { get; private set; }
  private void Start()
  {
    MovementControls = PlayerInputManager.GetInputcontrols(_inputType);
  }
  void OnMove()
  {
    MovementControls = null;
  }
}
