using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexGrid : MonoBehaviour
{
    private Dictionary<Vector3Int, Hex> hexTileDict = new Dictionary<Vector3Int, Hex>();
    private Dictionary<Vector3Int, List<Vector3Int>> hexTileNeighboursDict = new Dictionary<Vector3Int, List<Vector3Int>>();

    private void Start()
    {
        foreach (Hex hex in FindObjectsOfType<Hex>())
        {
            hexTileDict[hex.HexCoords] = hex;
        }
    }

    public Hex GetTileAt(Vector3Int hexCoordinates)
    {
        return hexTileDict.TryGetValue(hexCoordinates, out Hex result) ? result : null;
    }

    public List<Vector3Int> GetNeighboursFor(Vector3Int hexCoordinates)
    {
        if (!hexTileDict.ContainsKey(hexCoordinates))
        {
            return new List<Vector3Int>();
        }

        if (hexTileNeighboursDict.TryGetValue(hexCoordinates, out List<Vector3Int> neighbours))
        {
            return neighbours;
        }

        neighbours = new List<Vector3Int>();
        foreach (Vector3Int direction in Direction.GetDirectionList(hexCoordinates.z))
        {
            Vector3Int neighbourCoords = hexCoordinates + direction;
            if (hexTileDict.ContainsKey(neighbourCoords))
            {
                neighbours.Add(neighbourCoords);
            }
        }

        hexTileNeighboursDict[hexCoordinates] = neighbours;
        return neighbours;
    }

    public Vector3Int GetClosestHex(Vector3 worldPosition)
    {
        worldPosition.y = 0;
        return HexCoordinates.ConvertPositionToOffset(worldPosition);
    }

    internal void UpdateHealthBar(float fillAmount)
    {
        throw new NotImplementedException();
    }
}

public static class Direction
{
    private static readonly List<Vector3Int> directionsOffsetOdd = new List<Vector3Int>
    {
        new Vector3Int(-1, 0, 1), // N1
        new Vector3Int(0, 0, 1),  // N2
        new Vector3Int(1, 0, 0),  // E
        new Vector3Int(0, 0, -1), // S2
        new Vector3Int(-1, 0, -1),// S1
        new Vector3Int(-1, 0, 0)   // W
    };

    private static readonly List<Vector3Int> directionsOffsetEven = new List<Vector3Int>
    {
        new Vector3Int(0, 0, 1),  // N1
        new Vector3Int(1, 0, 1),  // N2
        new Vector3Int(1, 0, 0),  // E
        new Vector3Int(1, 0, -1), // S2
        new Vector3Int(0, 0, -1), // S1
        new Vector3Int(-1, 0, 0)  // W
    };

    public static List<Vector3Int> GetDirectionList(int z)
    {
        return z % 2 == 0 ? directionsOffsetEven : directionsOffsetOdd;
    }
}
