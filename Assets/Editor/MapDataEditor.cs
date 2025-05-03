using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MapData))]
public class MapDataEditor : Editor
{
    private MapData map;
    private SerializedProperty widthProp;
    private SerializedProperty heightProp;
    private SerializedProperty layoutProp;

    private void OnEnable()
    {
        map = (MapData)target;
        widthProp = serializedObject.FindProperty(nameof(MapData.width));
        heightProp = serializedObject.FindProperty(nameof(MapData.height));
        layoutProp = serializedObject.FindProperty(nameof(MapData.flattenedLayout));

        // Ensure the underlying array isn't null
        if (map.flattenedLayout == null)
        {
            map.flattenedLayout = new int[0];
            EditorUtility.SetDirty(map);
        }
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        // Draw width/height fields
        EditorGUI.BeginChangeCheck();
        EditorGUILayout.PropertyField(widthProp);
        EditorGUILayout.PropertyField(heightProp);
        if (EditorGUI.EndChangeCheck())
        {
            // After the user changes width/height, resize the array
            ResizeLayoutArray(widthProp.intValue, heightProp.intValue);
        }

        // Draw the toggle‐grid
        DrawGrid();

        serializedObject.ApplyModifiedProperties();
    }

    private void ResizeLayoutArray(int newW, int newH)
    {
        int newSize = Mathf.Max(0, newW * newH);
        int oldSize = layoutProp.arraySize;

        layoutProp.arraySize = newSize;

        // Initialize new slots to 0
        for (int i = oldSize; i < newSize; i++)
            layoutProp.GetArrayElementAtIndex(i).intValue = 0;
    }

    private void DrawGrid()
    {
        int w = widthProp.intValue;
        int h = heightProp.intValue;

        if (w <= 0 || h <= 0)
            return;

        GUILayout.Space(8);
        EditorGUILayout.LabelField("Layout (Click to toggle)", EditorStyles.boldLabel);

        // Draw rows top→bottom
        for (int y = h - 1; y >= 0; y--)
        {
            EditorGUILayout.BeginHorizontal();
            for (int x = 0; x < w; x++)
            {
                int idx = y * w + x;
                var cellProp = layoutProp.GetArrayElementAtIndex(idx);
                bool isOn = cellProp.intValue == 1;

                // White = playable (1), gray = blocked (0)
                GUI.backgroundColor = isOn ? Color.white : Color.gray;
                if (GUILayout.Button("", GUILayout.Width(20), GUILayout.Height(20)))
                {
                    cellProp.intValue = isOn ? 0 : 1;
                }
            }
            GUI.backgroundColor = Color.white;
            EditorGUILayout.EndHorizontal();
        }
    }
}
