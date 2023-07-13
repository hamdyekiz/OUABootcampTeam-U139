using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalScript : MonoBehaviour
{
    public int sceneIndex; // The index of the scene to load

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();

            // Disable character control before loading the new scene
            playerMovement.SetCanMove(false);

            SceneManager.LoadScene(1);
        }
    }
}