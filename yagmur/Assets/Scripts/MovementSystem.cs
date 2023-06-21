using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MovementSystem : MonoBehaviour
{
    private BFSResult movementRange = new BFSResult();
    private List<Vector3Int> currentPath = new List<Vector3Int>();
    private HashSet<Vector3Int> previousPathPositions = new HashSet<Vector3Int>();

    public void HideRange(HexGrid hexGrid)
    {
        foreach (Hex hex in movementRange.GetRangePositions().Select(hexGrid.GetTileAt))
        {
            hex.DisableHighlight();
        }
        movementRange = new BFSResult();
    }

    public void ShowRange(Unit selectedUnit, HexGrid hexGrid)
    {
        CalcualteRange(selectedUnit, hexGrid);

        Vector3Int unitPos = hexGrid.GetClosestHex(selectedUnit.transform.position);
        IEnumerable<Vector3Int> rangePositions = movementRange.GetRangePositions().Except(new[] { unitPos });

        foreach (Hex hex in rangePositions.Select(hexGrid.GetTileAt))
        {
            hex.EnableHighlight();
        }
    }

    public void CalcualteRange(Unit selectedUnit, HexGrid hexGrid)
    {
        movementRange = GraphSearch.BFSGetRange(hexGrid, hexGrid.GetClosestHex(selectedUnit.transform.position), selectedUnit.MovementPoints);
    }

    public void ShowPath(Vector3Int selectedHexPosition, HexGrid hexGrid)
    {
        if (movementRange.IsHexPositionInRange(selectedHexPosition))
        {
            foreach (Vector3Int hexPosition in currentPath)
            {
                hexGrid.GetTileAt(hexPosition).ResetHighlight();
            }

            currentPath = movementRange.GetPathTo(selectedHexPosition);
            foreach (Vector3Int hexPosition in currentPath)
            {
                Hex hex = hexGrid.GetTileAt(hexPosition);
                hex.HighlightPath();
                previousPathPositions.Add(hexPosition);
            }
        }
    }

    public void MoveUnit(Unit selectedUnit, HexGrid hexGrid)
    {
        Debug.Log("Moving unit " + selectedUnit.name);
        selectedUnit.MoveThroughPath(currentPath.Select(pos => hexGrid.GetTileAt(pos).transform.position).ToList());
    }

    public bool IsHexInRange(Vector3Int hexPosition)
    {
        return movementRange.IsHexPositionInRange(hexPosition);
    }
}
