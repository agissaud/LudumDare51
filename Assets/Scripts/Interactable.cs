using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    private void OnMouseDown()
    {
        PlayerInteraction.INSTANCE.StartInteraction(this);
    }

    public void OnNavigationStarted()
    { }

    public void OnNavigationInterupted()
    { }

    public abstract void OnPlayerStartInteraction();

    public void OnPlayerStopInteraction()
    { }
}
