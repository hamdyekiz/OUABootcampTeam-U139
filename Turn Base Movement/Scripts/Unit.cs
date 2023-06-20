using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Unit : MonoBehaviour
{
    [SerializeField] private int movementPoints = 20;
    public int MovementPoints => movementPoints;

    [SerializeField] private float movementDuration = 1f;
    [SerializeField] private float rotationDuration = 0.3f;

    private GlowHighlight glowHighlight;
    private Queue<Vector3> pathPositions = new Queue<Vector3>();

    public event Action<Unit> MovementFinished;

    private void Awake()
    {
        glowHighlight = GetComponent<GlowHighlight>();
    }

    public void Deselect()
    {
        glowHighlight.ToggleGlow(false);
    }

    public void Select()
    {
        glowHighlight.ToggleGlow(true);
    }

    public void MoveThroughPath(List<Vector3> currentPath)
    {
        pathPositions = new Queue<Vector3>(currentPath);
        if (pathPositions.Count > 0)
        {
            StartCoroutine(MovementCoroutine(pathPositions.Dequeue()));
        }
    }

    private IEnumerator MovementCoroutine(Vector3 endPosition)
    {
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.LookRotation(endPosition - transform.position, Vector3.up);

        float rotationTimeElapsed = 0f;
        while (rotationTimeElapsed < rotationDuration)
        {
            rotationTimeElapsed += Time.deltaTime;
            float rotationLerpStep = rotationTimeElapsed / rotationDuration;
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, rotationLerpStep);
            yield return null;
        }
        transform.rotation = endRotation;

        Vector3 startPosition = transform.position;
        float movementTimeElapsed = 0f;
        while (movementTimeElapsed < movementDuration)
        {
            movementTimeElapsed += Time.deltaTime;
            float movementLerpStep = movementTimeElapsed / movementDuration;
            transform.position = Vector3.Lerp(startPosition, endPosition, movementLerpStep);
            yield return null;
        }
        transform.position = endPosition;

        if (pathPositions.Count > 0)
        {
            StartCoroutine(MovementCoroutine(pathPositions.Dequeue()));
        }
        else
        {
            MovementFinished?.Invoke(this);
        }
    }
}
