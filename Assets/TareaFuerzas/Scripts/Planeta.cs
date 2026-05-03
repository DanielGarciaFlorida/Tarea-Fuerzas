using UnityEngine;
using UnityEngine.InputSystem;

public class Planeta : MonoBehaviour
{
    private float G = 6.67430e-11f; // Constante de gravitación universal
    public float planetMass; // Masa del planeta, M2
    public float playerMass; // Masa del habitante, M1
    public Transform planetTransform;

    private Rigidbody rb;
    private PlayerInputActions inputActions;
    private bool isGravitating = false;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }
    void OnEnable()
    {
        inputActions.Gameplay.Shoot.performed += ToggleGravity;
    }

    void OnDisable()
    {
        inputActions.Gameplay.Shoot.performed -= ToggleGravity;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        inputActions.Gameplay.Enable();

        playerMass = rb.mass;
        rb.isKinematic = true;
    }
    void FixedUpdate()
    {
        if (isGravitating && planetTransform != null)
        {
            ApplyGravitationalForce();
        }
    }

    void ApplyGravitationalForce()
    {
        //1. Calcular la distancia entre el planeta y el habitante
        Vector3 direction = planetTransform.position - transform.position;
        float r = direction.magnitude;
        //2. Calcular la fuerza gravitacional usando la fórmula F = G * (M1 * M2) / r^2, siendo M1 la masa del habitante (rb.mass) y M2 la masa del planeta
        float forceMagnitude = G * (rb.mass * planetMass) / (r * r);
        //3. Aplicar la fuerza al habitante en la dirección del planeta
        Vector3 forceV = direction.normalized * forceMagnitude;
        rb.AddForce(forceV, ForceMode.Force);
    }

    void ToggleGravity(InputAction.CallbackContext context)
    {
        isGravitating = !isGravitating;
        if (isGravitating)
        {
            rb.isKinematic = false;
        }
        else 
        {
            rb.isKinematic = true;
        }   
       
    }
}
