using UnityEngine;

public class DialogueFloor : MonoBehaviour
{
    public Dialogue dialogue;
    public string[] dialogueLines;

    void Start()
    {
        if(dialogue == null)
        {
            dialogue = FindObjectOfType<Dialogue>(); // automatically find the Dialogue component if not assigned
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Assuming the player object has a tag "Player"
        {
            dialogue.SetLines(dialogueLines);
            dialogue.ShowDialogue();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            dialogue.gameObject.SetActive(false); // Hide the dialogue box when player leaves
        }
    }
}
