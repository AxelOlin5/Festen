using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.LightTransport;
using UnityEngine.UIElements;

[Serializable]
[CreateAssetMenu(fileName = "StoryNode", menuName = "Scriptable Objects/StoryNode")]
public class StoryNode : ScriptableObject
{
    public StoryNode nextNode;
}
