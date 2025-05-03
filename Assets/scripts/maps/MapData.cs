using UnityEngine;

[CreateAssetMenu(fileName = "NewMap", menuName = "Maps/Map Data")]
public class MapData : ScriptableObject
{
    public int width;
    public int height;

    [Tooltip("Row-major flattened grid: index = y * width + x")]
    public int[] flattenedLayout;

    public int GetCell(int x, int y)
    {
        if (x < 0 || x >= width || y < 0 || y >= height)
            return 0;
        return flattenedLayout[y * width + x];
    }
}
