using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public static PlayerInteraction INSTANCE { get; private set; } = null;
    private Interactable currentInteraction = null;
    private bool interacting = false;
    public PlayerMovement PlayerMovement { get; private set; }

    private void Awake()
    {
        INSTANCE = this;
    }

    private void Start()
    {
        PlayerMovement = GetComponent<PlayerMovement>();
    }

    public void StartInteraction(Interactable i)
    {
        if (this.currentInteraction == i)
            return;

        StopInteraction();
        this.currentInteraction = i;
        this.interacting = false;
        this.PlayerMovement.moveToDestionation(this.currentInteraction);
        this.PlayerMovement.arrivalDetectionThreshold = this.currentInteraction.arrivalDetectionDistance;
        this.currentInteraction.OnNavigationStarted();
    }

    public void StopInteraction()
    {
        StopInteraction(this.currentInteraction);
    }

    public void StopInteraction(Interactable i)
    {
        if (this.currentInteraction != i)
            return;

        if (this.currentInteraction == null)
            return;

        if (this.interacting)
        {
            this.currentInteraction.OnPlayerStopInteraction();
            this.interacting = false;
        }
        else
        {
            this.currentInteraction.OnNavigationInterupted();
        }
        this.currentInteraction = null;
    }

    public void OnNavidationTargetReached(Interactable i)
    {
        if (i == this.currentInteraction && !this.interacting)
        {
            this.currentInteraction.OnPlayerStartInteraction();
            this.interacting = true;
        }
    }
}
