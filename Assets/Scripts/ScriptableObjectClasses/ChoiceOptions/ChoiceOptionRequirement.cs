using System;
using UnityEngine;
using static ChoiceOptionEffect;

[Serializable]
public class ChoiceOptionRequirement
{
    public string requirementName;
    public RequirementType requirementValueType;

    public bool requirementBoolValue;
    public int requirementIntValue;

    public enum RequirementType
    {
        Bool,
        Int
    }
}
