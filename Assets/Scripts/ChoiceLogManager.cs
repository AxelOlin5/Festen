using UnityEngine;

public class ChoiceLogManager : MonoBehaviour
{
    public bool broughtAlcohol;
    public bool talkedToEmma;
    public bool joinedTheDrinkingGame;
    public bool isPlayerDrunk;
    public string playerName;
    private int alcoholConsumed = 0;

    public void LogChoice(string choiceName, bool choiceValue)
    {
        switch (choiceName)
        {
            case "broughtAlcohol":
                broughtAlcohol = choiceValue;
                break;
            case "talkedToEmma":
                talkedToEmma = choiceValue;
                break;
            case "joinedTheDrinkingGame":
                joinedTheDrinkingGame = choiceValue;
                break;
            default:
                Debug.LogWarning("Unknown choice name: " + choiceName);
                break;
        }
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
    public string SetPlayerName(string name)
    {
        playerName = name;
        return playerName;
    }
}
