using UnityEngine;

public class DisappearAndTriggerGravityOnInteract : MonoBehaviour, IInteractable
{
    public GravityControlOnInteract gravityController; // Reference to the GravityControlOnInteract script
    public Dialogue dialogue; // Reference to the Dialogue script
    public string[] dialogueLines; // The lines of dialogue to display after interaction
    public float delay = 1f;  // Delay before showing dialogue

    private void Start()
    {
        if (gravityController == null)
        {
            Debug.LogError("No GravityControlOnInteract script assigned to " + gameObject.name);
        }
    }

    public void Interact()
    {
        Debug.Log("Interacting with: " + gameObject.name);
        
        // Set the GameObject to inactive when it's interacted with
        gameObject.SetActive(false);

        // Trigger the gravity controller
        if (gravityController != null)
        {
            gravityController.Interact();
        }

        // Start the dialogue after a delay
        Invoke("ShowDialogue", delay);
    }

    void ShowDialogue()
    {
        dialogue.SetLines(dialogueLines);
        dialogue.ShowDialogue();
    }
}
