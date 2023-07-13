using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CylinderTrap2 : MonoBehaviour
{
   
    public float speed = 0.5f;
    private Vector3 pos_1 = new Vector3(355.21f, 23.92f, 868.67f);
    private Vector3 pos_2 = new Vector3(359.14f, 23.92f, 846.37f);
   
    void Update()
    {

        transform.position = Vector3.Lerp(pos_1, pos_2, Mathf.PingPong(Time.time * speed, 1f));
        transform.Rotate(new Vector3(0, 1, 0)*360f*Time.deltaTime * speed);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
