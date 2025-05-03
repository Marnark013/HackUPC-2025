using UnityEngine;

[RequireComponent(typeof(BoardManager))]
public class GridOverlay : MonoBehaviour
{
    [Header("Line Settings")]
    [Tooltip("Color of the grid lines")]
    public Color lineColor = new Color(0.5f, 0.5f, 0.5f, 0.3f);
    [Tooltip("Width of each grid line")]
    public float lineWidth = 0.02f;

    private BoardManager board;

    private void Awake()
    {
        board = GetComponent<BoardManager>();
    }

    private void OnValidate()
    {
        Refresh();
    }

    private void OnEnable()
    {
        Refresh();
    }

    private void OnDisable()
    {
        // clean up so you don't get duplicates in edit mode
        foreach (Transform t in transform)
            DestroyImmediate(t.gameObject);
    }

    public void Refresh()
    {
        if (board == null || board.mapData == null) return;

        // Clear existing lines
        foreach (Transform t in transform)
            DestroyImmediate(t.gameObject);

        int w = board.mapData.width;
        int h = board.mapData.height;

        // Adjust for half-tile offset
        float halfTile = 0.5f;

        // Draw vertical lines
        for (int x = 0; x <= w; x++)
        {
            int startY = -1;
            for (int y = 0; y < h; y++)
            {
                bool playable = board.mapData.GetCell(x - 1, y) == 1 || board.mapData.GetCell(x, y) == 1;
                if (playable && startY < 0) startY = y;
                if ((!playable || y == h - 1) && startY >= 0)
                {
                    int endY = playable ? y : y - 1;
                    CreateLine(
                        new Vector3(x - halfTile, startY - halfTile, 0),
                        new Vector3(x - halfTile, endY + 1 - halfTile, 0)
                    );
                    startY = -1;
                }
            }
        }

        // Draw horizontal lines
        for (int y = 0; y <= h; y++)
        {
            int startX = -1;
            for (int x = 0; x < w; x++)
            {
                bool playable = board.mapData.GetCell(x, y - 1) == 1 || board.mapData.GetCell(x, y) == 1;
                if (playable && startX < 0) startX = x;
                if ((!playable || x == w - 1) && startX >= 0)
                {
                    int endX = playable ? x : x - 1;
                    CreateLine(
                        new Vector3(startX - halfTile, y - halfTile, 0),
                        new Vector3(endX + 1 - halfTile, y - halfTile, 0)
                    );
                    startX = -1;
                }
            }
        }
    }

    private void CreateLine(Vector3 a, Vector3 b)
    {
        var go = new GameObject("GridLine");
        go.transform.SetParent(transform, false);

        var lr = go.AddComponent<LineRenderer>();
        lr.positionCount = 2;
        lr.useWorldSpace = true;
        lr.SetPosition(0, a);
        lr.SetPosition(1, b);

        lr.material = new Material(Shader.Find("Sprites/Default"));
        lr.startColor = lr.endColor = lineColor;
        lr.startWidth = lr.endWidth = lineWidth;

        lr.sortingLayerName = "Background";
        lr.sortingOrder = 0;
    }
}
