using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class yball3 : MonoBehaviour
{
    public float speed = 0.5f;
    private Vector3 pos_1 = new Vector3(462.84f, 96.83f, 253.83f);
    private Vector3 pos_2 = new Vector3(462.84f, 46.89f, 253.83f);

    void Start()
    {

    }

    // Update is called once per frame
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
