using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sphereZaxis : MonoBehaviour
{
    // Start is called before the first frame update

    public float speed = 0.4f;
    private Vector3 pos_1 = new Vector3(472.28f, 50.25f, 250.96f);
    private Vector3 pos_2 = new Vector3(472.28f, 50.25f, 240.46f);

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
