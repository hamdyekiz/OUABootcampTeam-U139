using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArrowTrapIce3 : MonoBehaviour
{

    public float speed = 1.2f;
    private Vector3 pos_1 = new Vector3(201.9649f, 63.66f, 1377.412f);
    private Vector3 pos_2 = new Vector3(328.7627f, 63.66f, 1361.844f);

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


