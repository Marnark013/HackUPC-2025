using UnityEngine;
using System.Collections.Generic;
public class CoolingManager : MonoBehaviour
{
    private List<CoolingTile> coolingTiles;
    public static CoolingManager Instance { get; private set; }
    void Awake()
    {
        if (Instance == null)
        {
            coolingTiles = new  List<CoolingTile>();
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void setTile(CoolingTile tile) {
        coolingTiles.Add(tile);
    }
    public void CoolingTime()
    {
        coolingTiles.RemoveAll(item => item == null);
        foreach (CoolingTile tile in coolingTiles)
        {
                int rank = tile.getRank();
                double coolingEficienci = tile.getCoolingEficienci();
                //fer tota la logica de refredar
        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
