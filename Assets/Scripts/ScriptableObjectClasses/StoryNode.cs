using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.LightTransport;
using UnityEngine.UIElements;

[Serializable]
[CreateAssetMenu(fileName = "StoryNode", menuName = "Scriptable Objects/StoryNode")]
public class StoryNode : ScriptableObject
{
}

[Serializable]
[CreateAssetMenu(fileName = "DialogueNode", menuName = "Scriptable Objects/DialogueNode")]
public class DialogueNode : StoryNode
{
    public string characterName;
    public Sprite characterImage;
    public string dialogueText;
    StoryNode nextNode;
}

[Serializable]
[CreateAssetMenu(fileName = "ChoiceNode", menuName = "Scriptable Objects/ChoiceNode")]
public class ChoiceNode : StoryNode
{
    public string choiceDialogueText;

    public List<ChoiceOption> choiceOptions = new List<ChoiceOption>();
}

[Serializable]
[CreateAssetMenu(fileName = "ChoiceOption", menuName = "Scriptable Objects/ChoiceOption")]
public class ChoiceOption : ScriptableObject
{
    public string choiceText;
    public Dictionary<string, bool> requirements;
    public Dictionary<string, bool> effects;
    StoryNode nextNode;

}

[Serializable]
[CreateAssetMenu(fileName = "EventNode", menuName = "Scriptable Objects/EventNode")]
public class EventNode : StoryNode
{
    public string eventID;
    StoryNode nextNode;

    public void TriggerEvent()
    {
        // Implement event triggering logic here
    }
}
