using System;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class ChoiceOptionEffect
{
    public string effectName;
    public EffectType effectValueType;

    public bool effectBoolValue;
    public int effectIntValue;

    public enum EffectType
    {
        Bool,
        Int
    }
}
