using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public UnityEvent OnNavStarted;
    public UnityEvent OnNavInterupted;
    public UnityEvent OnInteractionStarted;
    public UnityEvent OnInteruptionStopped;
    private void OnMouseDown()
    {
        PlayerInteraction.INSTANCE.StartInteraction(this);
    }

    public void OnNavigationStarted()
    {
        this.OnNavStarted?.Invoke();
    }

    public void OnNavigationInterupted()
    {
        this.OnNavInterupted?.Invoke();
    }

    public void OnPlayerStartInteraction()
    {
        this.OnInteractionStarted?.Invoke();
    }

    public void OnPlayerStopInteraction()
    {
        this.OnInteruptionStopped?.Invoke();
    }
}
