using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UI_Manager : MonoBehaviour
{
    private UIDocument rootVisualElement;
    private VisualElement textBox;
    private Label textBoxText;
    private VisualElement speakerName;
    private Label speakerNameText;
    private Sprite characterImageComponent;

    private void Awake()
    {
        // Initialize UI elements here
        rootVisualElement = GetComponent<UIDocument>();

        textBox = rootVisualElement.rootVisualElement.Q<VisualElement>("TextBox");
        textBoxText = textBox.Q<Label>("TextBoxText");
        speakerName = textBox.Q<VisualElement>("SpeakerName");
        speakerNameText = speakerName.Q<Label>("SpeakerNameText");
        characterImageComponent = GameObject.FindWithTag("CharacterImage").GetComponent<SpriteRenderer>().sprite;
        rootVisualElement.rootVisualElement.style.display = DisplayStyle.None; // Hide the UI initially
    }

    public void ShowDialogueUI()
    {
        // Code to show the dialogue UI
        rootVisualElement.rootVisualElement.style.display = DisplayStyle.Flex;
    }

    public void HideDialogueUI()
    {
        // Code to hide the dialogue UI
        rootVisualElement.rootVisualElement.style.display = DisplayStyle.Flex;
    }

    public void UpdateCharacterDialogue(string characterName, Sprite characterImage, string dialogueText)
    {
        // Code to update the dialogue in the UI
        if (textBoxText != null)
        {
            textBoxText.text = dialogueText;
        }
        if (speakerNameText != null)
        {
            speakerNameText.text = characterName;
        }
        if (characterImageComponent != null)
        {
            characterImageComponent = characterImage;
        }
    }

    public void UpdatePlayerDialogue(string characterName, string dialogueText)
    {
        // Code to update the dialogue in the UI
        if (textBoxText != null)
        {
            textBoxText.text = dialogueText;
        }
        if (speakerNameText != null)
        {
            speakerNameText.text = characterName;
        }
    }
}
