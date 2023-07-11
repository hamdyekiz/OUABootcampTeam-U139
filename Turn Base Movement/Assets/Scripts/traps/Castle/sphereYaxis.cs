using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sphereYaxis : MonoBehaviour
{
    // Start is called before the first frame update

    public float speed = 0.6f;
    private Vector3 pos_1 = new Vector3(454.82f, 46.73f, 245.58f);
    private Vector3 pos_2 = new Vector3(454.82f, 90.9f, 245.58f);

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
