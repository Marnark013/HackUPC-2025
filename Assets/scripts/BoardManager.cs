using System;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public int width;
    public int height;

    private Tile[,] tiles;

    public static BoardManager Instance { get; private set; }
    void Awake()
    {
        if (Instance == null)
        {
            tiles = new Tile[width, height];
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool PlaceTile(Tile tile, Vector2Int gridPos)
    {
        if (tile == null)
        {
            Debug.LogWarning("PlaceTile: tile is null");
            return false;
        }

        if (!IsValidPosition(gridPos))
        {
            Debug.LogWarning($"PlaceTile: invalid position {gridPos}");
            return false;
        }

        if (tiles[gridPos.x, gridPos.y] != null)
        {
            Debug.LogWarning($"PlaceTile: cell {gridPos} already occupied.");
            return false;
        }

        Vector3 worldPos = GridToWorld(gridPos);
        tile.transform.position = worldPos;
        tile.Initialize(gridPos);

        // Keep track in the array
        tiles[gridPos.x, gridPos.y] = tile;
        if(CheckForFourMatchingTiles(gridPos))
        {
            Debug.Log($"PlaceTile: Four matching tiles found at {gridPos}");
            // Handle the four matching tiles logic here
        }

        return true;
    }


    public bool CheckForFourMatchingTiles(Vector2Int gridPos)
    {
        Tile centerTile = GetTileAt(gridPos);
        if (centerTile == null)
        {
            Debug.LogWarning($"CheckForFourMatchingTiles: No tile at position {gridPos}");
            return false;
        }

        Type centerTileType = centerTile.GetType();
        int matchingCount = 0;

        // Define all 8 directions (cardinal + diagonal)
        Vector2Int[] directions = new Vector2Int[]
        {
        new Vector2Int(0, 1),   // Up
        new Vector2Int(0, -1),  // Down
        new Vector2Int(-1, 0),  // Left
        new Vector2Int(1, 0),   // Right
        new Vector2Int(-1, 1),  // Top-left
        new Vector2Int(1, 1),   // Top-right
        new Vector2Int(-1, -1), // Bottom-left
        new Vector2Int(1, -1)   // Bottom-right
        };

        foreach (Vector2Int direction in directions)
        {
            Vector2Int neighborPos = gridPos + direction;
            if (IsValidPosition(neighborPos))
            {
                Tile neighborTile = GetTileAt(neighborPos);
                if (neighborTile != null && neighborTile.GetType() == centerTileType)
                {
                    matchingCount++;
                }
            }
        }
        Debug.Log($"Matching tiles found: {matchingCount}");
        return matchingCount >= 3;
    }

    public Tile GetTileAt(Vector2Int position)
    {
        if (IsValidPosition(position))
            return tiles[position.x, position.y];
        return null;
    }


    public bool IsValidPosition(Vector2Int pos)
    {
        return pos.x >= 0 && pos.x < width && pos.y >= 0 && pos.y < height;
    }

    public Vector3 GridToWorld(Vector2Int gridPos)
    {
        return new Vector3(gridPos.x, gridPos.y, 0);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Gizmos.DrawWireCube(new Vector3(x, y, 0), new Vector3(1, 1, 0));
            }
        }

    }
}
