using UnityEngine;

public class DelayedDialogueTrigger : MonoBehaviour, IInteractable
{
    public Dialogue dialogue;
    public string[] lines;
    public float delay = 2f;  // Delay before showing dialogue

    public void Interact()
    {
        // Wait for the specified delay then show the dialogue
        Invoke("ShowDialogue", delay);
    }

    void ShowDialogue()
    {
        dialogue.SetLines(lines);
        dialogue.ShowDialogue();
    }
}
