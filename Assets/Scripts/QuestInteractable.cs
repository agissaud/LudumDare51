using UnityEngine;
using System.Collections.Generic;

public class QuestInteractable : Interactable
{
    public ObjectType targetType;
    public List<Dialog> defaultDialogs;
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
            if (defaultDialogs.Count != 0)
            {
                // Show default dialog
                DialogManager.Instance.PopUp(this.defaultDialog());
            }
        }
    }

    private Dialog defaultDialog()
    {
        int nbDialog = defaultDialogs.Count;
        int selected = Random.Range(0, nbDialog);
        return defaultDialogs[selected];
    }
}


