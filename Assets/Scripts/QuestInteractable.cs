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
            // Show quest dialog
            DialogManager.Instance.PopUp(this.availableQuest.Data.dialog);
            this.availableQuest.Validate();
        }
        else
        {
            // Show default dialog
            DialogManager.Instance.PopUp(this.defaultDialog);
        }
    }
}


