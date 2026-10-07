using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "DialogueNode", menuName = "Scriptable Objects/DialogueNode")]
public class DialogueNode : StoryNode
{
    public string characterName;
    public Sprite characterImage;
    public string dialogueText;
    public bool isPlayerSpeaking;
    public Color speakerColor;
}