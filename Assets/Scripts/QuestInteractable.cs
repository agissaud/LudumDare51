using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class QuestInteractable : MonoBehaviour
{
    public ObjectType targetType;
    public string defaultDialog;
    public QuestPartInstance availableQuest = null;

    void Reset()
    {
        GetComponent<Interactable>().OnInteractionStarted.RemoveListener(OnInteraction);
        GetComponent<Interactable>().OnInteractionStarted.AddListener(OnInteraction);
    }

    private void OnInteraction()
    {
        if (this.availableQuest != null && this.availableQuest.Active)
        {
            // Showw quest dialog
            this.availableQuest.Validate();
        }
        else
        {
            // Show default dialog
        }
    }
}


