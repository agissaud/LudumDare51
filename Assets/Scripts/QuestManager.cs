using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public QuestList questDatas;

    public List<QuestInstance> QuestInstances { get; private set; }

    public event System.Action<int> OnQuestCompleted;

    void Awake()
    {
        if (QuestInstances == null)
        {
            QuestInstances = new List<QuestInstance>();
            QuestInteractable[] targets = GetComponentsInChildren<QuestInteractable>();

            Dictionary<ObjectType, List<QuestInteractable>> targetPerType = targets.GroupBy(qi => qi.targetType).ToDictionary(g => g.Key, g => g.ToList());

            for (int j = 0; j < questDatas.quests.Count; j++)
            {
                Quest q = questDatas.quests[j];
                QuestInstance qi = new QuestInstance(this, j, q);
                QuestInstances.Add(qi);
                for (int i = 0; i < q.parts.Count; i++)
                {
                    QuestPart qp = q.parts[i];
                    if (qp.targetType == ObjectType.NONE)
                    {
                        continue;
                    }

                    List<QuestInteractable> validTargets;
                    if (targetPerType.TryGetValue(qp.targetType, out validTargets) && validTargets.Count > 0)
                    {
                        int chosen = Random.Range(0, validTargets.Count);
                        validTargets[chosen].availableQuest = new QuestPartInstance(qi, i);
                        validTargets.RemoveAt(chosen);
                    }
                    else
                    {
                        Debug.LogWarning("Could not find a target QuestInteractable for quest part of type " + qp.targetType.ToString());
                    }
                }
            }
        }
    }

    public void OnQuestValidated(int questIndex)
    {
        OnQuestCompleted(questIndex);
    }
}
