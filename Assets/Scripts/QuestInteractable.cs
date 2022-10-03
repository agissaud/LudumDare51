using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class QuestInteractable : Interactable
{
    public ObjectType targetType;
    public int maxQuestAssigned = 1;
    public List<Dialog> defaultDialogs;
    public List<QuestPartInstance> availableQuests = new List<QuestPartInstance>();

    public override void OnPlayerStartInteraction()
    {
        foreach (QuestPartInstance qi in availableQuests)
        {
            if (qi.Active)
            {
                // Show quest dialog
                DialogManager.Instance.PopUp(qi.Data.dialog);
                qi.Validate();
                return;
            }
        }

        if (defaultDialogs.Count != 0)
        {
            // Show default dialog
            DialogManager.Instance.PopUp(this.defaultDialog());
        }
    }

    private Dialog defaultDialog()
    {
        int nbDialog = defaultDialogs.Count;
        int selected = Random.Range(0, nbDialog);
        return defaultDialogs[selected];
    }
}


