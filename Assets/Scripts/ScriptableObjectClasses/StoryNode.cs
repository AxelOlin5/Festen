using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "StoryNode", menuName = "Scriptable Objects/StoryNode")]
public class StoryNode : ScriptableObject
{
    public StoryNode nextNode;
}
