using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthBar : MonoBehaviour
{
    public float moveSpeed = 5f;
   
    private void Update()
    {
        // Hareket kontrolleri
        float moveX = 0f;
        float moveZ = 0f;

        if (Input.GetKey(KeyCode.W))
        {
            moveZ = 1f;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            moveZ = -1f;
        }

        if (Input.GetKey(KeyCode.A))
        {
            moveX = -1f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            moveX = 1f;
        }

        // Hareket vektörü oluşturma
        Vector3 movement = new Vector3(moveX, 0f, moveZ) * moveSpeed * Time.deltaTime;

        // Karakteri hareket ettirme
        transform.Translate(movement);
    }
}
