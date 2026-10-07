using System;
using UnityEngine;
using UnityEngine.Playables;

[Serializable]
[CreateAssetMenu(fileName = "EventNode", menuName = "Scriptable Objects/EventNode")]
public class EventNode : StoryNode
{
    public string eventID;
    public PlayableAsset timelineAsset;

}
