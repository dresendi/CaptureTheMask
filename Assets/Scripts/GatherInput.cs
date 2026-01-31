using UnityEngine;
using UnityEngine.InputSystem;

public class GatherInput : MonoBehaviour
{
    public Vector2 Movement { get; private set; }

    // Este método lo llama PlayerInput
    public void OnMove(InputAction.CallbackContext context)
    {
        //Movement = context.ReadValue<Vector2>();
        Vector2 value = context.ReadValue<Vector2>();
        Movement = value;
    }
}
