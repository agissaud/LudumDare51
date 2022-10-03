using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Quest
{
    public string text = "";
    public List<QuestPart> parts = new List<QuestPart>();
}

public enum ObjectType
{
    CHARACTER,
    GLOBE,
    PC,
    NONE,
    SANDWICH1,
    SANDWICH2,
    PLANT
}

[Serializable]
public class QuestPart
{
    public ObjectType targetType = ObjectType.CHARACTER;
    public Dialog dialog;
    //public Item requiredItem;
    //public Item rewardedItem;
}

public class QuestInstance
{
    private QuestManager manager;
    private int index;
    private Quest data;
    private int progress = 0;

    public QuestInstance(QuestManager manager, int index, Quest data)
    {
        this.manager = manager;
        this.index = index;
        this.data = data;
    }

    public Quest Data => this.data;

    public bool Complete => this.progress >= this.data.parts.Count;

    public QuestPart GetPartData(int partIndex) => this.data.parts[partIndex];

    public bool IsPartActive(int partIndex) => this.progress >= partIndex;

    public void ValidatePart(int partIndex)
    {
        if (this.progress == partIndex)
        {
            this.progress++;
            if (this.Complete)
            {
                this.manager.OnQuestValidated(this.index);
            }
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
