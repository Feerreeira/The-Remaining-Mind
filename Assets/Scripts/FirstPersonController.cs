using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Controlador simples de movimento em primeira pessoa
/// </summary>
[RequireComponent(typeof(CharacterController))]
public class FirstPersonController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 3.5f;
    [SerializeField] private float gravity = -9.81f;
    
    // Components
    private CharacterController characterController;
    
    // Input
    private Vector2 moveInput;
    
    // Physics
    private Vector3 velocity;
    private bool isGrounded;
    
    void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }
    
    void Update()
    {
        HandleMovement();
        ApplyGravity();
    }
    
    void HandleMovement()
    {
        // Check if grounded
        isGrounded = characterController.isGrounded;
        
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Small negative value to keep grounded
        }
        
        // Get movement direction relative to player orientation (Local Space -> World Space)
        // x = right/left (strafe), z = forward/backward
        Vector3 moveDirection = transform.right * moveInput.x + transform.forward * moveInput.y;
        
        // Move the character
        characterController.Move(moveDirection * moveSpeed * Time.deltaTime);
    }
    
    void ApplyGravity()
    {
        // Apply gravity
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }
    
    // Input System callbacks
    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
}
