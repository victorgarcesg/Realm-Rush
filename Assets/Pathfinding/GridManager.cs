using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] Vector2Int gridSize;

    [Tooltip("World Grid Size - Should match Unity editor snap settings.")]
    [SerializeField] int unityGridSize = 10;
    public int UnityGridSize { get { return unityGridSize; } }

    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();
    public Dictionary<Vector2Int, Node> Grid { get { return grid; } }

    private void Awake()
    {
        CreateGrid();
    }

    public Node GetNode(Vector2Int coordinates)
    {
        if (grid.ContainsKey(coordinates))
        {
            return grid[coordinates];
        }

        return null;
    }

    public Vector2Int GetCoordinatesFromPosition(Vector3 position)
    {
        Vector2Int coordinates = new Vector2Int();
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / unityGridSize);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / unityGridSize);

        return coordinates;
    }

    private void CreateGrid()
    {
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                Vector2Int coordinates = new Vector2Int(x, y);
                Node node = new Node(coordinates, true);
                grid.Add(coordinates, node);
            }
        }
    }
}
