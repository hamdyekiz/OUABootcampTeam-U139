using UnityEngine;

public class DialogueTrigger : MonoBehaviour, IInteractable
{
    public Dialogue dialogue;
    public string[] lines;

    public void Interact()
    {
        dialogue.SetLines(lines);
        dialogue.ShowDialogue();
    }
}
