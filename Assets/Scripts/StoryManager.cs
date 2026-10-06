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
    [SerializeField] public StoryNode startNode;
    public StoryNode nextNode;
    public StoryNode currentNode;
    [SerializeField] private PlayableDirector director;

    private void Awake()
    {
        // instantiate C# managers and references
        cm = new ChoiceManager(this);
        dm = new DialogueManager(this, cm);
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

    public void OnChoiceMade()
    {
        if (currentNode is ChoiceNode choiceNode)
        {
            NextNode(choiceNode.nextNode);
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

    public DialogueManager(StoryManager sm, ChoiceManager cm)
    {
        this.sm = sm;
        this.cm = cm;
    }

    public void HandleDialogueNode(DialogueNode dialogueNode)
    {
        sm.nextNode = dialogueNode.nextNode;

        Debug.Log("Dialogue.");
    }
}

public class ChoiceManager
{
    private StoryManager sm;

    public ChoiceManager(StoryManager sm)
    {
        this.sm = sm;
    }

    public void HandleChoiceNode(ChoiceNode choiceNode)
    {
        sm.nextNode = choiceNode.nextNode;

        Debug.Log("Choice.");
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

        sm.OnEventFinished();
    }
}
