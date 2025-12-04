using UnityEngine;
using UnityEngine.UI;

public class CrosshairController : MonoBehaviour
{
    [Header("Crosshair Sprites")]
    [SerializeField] private Sprite idleSprite;
    [SerializeField] private Sprite interactSprite;
    [SerializeField] private Sprite interactAlternativeSprite;

    private Image crosshairImage;

    private void Awake()
    {
        crosshairImage = GetComponent<Image>();
        
        if (crosshairImage == null)
        {
            Debug.LogError("CrosshairController: No Image component found on this GameObject!");
        }

        // Set initial sprite
        SetInteractionType(InteractionType.None);
    }

    public void SetInteractionType(InteractionType type)
    {
        if (crosshairImage == null) return;

        switch (type)
        {
            case InteractionType.None:
                crosshairImage.sprite = idleSprite;
                break;
            case InteractionType.Default:
                crosshairImage.sprite = interactSprite;
                break;
            case InteractionType.Alternative:
                crosshairImage.sprite = interactAlternativeSprite;
                break;
        }
    }
}
