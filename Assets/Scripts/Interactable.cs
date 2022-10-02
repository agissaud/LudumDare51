using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Interactable : MonoBehaviour, IPointerDownHandler
{
    public float arrivalDetectionDistance = 0.25f;

    public void OnPointerDown(PointerEventData eventData)
    {
        PlayerInteraction.INSTANCE.StartInteraction(this);
    }

    public virtual void OnNavigationStarted()
    { }

    public virtual void OnNavigationInterupted()
    { }

    public abstract void OnPlayerStartInteraction();

    public virtual void OnPlayerStopInteraction()
    { }
}
