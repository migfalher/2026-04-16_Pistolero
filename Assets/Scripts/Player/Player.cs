using UnityEngine;
using System.Collections.Generic;
using Context = UnityEngine.InputSystem.InputAction.CallbackContext;
using Phase = UnityEngine.InputSystem.InputActionPhase;

public class Player : MonoBehaviour
{
    // Public objects
    public GameObject bullet;
    public Camera mainCamera;

    // Attributes
    private CharacterController characterController;
    private Rigidbody rigidbody;
    private Transform bodyTransform;
    private Transform cannonTransform;
    private List<Transform> originsTransforms;

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
        if (bullet != null) { bullet.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f); }
        characterController = GetComponent<CharacterController>();
        rigidbody = GetComponent<Rigidbody>();
        bodyTransform = transform.GetChild(0).transform;
        cannonTransform = transform.GetChild(1).transform;
        originsTransforms = new List<Transform>();
        originsTransforms.Add(transform.GetChild(1).GetChild(0).transform);
        originsTransforms.Add(transform.GetChild(1).GetChild(1).transform);
    }

    // Update is called once per frame
    void FixedUpdate()
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

    public void ShootInput(Context c)
    {
        if (c.phase.Equals(Phase.Started))
        {
            // throw bullet(s)
            foreach (Transform origin in originsTransforms)
            {
                GameObject clone = GameObject.Instantiate(this.GetComponent<Player>().bullet, origin.position, Quaternion.identity);
                Rigidbody rb = clone.GetComponent<Rigidbody>();
                rb.AddForce((cannonTransform.forward * rb.mass * 10), ForceMode.Impulse);
            }
        }
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
