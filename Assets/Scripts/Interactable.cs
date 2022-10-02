using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public float arrivalDetectionDistance = 0.25f;

    private void OnMouseDown()
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
