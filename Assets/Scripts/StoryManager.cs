using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class StoryManager : MonoBehaviour
{
    [SerializeField] private InputActionReference ClickAction;
    private DialogueManager dm;
    private ChoiceManager cm;
    private EventManager em;
    [SerializeField] private List<StoryNode> storyNodes = new List<StoryNode>();
    [SerializeField] private int currentNodeIndex = -1;

    private void Awake()
    {
        // instantiate plain C# managers and wire references
        cm = new ChoiceManager(this);
        dm = new DialogueManager(this, cm);
        em = new EventManager(cm);
    }

    private void Start()
    {
        NextNode();
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
        if (context.performed && !cm.making_A_Choice)
        {
            NextNode();
        }
    }

    public void NextNode()
    {

        if (storyNodes == null || storyNodes.Count == 0)
        {
             Debug.LogWarning("No story nodes available.");
             return;
        }

        currentNodeIndex++;

        StoryNode node = storyNodes[currentNodeIndex];

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


// Make these plain C# classes (remove MonoBehaviour)

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
        cm.making_A_Choice = false;

        Debug.Log("Dialogue.");
    }
}

public class ChoiceManager
{
    public bool making_A_Choice = false;
    private StoryManager sm;

    public ChoiceManager(StoryManager sm)
    {
        this.sm = sm;
    }

    public void HandleChoiceNode(ChoiceNode choiceNode)
    {
        making_A_Choice = true;

        Debug.Log("Choice.");
    }
}

public class EventManager
{
    private ChoiceManager cm;

    public EventManager(ChoiceManager cm)
    {
        this.cm = cm;
    }

    public void HandleEventNode(EventNode eventNode)
    {
        cm.making_A_Choice = false;

        eventNode.TriggerEvent();
        Debug.Log("Event.");
    }
}
