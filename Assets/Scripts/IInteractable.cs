using UnityEngine;

public interface IInteractable
{
    void Interact();
    InteractionType CurrentInteractionType { get; }
}
