using UnityEngine;

public class PowerManager : MonoBehaviour
{


    double coumputePowerNeeded()
    {
        BoardManager man = BoardManager.Instance;
        Tile[,] tiles = man.getTiles();
        int width = tiles.GetLength(0);  // Number of columns (x)
        int height = tiles.GetLength(1); // Number of rows (y)
        double NeededPower = 0;
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Tile tile = tiles[x, y];
                NeededPower+= tile.EnergyConcumption();
                // Do something with tile
            }
        }
        return NeededPower;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
