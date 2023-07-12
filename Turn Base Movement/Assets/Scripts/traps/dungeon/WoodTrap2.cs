using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WoodTrap2 : MonoBehaviour
{
    public float speed = 1.2f;
    private Vector3 pos_1 = new Vector3(298.95f, 26.51f, 846.32f);
    private Vector3 pos_2 = new Vector3(298.95f, 26.51f, 823.9f);

    void Start()
    {

    }
    void Update()
    {
        transform.position = Vector3.Lerp(pos_1, pos_2, Mathf.PingPong(Time.time * speed, 1f));
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
