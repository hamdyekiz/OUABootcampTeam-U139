using System.Collections;
using TMPro;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public float textSpeed;

    private string[] lines;
    private int index;

    void Start()
    {
        textComponent.text = string.Empty;
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (lines != null && gameObject.activeSelf)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (textComponent.text == lines[index])
                {
                    NextLine();
                }
                else
                {
                    StopAllCoroutines();
                    textComponent.text = lines[index];
                }
            }
        }
    }

    public void SetLines(string[] newLines)
    {
        lines = newLines;
    }

    public void ShowDialogue()
    {
        index = 0;
        textComponent.text = string.Empty;  // Clear the text component
        gameObject.SetActive(true);
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false); // Hide the dialogue box when finished
            lines = null;
        }
    }
}
