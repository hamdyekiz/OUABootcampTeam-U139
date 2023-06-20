using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexCoordinates : MonoBehaviour
{
    public static float xOffset = 2f;
    public static float yOffset = 1f;
    public static float zOffset = 1.73f;

    private static readonly float invertedXOffset = 1f / xOffset;
    private static readonly float invertedYOffset = 1f / yOffset;
    private static readonly float invertedZOffset = 1f / zOffset;

    [Header("Offset coordinates")]
    [SerializeField]
    private Vector3Int offsetCoordinates;

    public Vector3Int GetHexCoords() => offsetCoordinates;

    private void Awake()
    {
        offsetCoordinates = ConvertPositionToOffset(transform.position);
    }

    public static Vector3Int ConvertPositionToOffset(Vector3 position)
    {
        int x = Mathf.CeilToInt(position.x * invertedXOffset);
        int y = Mathf.RoundToInt(position.y * invertedYOffset);
        int z = Mathf.RoundToInt(position.z * invertedZOffset);
        return new Vector3Int(x, y, z);
    }
}
