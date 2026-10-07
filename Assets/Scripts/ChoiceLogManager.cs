using NUnit.Framework.Internal;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ChoiceLogManager : MonoBehaviour
{
    public bool broughtAlcohol;
    public bool talkedToEmma;
    public bool joinedTheDrinkingGame;

    public bool isPlayerDrunk;

    public string playerName;

    private int alcoholConsumed = 0;

    public void ApplyEffect(ChoiceOption choiceOption)
    {
        if(choiceOption.effects == null || choiceOption.effects.Count == 0)
        {
            Debug.LogWarning("No effects found for the choice option: " + choiceOption.name);
            return;
        }

        foreach (var effect in choiceOption.effects)
        {
            if (effect.effectValueType == ChoiceOptionEffect.EffectType.Bool)
            {
                switch (effect.effectName)
                {
                    case "broughtAlcohol":
                        broughtAlcohol = effect.effectBoolValue;
                        break;

                    case "talkedToEmma":
                        talkedToEmma = effect.effectBoolValue;
                        break;

                    case "joinedTheDrinkingGame":
                        joinedTheDrinkingGame = effect.effectBoolValue;
                        break;

                    default:
                        Debug.LogWarning("Unknown effect name: " + effect.effectName);
                        break;
                }
            }
            else if (effect.effectValueType == ChoiceOptionEffect.EffectType.Int)
            {
                switch (effect.effectName)
                {
                    case "alcoholConsumed":
                    IncreaseAlcoholConsumed(effect.effectIntValue);
                    CheckIfPlayerIsDrunk();
                    break;

                default:
                    Debug.LogWarning("Unknown effect name: " + effect.effectName);
                    break;
                }
            }
        }
    }

    public bool CheckRequirements(ChoiceOption choiceOption)
    {
        if (choiceOption.requirements == null || choiceOption.requirements.Count == 0)
        {
            Debug.LogWarning("No requirements found for the choice option: " + choiceOption.name);
            return true;
        }

        foreach (var requirement in choiceOption.requirements)
        {
            if (requirement.requirementValueType == ChoiceOptionRequirement.RequirementType.Bool)
            {
                switch (requirement.requirementName)
                {
                    case "broughtAlcohol":
                        if (broughtAlcohol != requirement.requirementBoolValue)
                        {
                            return false;
                        }
                        break;

                    case "talkedToEmma":
                        if (talkedToEmma != requirement.requirementBoolValue)
                        {
                            return false;
                        }
                        break;

                    case "joinedTheDrinkingGame":
                        if (joinedTheDrinkingGame != requirement.requirementBoolValue)
                        {
                            return false;
                        }
                        break;

                    case "isPlayerDrunk":
                        if (isPlayerDrunk != requirement.requirementBoolValue)
                        {
                            return false;
                        }
                        break;

                    default:
                        Debug.LogWarning("Unknown requirement name: " + requirement.requirementName);
                        return false;
                }
                break;
            }
            else if (requirement.requirementValueType == ChoiceOptionRequirement.RequirementType.Int)
            {
                switch (requirement.requirementName)
                {
                    case "alcoholConsumed":
                        if (alcoholConsumed < requirement.requirementIntValue)
                        {
                            return false;
                        }
                        break;

                    default:
                        Debug.LogWarning("Unknown requirement name: " + requirement.requirementName);
                        return false;
                }
                break;
            }
        }
        // All requirements passed
        return true;
    }

    
    public void CheckIfPlayerIsDrunk()
    {
        if (alcoholConsumed >= 15 && alcoholConsumed < 20)
        {
            // Set the player to drunk state
            Debug.Log("Player is now drunk!");
            isPlayerDrunk = true;
            // You can add additional logic here to handle the drunk state
        }
        else if(alcoholConsumed > 20)
        {
            Debug.Log("Player is now very drunk!");
            isPlayerDrunk = true;
        }
        else
        {
            Debug.Log("Player is not drunk yet.");
            isPlayerDrunk = false;
        }
    }

    public void IncreaseAlcoholConsumed(int amount)
    {
        alcoholConsumed += amount;
    }

    public string GetPlayerName()
    {
        return playerName;
    }
    public void SetPlayerName(string name)
    {
        playerName = name;
    }
    
}
