using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

public class StoryManager : MonoBehaviour
{
    [SerializeField] private InputActionReference ClickAction;
    private DialogueManager dm;
    private ChoiceManager cm;
    private EventManager em;
    public UI_Manager uiManager;
    public ChoiceLogManager choiceLogManager;
    [SerializeField] public StoryNode startNode;
    public StoryNode nextNode;
    public StoryNode currentNode;
    [SerializeField] private PlayableDirector director;

    private void Awake()
    {
        // instantiate C# managers and references
        uiManager = GameObject.FindWithTag("UI_Manager").GetComponent<UI_Manager>();
        choiceLogManager = GameObject.FindWithTag("ChoiceLogManager").GetComponent<ChoiceLogManager>();

        cm = new ChoiceManager(this, uiManager, choiceLogManager);
        dm = new DialogueManager(this, cm, uiManager);
        em = new EventManager(cm, this, director);
    }

    private void Start()
    {
        currentNode = startNode;
        NextNode(startNode);
    }

    void OnEnable()
    {
        if (ClickAction != null && ClickAction.action != null)
        {
            ClickAction.action.performed += OnDialogueClick;
        }
    }

    void OnDisable()
    {
        if (ClickAction != null && ClickAction.action != null)
        {
            ClickAction.action.performed -= OnDialogueClick;
        }

    }

    private void OnDialogueClick(InputAction.CallbackContext context)
    {
        if (context.performed && currentNode is DialogueNode dialogueNode)
        {
            NextNode(dialogueNode.nextNode);
        }
    }

    public void OnEventFinished()
    {
        if (currentNode is EventNode eventNode)
        {
            NextNode(eventNode.nextNode);
        }
    }

    public void OnChoiceMade(int choiceIndex)
    {
        if (currentNode is ChoiceNode choiceNode)
        {
            ChoiceOption selectedChoice = choiceNode.choiceOptions[choiceIndex];

            NextNode(selectedChoice.nextNode);
            uiManager.HideChoices();
        }
    }

    public void NextNode(StoryNode nextNode)
    {
        StoryNode node = nextNode;

        currentNode = node;

        if (node == null)
        {
             Debug.LogWarning("No story node available. Game Finished");
             return;
        }

        switch (node)
        {
            case DialogueNode dialogueNode:
                dm.HandleDialogueNode(dialogueNode);
                break;
            case ChoiceNode choiceNode:
                cm.HandleChoiceNode(choiceNode);
                break;
            case EventNode eventNode:
                em.HandleEventNode(eventNode);
                break;
            default:
                Debug.LogWarning("Unknown story node type.");
                break;
        }

    }
}


public class DialogueManager
{
    private StoryManager sm;
    private ChoiceManager cm;
    private UI_Manager uiManager;

    public DialogueManager(StoryManager sm, ChoiceManager cm, UI_Manager uiManager)
    {
        this.sm = sm;
        this.cm = cm;
        this.uiManager = uiManager;
    }

    public void HandleDialogueNode(DialogueNode dialogueNode)
    {
        if(dialogueNode.isPlayerSpeaking)
        {
            uiManager.UpdatePlayerDialogue(dialogueNode.characterName, dialogueNode.dialogueText, dialogueNode.speakerColor);
        }
        else
        {
            uiManager.UpdateCharacterDialogue(dialogueNode.characterName, dialogueNode.characterImage, dialogueNode.dialogueText, dialogueNode.speakerColor);
        }
    }
}

public class ChoiceManager
{
    private StoryManager sm;
    private UI_Manager uiManager;
    private ChoiceLogManager choiceLogManager;

    public ChoiceManager(StoryManager sm, UI_Manager uiManager, ChoiceLogManager choiceLogManager)
    {
        this.sm = sm;
        this.uiManager = uiManager;
        this.choiceLogManager = choiceLogManager;
    }

    public void HandleChoiceNode(ChoiceNode choiceNode)
    {
        sm.currentNode = choiceNode;
        uiManager.ShowChoices(choiceNode.choiceOptions, choiceNode, (choiceIndex) => 
        {
            sm.OnChoiceMade(choiceIndex);
        });
    }

}

public class EventManager
{
    private ChoiceManager cm;
    private StoryManager sm;
    private PlayableDirector director;

    public EventManager(ChoiceManager cm, StoryManager sm, PlayableDirector director)
    {
        this.cm = cm;
        this.sm = sm;
        this.director = director;
    }

    public void HandleEventNode(EventNode eventNode)
    {
        director.playableAsset = eventNode.timelineAsset;

        director.stopped += OnPlayableDirectorStopped;

        director.Play();
    }

    public void OnPlayableDirectorStopped(PlayableDirector pd)
    {
        director.stopped -= OnPlayableDirectorStopped;

        Debug.Log("Event finished.");

        sm.OnEventFinished();
    }
}
