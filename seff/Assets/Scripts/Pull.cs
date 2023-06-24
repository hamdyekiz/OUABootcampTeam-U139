using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pull : MonoBehaviour
{
    [SerializeField] private Transform attachmentPoint;
    [SerializeField] private float pullForce = 10f;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartWebPull();
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            EndWebPull();
        }
    }

    private void StartWebPull()
    {
        if (attachmentPoint != null)
        {
            Vector3 direction = attachmentPoint.position - transform.position;
            rb.AddForce(direction.normalized * pullForce, ForceMode.VelocityChange);
        }
    }

    private void EndWebPull()
    {
        // Optionally, you can add code to handle the release or detachment of the character from the web or zipline.
    }
}

