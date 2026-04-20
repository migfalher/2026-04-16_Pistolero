using UnityEngine;
using Context = UnityEngine.InputSystem.InputAction.CallbackContext;
using Phase = UnityEngine.InputSystem.InputActionPhase;

public class Player : MonoBehaviour
{
    // Public objects
    public Camera mainCamera;

    // Attributes
    private CharacterController characterController;
    private Transform bodyTransform;
    private Transform cannonTransform;

    // Private variables
    private float walkSpeed = 5.0f;
    private float rotationSpeed = 180.0f;
    private float gravitySpeed = 9.8f;
    private float bodyAngle = 0.0f;
    private float cannonAngle = 0.0f;
    private Vector3 move;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        bodyTransform = transform.GetChild(0).transform;
        cannonTransform = transform.GetChild(1).transform;
    }

    // Update is called once per frame
    void Update()
    {
        MoveAction(move * Time.deltaTime);
        RotateAction(cannonAngle * Time.deltaTime);
    }

    // Simulate Gravity
    private float SimulateGravity()
    {
        float verticalVelocity = (characterController.isGrounded ? -1 : Mathf.Abs(gravitySpeed - 1) * -1);
        return verticalVelocity;
    }

    // Input methods attached to unity events
    public void MoveInput(Context c)
    {
        Vector2 input = c.ReadValue<Vector2>().normalized;
        if (!input.Equals(Vector2.zero))
        {
            bodyAngle = Mathf.Atan2(input.x, input.y) * Mathf.Rad2Deg;
        }
        move = (new Vector3(input.x, SimulateGravity(), input.y)) * walkSpeed;
        bodyTransform.rotation = Quaternion.Euler(new Vector3(0, bodyAngle, 0));
    }

    public void RotateInput(Context c)
    {
        float input = c.ReadValue<float>();
        cannonAngle = rotationSpeed * input;
    }

    // Action methods that respond to input methods
    private void MoveAction(Vector3 move)
    {
        characterController.Move(move);
    }

    private void RotateAction(float rotation)
    {
        Vector3 rotationVector = new Vector3(0, rotation, 0);
        cannonTransform.Rotate(rotationVector);
    }
}
