using System;
using UnityEngine;

public class Hex : MonoBehaviour
{
    private HexCoordinates hexCoordinates;
    [SerializeField]
    private HexType hexType;

    public Vector3Int HexCoords => hexCoordinates.GetHexCoords();

    public Vector3Int HexPosition { get; internal set; }

    public int GetCost()
    {
        return hexType switch
        {
            HexType.Difficult => 20,
            HexType.Default => 10,
            HexType.Road => 5,
            _ => throw new Exception($"Hex of type {hexType} not supported")
        };
    }

    public bool IsObstacle()
    {
        return hexType == HexType.Obstacle;
    }

    private void Awake()
    {
        hexCoordinates = GetComponent<HexCoordinates>();
    }

    public void EnableHighlight()
    {
        ToggleHighlight(true);
    }

    public void DisableHighlight()
    {
        ToggleHighlight(false);
    }

    internal void ResetHighlight()
    {
        GetHighlight().ResetGlowHighlight();
    }

    internal void HighlightPath()
    {
        GetHighlight().HighlightValidPath();
    }

    private GlowHighlight GetHighlight()
    {
        return GetComponent<GlowHighlight>();
    }

    private void ToggleHighlight(bool state)
    {
        GetHighlight().ToggleGlow(state);
    }
}

public enum HexType
{
    None,
    Default,
    Difficult,
    Road,
    Water,
    Obstacle
}
