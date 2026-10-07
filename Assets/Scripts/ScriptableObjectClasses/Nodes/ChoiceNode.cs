using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "ChoiceNode", menuName = "Scriptable Objects/ChoiceNode")]
public class ChoiceNode : StoryNode
{
    public string choiceDialogueText;

    public List<ChoiceOption> choiceOptions = new List<ChoiceOption>();
}
