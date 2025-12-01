using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Controlador de câmara em primeira pessoa
/// </summary>
public class FirstPersonCamera : MonoBehaviour
{
    [Header("Camera Settings")]
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float mouseSensitivity = 0.1f; // Reduced from 2f
    [SerializeField] private float gamepadSensitivity = 100f;
    
    [Header("Look Constraints")]
    [SerializeField] private float minPitch = -60f;
    [SerializeField] private float maxPitch = 60f;
    
    // Input
    private Vector2 lookInput;
    
    // Rotation
    private float pitch = 0f;
    
    void Start()
    {
        // Lock and hide cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        // If cameraTransform not assigned, try to find it in children
        if (cameraTransform == null)
        {
            cameraTransform = GetComponentInChildren<Camera>()?.transform;
        }
    }
    
    void Update()
    {
        HandleCameraRotation();
    }
    
    void HandleCameraRotation()
    {
        if (cameraTransform == null) return;
        
        // Simple sensitivity application
        // Note: For gamepad, you might want to multiply by Time.deltaTime, but for mouse delta it's usually not needed
        // If supporting both seamlessly is hard, we can rely on Input System Processors to scale input
        float mouseX = lookInput.x * mouseSensitivity;
        float mouseY = lookInput.y * mouseSensitivity;
        
        // Rotate player body horizontally (yaw) - THIS object
        transform.Rotate(Vector3.up * mouseX);
        
        // Rotate camera vertically (pitch) - CAMERA object
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
        
        cameraTransform.localRotation = Quaternion.Euler(pitch, 0f, 0f);
    }
    
    // Input System callbacks
    public void OnLook(InputValue value)
    {
        lookInput = value.Get<Vector2>();
    }
}
