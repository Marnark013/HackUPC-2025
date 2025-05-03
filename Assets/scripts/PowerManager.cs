using UnityEngine;
using System.Collections.Generic;
public class PowerManager : MonoBehaviour
{

    public static PowerManager Instance { get; private set; }
    private List<ElectricityTile> electricityTiles;
    void Awake()
    {
        if (Instance == null)
        {
            electricityTiles = new List<ElectricityTile>();
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetElectricuityTile(ElectricityTile tile) {
        electricityTiles.Add(tile);
    }

    public double getGeneratedPower()
    {
        double generatedPower =0;
        electricityTiles.RemoveAll(item => item == null);
        foreach (ElectricityTile tile in electricityTiles)
        {
            generatedPower += tile.getGeneratedEnergy();
        }
        return generatedPower;
    }
    double coumputePowerNeeded()
    {
        BoardManager man = BoardManager.Instance;
        Tile[,] tiles = man.getTiles();
        int width = tiles.GetLength(0);  
        int height = tiles.GetLength(1); 
        double NeededPower = 0;
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Tile tile = tiles[x, y];
                NeededPower+= tile.EnergyConcumption();
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
