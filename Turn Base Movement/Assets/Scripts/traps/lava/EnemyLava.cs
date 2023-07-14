using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyLava : MonoBehaviour
{

    public float speed;
    public Transform movePoints;

    public float minX;
    public float maxX;

    public float minY;
    public float maxY;

    public float minZ;
    public float maxZ;
    public float startTime;
    private float waitTime;
    void Start()
    {
        movePoints.position = new Vector3(
            Random.Range(minX, maxX),
            Random.Range(minY, maxY),
            Random.Range(minZ, maxZ)
            );
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            movePoints.position,
            speed * Time.deltaTime
            );

        if (Vector3.Distance(transform.position, movePoints.position) < 0.2f)
        {

            if (waitTime <= 0)
            {
                movePoints.position = new Vector3(
                 Random.Range(minX, maxX),
                 Random.Range(minY, maxY),
                 Random.Range(minZ, maxZ)
                 );
                waitTime = startTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}