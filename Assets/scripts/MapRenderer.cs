using System;
using UnityEngine;

public class MapRenderer : MonoBehaviour
{
    [Header("Map Configuration")]
    public MapData mapData;


    [Header("Tile Colors")]
    public Color playableColor = new Color(254f / 255f, 250f / 255f, 224f / 255f, 1f);
    public Color blockedColor = new Color(253f / 255f, 245f / 255f, 195f / 255f, 1f);

    [Header("References")]
    public GameObject tilePrefab;

    public void Start()
    {
        Debug.Log("MapRenderer started.");
        if (mapData == null || tilePrefab == null)
        {
            Debug.LogWarning("MapRenderer missing references.");
            return;
        }

        for (int y = 0; y < mapData.height; y++)
        {
            for (int x = 0; x < mapData.width; x++)
            {
                int cell = mapData.GetCell(x, y);
                Vector3 pos = new Vector3(x, y, 0);

                GameObject tile = Instantiate(tilePrefab, pos, Quaternion.identity, transform);
                var sr = tile.GetComponent<SpriteRenderer>();
                if (sr == null)
                {
                    Debug.LogWarning($"Tile prefab does not have a SpriteRenderer component.");
                    continue;
                }
                if (sr != null)
                {
                    Debug.Log($"Tile prefab has a SpriteRenderer component.");
                    sr.color = (cell == 1) ? playableColor : blockedColor;
                }
            }
        }
    }
}
