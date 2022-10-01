using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quests", menuName = "ScriptableObjects/Quests")]
public class QuestList : ScriptableObject
{
    public List<Quest> quests;
}