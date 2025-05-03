using UnityEngine;

public class EthernetTile : Tile
{

    [Header("Parametres de la tile d'internet")]
    [SerializeField] private double maxBandwidth;
    public override void Tick()
    {

    }
    public double getMaxBandwidth() => maxBandwidth;
   
}
