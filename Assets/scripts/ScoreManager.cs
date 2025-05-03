using UnityEngine;
using UnityEngine.UI;
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    [SerializeField] private int currentScore = 0;

    [SerializeField] private Text scoreText;
    [SerializeField] private int dataNeeded;

    public bool SetData(int dataIncrement)
    {
        BoardManager board;
        Tile[,] tiles = board.tiles;

        int width = tiles.GetLength(0);  // Number of columns (x)
        int height = tiles.GetLength(1); // Number of rows (y)

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {

                Tile tile = tiles[x, y];
                if (tile is DiskTile) { 
                    dataIncrement = tile.SetData(dataIncrement);
                }

                if (dataIncrement == 0) return true;
            }
        }
        return dataIncrement == 0;
    }


    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        UpdateScoreUI();
    }

    public void AddScore(int amount)
    {
        currentScore += amount;
        UpdateScoreUI();
    }

    public void ResetScore()
    {
        currentScore = 0;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + currentScore;
    }

    public int GetCurrentScore()
    {
        return currentScore;
    }
}
