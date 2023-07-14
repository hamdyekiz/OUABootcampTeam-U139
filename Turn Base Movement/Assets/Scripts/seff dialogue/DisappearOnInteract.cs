using UnityEngine;

public class DisappearOnInteract : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log("Interacting with: " + gameObject.name); // Log a message when Interact() is called
        gameObject.SetActive(false);
    }
}
