using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "ChoiceOption", menuName = "Scriptable Objects/ChoiceOption")]
public class ChoiceOption : ScriptableObject
{
    public string choiceText;
    public List<ChoiceOptionRequirement> requirements; 
    public List<ChoiceOptionEffect> effects; 

    public StoryNode nextNode;
}