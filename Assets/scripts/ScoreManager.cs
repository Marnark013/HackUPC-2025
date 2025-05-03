using UnityEngine;
using UnityEngine.UI;
using System;
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    [SerializeField] private int currentScore = 0;

    [SerializeField] private Text scoreText;
    [SerializeField] private int dataNeeded;

    public double ComputationPower() {
        double computpower = 0;
        double storage = 0;
        BoardManager board = BoardManager.Instance;
        Tile[,] tiles = board.getTiles();

        int width = tiles.GetLength(0);  // Number of columns (x)
        int height = tiles.GetLength(1);
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Tile tile = tiles[x, y];

                if (tile is DiskTile diskTile)
                {
                    storage += diskTile.getMaxData();
                }else if( tile is CPUTile cpuTile)
                {
                    computpower += cpuTile.getTrowhtput();
                }
            }
        }
        return Math.Min(computpower,storage);


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
