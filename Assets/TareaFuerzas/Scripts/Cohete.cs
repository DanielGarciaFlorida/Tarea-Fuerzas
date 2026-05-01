using UnityEngine;
using UnityEngine.InputSystem;

public class Cohete : MonoBehaviour
{
    public float force = 300000f;

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
    }

    void FixedUpdate()
    {
        if (isThrusting)
        {
            rb.AddForce(Vector3.up * force, ForceMode.Force);

        }
    }
    void ToggleThrust(InputAction.CallbackContext context)
    {
        isThrusting = !isThrusting;
    }
}
