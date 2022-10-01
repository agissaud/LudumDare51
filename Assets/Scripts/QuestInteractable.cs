using UnityEngine;

public class QuestInteractable : Interactable
{
    public ObjectType targetType;
    public Dialog defaultDialog;
    public QuestPartInstance availableQuest = null;

    public override void OnPlayerStartInteraction()
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


