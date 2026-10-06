using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "ChoiceOption", menuName = "Scriptable Objects/ChoiceOption")]
public class ChoiceOption : ScriptableObject
{
    public string choiceText;
    public Dictionary<string, bool> requirements;
    public Dictionary<string, bool> effects;

    public StoryNode nextNode;
}