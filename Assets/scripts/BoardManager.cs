using System;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    [Header("Grid Settings")]
    [Tooltip("Defines which cells are playable (1) or blocked (0)")]
    public MapData mapData;

    [Tooltip("Prefab used to render each cell; must have a SpriteRenderer")]
    public GameObject cellPrefab;

    private Color playableColor = new(254f / 255f, 250f / 255f, 224f / 255f, 1f);
    private Color blockedColor = new(253f / 255f, 245f / 255f, 195f / 255f, 1f);

    [Header("Tile Placement")]
    public TileSelector TileSelector;

    [HideInInspector] public int width;
    [HideInInspector] public int height;

    private Tile[,] tiles;

    public static BoardManager Instance { get; private set; }

     public Tile[,] getTiles()
    {
        return tiles;
    }
    private void Awake()
    {
        // --- singleton + array init ---
        if (Instance == null)
        {
            Instance = this;
            width = mapData.width;
            height = mapData.height;
            tiles = new Tile[width, height];

            RenderBackgroundGrid();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void RenderBackgroundGrid()
    {
        if (mapData == null || cellPrefab == null)
        {
            Debug.LogWarning("BoardManager: missing MapData or cellPrefab.");
            return;
        }

        for (int y = 0; y < mapData.height; y++)
        {
            for (int x = 0; x < mapData.width; x++)
            {
                // read 1 or 0
                int val = mapData.GetCell(x, y);

                // instantiate at grid position
                Vector3 worldPos = GridToWorld(new Vector2Int(x, y));
                GameObject go = Instantiate(cellPrefab, worldPos, Quaternion.identity, transform);

                // apply color
                var sr = go.GetComponent<SpriteRenderer>();
                if (sr != null)
                {
                    sr.color = (val == 1) ? playableColor : blockedColor;
                }
                else
                {
                    Debug.LogWarning("cellPrefab needs a SpriteRenderer");
                }
            }
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

        if (mapData.GetCell(gridPos.x, gridPos.y) == 0)
        {
            Debug.LogWarning($"PlaceTile: cell {gridPos} is blocked.");
            return false;
        }

        Vector3 worldPos = GridToWorld(gridPos);
        tile.transform.position = worldPos;
        tile.Initialize(gridPos);

        tiles[gridPos.x, gridPos.y] = tile;

        if (CheckForFourMatchingTiles(gridPos))
        {
            Debug.Log($"PlaceTile: Four matching tiles found at {gridPos}");
        }

        return true;
    }

    public void RemoveTile(Vector2Int gridPos)
    {
        if (IsValidPosition(gridPos) && tiles[gridPos.x, gridPos.y] != null)
        {
            tiles[gridPos.x, gridPos.y] = null;
        }
    }



    public bool CheckForFourMatchingTiles(Vector2Int gridPos)
    {
        Tile centerTile = GetTileAt(gridPos);
        if (centerTile == null)
        {
            Debug.LogWarning($"CheckForFourMatchingTiles: No tile at {gridPos}");
            return false;
        }

        Type type = centerTile.GetType();

        Vector2Int[] squareOrigins = new Vector2Int[]
        {
        new Vector2Int(0,  0),  
        new Vector2Int(-1, 0),  
        new Vector2Int(0, -1),  
        new Vector2Int(-1, -1)  
        };

        foreach (var originOffset in squareOrigins)
        {
            Vector2Int origin = gridPos + originOffset;

            var p1 = origin;
            var p2 = origin + Vector2Int.right;
            var p3 = origin + Vector2Int.up;
            var p4 = origin + Vector2Int.one;

            if (!IsValidPosition(p1) || !IsValidPosition(p2) ||
                !IsValidPosition(p3) || !IsValidPosition(p4))
                continue;

            Tile t1 = GetTileAt(p1);
            Tile t2 = GetTileAt(p2);
            Tile t3 = GetTileAt(p3);
            Tile t4 = GetTileAt(p4);

            // Check that none are null and all are the same type
            if (t1 != null && t2 != null && t3 != null && t4 != null
                && t1.GetType() == type
                && t2.GetType() == type
                && t3.GetType() == type
                && t4.GetType() == type)
            {
                tiles[p2.x, p2.y] = t1;
                tiles[p3.x, p3.y] = t1;
                tiles[p4.x, p4.y] = t1;
                Destroy(t2.gameObject);
                Destroy(t3.gameObject);
                Destroy(t4.gameObject);

                t1.transform.position += new Vector3(0.5f, 0.5f, 0);
                t1.transform.localScale *= 2;
                return true;
            }
        }

        // No 2×2 block found
        return false;
    }

    public Tile GetTileAt(Vector2Int position)
    {
        if (IsValidPosition(position))
            return tiles[position.x, position.y];
        return null;
    }

    public bool IsValidPosition(Vector2Int pos)
        => pos.x >= 0 && pos.x < width
        && pos.y >= 0 && pos.y < height;

    public Vector3 GridToWorld(Vector2Int gridPos)
        => new Vector3(gridPos.x, gridPos.y, 0f);

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
        
        if(tiles != null)
        {
            Gizmos.color = Color.red;
            foreach (Tile t in tiles)
            {
                if (t != null)
                {
                    Vector3 pos = t.transform.position;
                    Gizmos.DrawWireCube(pos, Vector3.one);
                }
            }
        }
}
}
