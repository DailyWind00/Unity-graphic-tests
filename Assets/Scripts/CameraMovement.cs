using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float cameraSpeed = 2.5f;
    public float rotationSpeed = 12.5f;

    private CameraControls controls;
    private Vector2 moveInput;
    private float verticalInput;
    private Vector2 lookInput;
    private bool isBoosting;
    private bool isRotating;

    private Vector2 currentRotation;

    void Awake()
    {
        controls = new CameraControls();

        controls.Camera.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Camera.Move.canceled += ctx => moveInput = Vector2.zero;

        controls.Camera.UpDown.performed += ctx => verticalInput = ctx.ReadValue<float>();
        controls.Camera.UpDown.canceled += ctx => verticalInput = 0;

        controls.Camera.Look.performed += ctx => lookInput = ctx.ReadValue<Vector2>();
        controls.Camera.Look.canceled += ctx => lookInput = Vector2.zero;

        controls.Camera.Boost.performed += ctx => isBoosting = true;
        controls.Camera.Boost.canceled += ctx => isBoosting = false;

        controls.Camera.Rotate.performed += ctx => isRotating = true;
        controls.Camera.Rotate.canceled += ctx => isRotating = false;
    }

    void OnEnable() => controls.Enable();
    void OnDisable() => controls.Disable();

    void Start()
    {
        transform.position = new Vector3(0, 3, -30);
        currentRotation = Vector2.zero;
    }

    void Update()
    {
        HandleCameraMovement();
        HandleCameraRotation();
    }

    private void HandleCameraMovement()
    {
        float speed = cameraSpeed * Time.deltaTime;
        if (isBoosting) speed *= 3f;

        Vector3 forward = new Vector3(transform.forward.x, 0, transform.forward.z).normalized;
        Vector3 right = new Vector3(transform.right.x, 0, transform.right.z).normalized;
        Vector3 up = Vector3.up;

        transform.position += forward * moveInput.y * speed;
        transform.position += right * moveInput.x * speed;
        transform.position += up * verticalInput * speed;
    }

    private void HandleCameraRotation()
    {
        if (!isRotating) return;

        float speed = rotationSpeed * Time.deltaTime;

        currentRotation.x -= lookInput.y * speed;
        currentRotation.y += lookInput.x * speed;

        currentRotation.x = Mathf.Clamp(currentRotation.x, -90f, 90f);

        transform.rotation = Quaternion.Euler(currentRotation.x, currentRotation.y, 0f);
    }
}
