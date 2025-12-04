using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Interaction Settings")]
    [SerializeField] private float interactionDistance = 3f;
    [SerializeField] private LayerMask interactionLayer;

    [Header("UI References")]
    [SerializeField] private CrosshairController crosshairController;

    private Camera playerCamera;
    private bool isLookingAtInteractable = false;

    private void Start()
    {
        playerCamera = GetComponentInChildren<Camera>();
        if (playerCamera == null)
        {
            playerCamera = Camera.main;
        }
    }

    private void Update()
    {
        UpdateCrosshairState();
        CheckForInteraction();
    }

    private void UpdateCrosshairState()
    {
        if (playerCamera == null || crosshairController == null) return;

        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        InteractionType currentInteractionType = InteractionType.None;

        if (Physics.Raycast(ray, out hit, interactionDistance, interactionLayer))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();
            
            if (interactable == null)
            {
                interactable = hit.collider.GetComponentInParent<IInteractable>();
            }

            if (interactable != null)
            {
                currentInteractionType = interactable.CurrentInteractionType;
            }
        }

        // Update crosshair with the interaction type
        crosshairController.SetInteractionType(currentInteractionType);
    }

    private void CheckForInteraction()
    {
        // Use 'E' key for interaction by default using the new Input System directly
        // This avoids needing to modify the Input Actions asset immediately
        if (Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            PerformInteraction();
        }
        // Optional: Add mouse click support
        // if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        // {
        //     PerformInteraction();
        // }
    }

    private void PerformInteraction()
    {
        if (playerCamera == null) return;

        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionDistance, interactionLayer))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();
            
            // Also check parent if the collider is on a child object
            if (interactable == null)
            {
                interactable = hit.collider.GetComponentInParent<IInteractable>();
            }

            if (interactable != null)
            {
                interactable.Interact();
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (playerCamera != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(playerCamera.transform.position, playerCamera.transform.forward * interactionDistance);
        }
    }
}
