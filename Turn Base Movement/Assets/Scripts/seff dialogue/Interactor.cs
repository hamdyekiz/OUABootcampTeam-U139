using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void Interact();
}

public class Interactor : MonoBehaviour
{
    public Transform InteractorSource;
    public float InteractRange;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E key pressed");

            Collider closestInteractable = null;
            float closestDistance = InteractRange;

            // Get all colliders within the interact range
            Collider[] hitColliders = Physics.OverlapSphere(InteractorSource.position, InteractRange);
            foreach (var hitCollider in hitColliders)
            {
                // Check if the collider has the "Interactable" tag
                if (hitCollider.gameObject.CompareTag("Interactable"))
                {
                    if (hitCollider.gameObject.TryGetComponent(out IInteractable interactable))
                    {
                        float distance = Vector3.Distance(InteractorSource.position, hitCollider.transform.position);
                        if (distance < closestDistance)
                        {
                            // This interactable is closer than any other found so far
                            closestInteractable = hitCollider;
                            closestDistance = distance;
                        }
                    }
                }

                // Draw debug lines for all detected colliders
                if (hitCollider.gameObject.name == "Wing6")
                {
                    Debug.DrawLine(InteractorSource.position, hitCollider.transform.position, Color.red, 2f);
                }
                else
                {
                    Debug.DrawLine(InteractorSource.position, hitCollider.transform.position, Color.yellow, 2f);
                }
            }

            if (closestInteractable != null)
            {
                // Interact with the closest interactable object
                Debug.Log("Interacting with: " + closestInteractable.gameObject.name);
                closestInteractable.gameObject.GetComponent<IInteractable>().Interact();
            }
            else
            {
                Debug.Log("No interactable objects within range");
            }
        }
    }
}
