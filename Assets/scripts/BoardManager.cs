using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public static BoardManager Instance { get; private set; }

    public int rows = 20;
    public int columns = 10;

    public GameObject[][] blockPrefabs;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void initializeMatrix()
    {
        blockPrefabs = new GameObject[rows][];
        for (int i = 0; i < rows; i++)
        {
            blockPrefabs[i] = new GameObject[columns];
            for (int j = 0; j < columns; j++)
            {
                blockPrefabs[i][j] = null;
            }
        }
    }
}
