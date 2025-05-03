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

    public void PlaceTile(Tile tilePrefab, Vector2Int position)
    {
        if (IsValidPosition(position) && tiles[position.x, position.y] == null)
        {
            Tile newTile = Instantiate(tilePrefab, GridToWorld(position), Quaternion.identity);
            newTile.Initialize(position);
            tiles[position.x, position.y] = newTile;
        }
    }


    public Tile GetTileAt(Vector2Int position)
    {
        if (IsValidPosition(position))
            return tiles[position.x, position.y];
        return null;
    }


    private bool IsValidPosition(Vector2Int pos)
    {
        return pos.x >= 0 && pos.x < width && pos.y >= 0 && pos.y < height;
    }

    private Vector3 GridToWorld(Vector2Int gridPos)
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
