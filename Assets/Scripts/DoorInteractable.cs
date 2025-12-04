using UnityEngine;
using System.Collections;

public class DoorInteractable : MonoBehaviour, IInteractable
{
    [Header("Door Settings")]
    [SerializeField] private Transform doorPivot; // The "porta" object that rotates
    [SerializeField] private float openAngle = 90f;
    [SerializeField] private float animationDuration = 1.0f;
    [SerializeField] private AnimationCurve animationCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    private bool isOpen = false;
    private bool isAnimating = false;
    private Quaternion closedRotation;
    private Quaternion openRotation;

    // IInteractable implementation
    public InteractionType CurrentInteractionType
    {
        get
        {
            return isOpen ? InteractionType.Alternative : InteractionType.Default;
        }
    }

    private void Start()
    {
        if (doorPivot == null)
        {
            // Try to find child named "porta" if not assigned
            Transform childPorta = transform.Find("porta");
            if (childPorta != null)
            {
                doorPivot = childPorta;
            }
            else
            {
                // Fallback to this transform if no specific pivot found (though user specified "porta" rotates)
                doorPivot = transform;
            }
        }

        // Initialize rotations
        // Use the current rotation as the closed state (respects Unity rotation)
        closedRotation = doorPivot.localRotation;
        
        // Calculate open rotation by adding openAngle to the current Y rotation
        Vector3 currentEuler = doorPivot.localRotation.eulerAngles;
        openRotation = Quaternion.Euler(currentEuler.x, currentEuler.y + openAngle, currentEuler.z);
    }

    public void Interact()
    {
        if (isAnimating) return;

        isOpen = !isOpen;
        StartCoroutine(AnimateDoor(isOpen ? openRotation : closedRotation));


    }

    private IEnumerator AnimateDoor(Quaternion targetRotation)
    {
        isAnimating = true;
        Quaternion startRotation = doorPivot.localRotation;
        float timeElapsed = 0f;

        while (timeElapsed < animationDuration)
        {
            timeElapsed += Time.deltaTime;
            float t = timeElapsed / animationDuration;
            float curveValue = animationCurve.Evaluate(t);

            doorPivot.localRotation = Quaternion.Lerp(startRotation, targetRotation, curveValue);
            yield return null;
        }

        doorPivot.localRotation = targetRotation;
        isAnimating = false;
    }
}
