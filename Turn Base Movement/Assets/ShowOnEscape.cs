using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    public GameObject PauseMenuCanvas;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Toggle the visibility of the pause menu canvas
            PauseMenuCanvas.SetActive(!PauseMenuCanvas.activeSelf);
        }
    }
}
