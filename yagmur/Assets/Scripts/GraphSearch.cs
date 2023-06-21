using System.Collections.Generic;
using UnityEngine;

public class GraphSearch
{
    public static BFSResult BFSGetRange(HexGrid hexGrid, Vector3Int startPoint, int movementPoints)
    {
        Dictionary<Vector3Int, Vector3Int?> visitedNodes = new Dictionary<Vector3Int, Vector3Int?>();
        Dictionary<Vector3Int, int> costSoFar = new Dictionary<Vector3Int, int>();
        Queue<Vector3Int> nodesToVisitQueue = new Queue<Vector3Int>();

        nodesToVisitQueue.Enqueue(startPoint);
        visitedNodes[startPoint] = null;
        costSoFar[startPoint] = 0;

        while (nodesToVisitQueue.Count > 0)
        {
            Vector3Int currentNode = nodesToVisitQueue.Dequeue();

            int currentCost = costSoFar[currentNode];

            if (currentCost >= movementPoints)
                continue;

            foreach (Vector3Int neighbourPosition in hexGrid.GetNeighboursFor(currentNode))
            {
                if (visitedNodes.ContainsKey(neighbourPosition) || hexGrid.GetTileAt(neighbourPosition).IsObstacle())
                    continue;

                int nodeCost = hexGrid.GetTileAt(neighbourPosition).GetCost();
                int newCost = currentCost + nodeCost;

                if (newCost <= movementPoints)
                {
                    visitedNodes[neighbourPosition] = currentNode;
                    costSoFar[neighbourPosition] = newCost;
                    nodesToVisitQueue.Enqueue(neighbourPosition);
                }
            }
        }

        return new BFSResult { visitedNodesDict = visitedNodes };
    }

    public static List<Vector3Int> GeneratePathBFS(Vector3Int current, Dictionary<Vector3Int, Vector3Int?> visitedNodesDict)
    {
        List<Vector3Int> path = new List<Vector3Int>();
        path.Add(current);

        while (visitedNodesDict[current] != null)
        {
            current = visitedNodesDict[current].Value;
            path.Add(current);
        }

        path.Reverse();
        path.RemoveAt(0);

        return path;
    }
}

public struct BFSResult
{
    public Dictionary<Vector3Int, Vector3Int?> visitedNodesDict;

    public List<Vector3Int> GetPathTo(Vector3Int destination)
    {
        if (visitedNodesDict.TryGetValue(destination, out Vector3Int? parent))
        {
            return GraphSearch.GeneratePathBFS(destination, visitedNodesDict);
        }

        return new List<Vector3Int>();
    }

    public bool IsHexPositionInRange(Vector3Int position)
    {
        return visitedNodesDict.ContainsKey(position);
    }

    public IEnumerable<Vector3Int> GetRangePositions()
    {
        return visitedNodesDict.Keys;
    }
}
