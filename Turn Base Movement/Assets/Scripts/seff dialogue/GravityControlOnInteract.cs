using UnityEngine;

public class GravityControlOnInteract : MonoBehaviour, IInteractable
{
    public GameObject objectToControl; // The object whose gravity will be controlled
    private Rigidbody objectRigidbody; // Rigidbody component of the object

    void Start()
    {
        // Attempt to get the Rigidbody component of the object
        objectRigidbody = objectToControl.GetComponent<Rigidbody>();
        if (objectRigidbody == null)
        {
            Debug.LogError("No Rigidbody found on " + objectToControl.name);
        }
        else
        {
            // Ensure the object is initially not kinematic so it can fall
            objectRigidbody.isKinematic = false;
        }
    }

    public void Interact()
    {
        if (objectRigidbody != null)
        {
            // Enable gravity for the object
            objectRigidbody.useGravity = true;
        }
    }

    // This function is called when the object starts colliding with another object
    void OnCollisionEnter(Collision collision)
    {
        if (objectRigidbody != null)
        {
            // Make the object kinematic so it doesn't move after falling
            objectRigidbody.isKinematic = true;
        }
    }
}
