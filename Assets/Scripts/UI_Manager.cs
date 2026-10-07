using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System;
using System.Collections;

public class UI_Manager : MonoBehaviour
{
    private UIDocument UIDocument;
    private VisualElement textBox;
    private Label textBoxText;
    private VisualElement speakerName;
    private Label speakerNameText;
    private SpriteRenderer characterImageComponent;
    private VisualElement choiceContainer;
    //private Coroutine fadeCoroutine;

    private void Awake()
    {
        // Initialize UI elements here
        UIDocument = GetComponent<UIDocument>();

        textBox = UIDocument.rootVisualElement.Q<VisualElement>("TextBox");
        textBoxText = textBox.Q<Label>("TextBoxText");

        speakerName = textBox.Q<VisualElement>("SpeakerName");
        speakerNameText = speakerName.Q<Label>("SpeakerNameText");

        characterImageComponent = GameObject.FindWithTag("CharacterImage").GetComponent<SpriteRenderer>();
        choiceContainer = UIDocument.rootVisualElement.Q<VisualElement>("ChoiceContainer");

        textBox.style.opacity = 0f;

        Debug.Log($"SpeakerName found: {speakerName != null}");
    }

    public void ShowDialogueUI()
    {
        StartCoroutine(FadeDialogue(0f, 1f, 0.5f));
    }

    public void HideDialogueUI()
    {
        StartCoroutine(FadeDialogue(1f, 0f, 0.5f));
    }

    private IEnumerator FadeDialogue(float startOpacity, float targetOpacity, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float newOpacity = Mathf.Lerp(startOpacity, targetOpacity, elapsedTime / duration);
            textBox.style.opacity = newOpacity;
            yield return null;
        }
        textBox.style.opacity = targetOpacity;
    }

    public void UpdateCharacterDialogue(string characterName, Sprite characterImage, string dialogueText, Color speakerColor)
    {
        // Code to update the dialogue in the UI
        if (textBoxText != null)
        {
            textBoxText.text = dialogueText;
        }
        if(speakerName != null)
        {
            ChangeBorderColor(speakerName, speakerColor);
        }
        if (speakerNameText != null)
        {
            speakerNameText.text = characterName;
        }
        if (characterImageComponent != null)
        {
            characterImageComponent.sprite = characterImage;
        }
    }

    public void UpdatePlayerDialogue(string characterName, string dialogueText, Color speakerColor)
    {
        // Code to update the dialogue in the UI
        if (textBoxText != null)
        {
            textBoxText.text = dialogueText;
        }
        if (speakerName != null)
        {
            ChangeBorderColor(speakerName, speakerColor);
        }
        if (speakerNameText != null)
        {
            speakerNameText.text = characterName;
        }
    }
    public void ChangeBorderColor(VisualElement visualElement, Color color)
    {
        visualElement.style.borderTopColor = color;
        visualElement.style.borderBottomColor = color;
        visualElement.style.borderLeftColor = color;
        visualElement.style.borderRightColor = color;

        visualElement.style.borderTopWidth = 4;
        visualElement.style.borderBottomWidth = 4;
        visualElement.style.borderLeftWidth = 4;
        visualElement.style.borderRightWidth = 4;

        Debug.Log($"Changing speaker border to: {color}");
    }

    public void ShowChoices(List<ChoiceOption> choices, ChoiceNode choiceNode, Action<int> onChoiceSelected)
    {
        choiceContainer.Clear();

        for (int i = 0; i < choices.Count; i++)
        {
            int choiceIndex = i;

            Button button = new Button();
            button.text = choices[i].choiceText;
            button.AddToClassList("choiceButton");

            button.clicked += () => onChoiceSelected(choiceIndex);

            choiceContainer.Add(button);
        }
        
        textBoxText.text = choiceNode.choiceDialogueText;
    }

    public void HideChoices()
    {
        choiceContainer.Clear();
    }
}
