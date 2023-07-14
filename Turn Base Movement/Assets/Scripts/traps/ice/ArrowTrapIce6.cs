using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArrowTrapIce6 : MonoBehaviour
{

    public float speed = 1.2f;
    private Vector3 pos_1 = new Vector3(418.75f, 68.75f, 1391.3f);
    private Vector3 pos_2 = new Vector3(258.2101f, 60.65997f, 1433.671f);

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


