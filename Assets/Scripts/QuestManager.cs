using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public QuestList questDatas;

    private List<QuestInstance> questInstances;

    void Start()
    {
        QuestInteractable[] targets = GetComponentsInChildren<QuestInteractable>();

        Dictionary<ObjectType, List<QuestInteractable>> targetPerType = targets.GroupBy(qi => qi.targetType).ToDictionary(g => g.Key, g => g.ToList());

        foreach (Quest q in questDatas.quests)
        {
            QuestInstance qi = new QuestInstance(q);
            questInstances.Add(qi);
            for (int i = 0; i < q.parts.Count; i++)
            {
                QuestPart qp = q.parts[i];
                List<QuestInteractable> validTargets = targetPerType[qp.targetType];
                int chosen = Random.Range(0, validTargets.Count);
                validTargets[chosen].availableQuest = new QuestPartInstance(qi, i);
                validTargets.RemoveAt(chosen);
            }
        }
    }
}
