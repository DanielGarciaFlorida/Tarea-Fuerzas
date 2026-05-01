using UnityEngine;
using UnityEngine.InputSystem;

public class Cohete : MonoBehaviour
{
    public float thrustForce = 300000f;
    private float currentMass;
    private float acceleration;

    private Rigidbody rb;
    private PlayerInputActions inputActions;
    private bool isThrusting = false;

    void Awake()
    {
        inputActions = new PlayerInputActions();

    }

    void OnEnable()
    {
        inputActions.Gameplay.Shoot.performed += ToggleThrust;
    }

    void OnDisable()
    {
        inputActions.Gameplay.Shoot.performed -= ToggleThrust;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        inputActions.Gameplay.Enable();

        currentMass = rb.mass;
        if (currentMass > 0)
        {
            acceleration = thrustForce / currentMass;
        }
    }

    void FixedUpdate()
    {
        if (isThrusting)
        {
            rb.AddForce(Vector3.up * acceleration, ForceMode.Acceleration);

        }
    }
    void ToggleThrust(InputAction.CallbackContext context)
    {
        isThrusting = !isThrusting;
    }
}
