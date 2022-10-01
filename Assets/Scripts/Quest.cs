using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Quest
{
    public List<QuestPart> parts;
}

public enum ObjectType
{
    CHARACTER,
    GLOBE
}

[Serializable]
public class QuestPart
{
    public ObjectType targetType;
    public Dialog dialog;
    public Item requiredItem;
    public Item rewardedItem;
}

public class QuestInstance
{
    private Quest data;
    private int progress = 0;

    public QuestInstance(Quest data)
    {
        this.data = data;
    }

    public bool Complete => this.progress >= this.data.parts.Count;

    public QuestPart GetPartData(int partIndex) => this.data.parts[partIndex];

    public bool IsPartActive(int partIndex) => this.progress >= partIndex;

    public void ValidatePart(int partIndex)
    {
        if (this.progress == partIndex)
        {
            this.progress++;
        }
    }
}

public class QuestPartInstance
{
    private QuestInstance quest;
    private int index;

    public QuestPartInstance(QuestInstance quest, int index)
    {
        this.quest = quest;
        this.index = index;
    }

    public QuestPart Data => this.quest.GetPartData(this.index);

    public bool Active => this.quest.IsPartActive(this.index);

    public void Validate() => this.quest.ValidatePart(this.index);
}
