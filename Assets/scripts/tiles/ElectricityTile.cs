using UnityEngine;

public class ElectricityTile : Tile
{

    [Header("Parametres de la tile d'energia")]
    [SerializeField] private double maxEnergy;

    public double getGeneratedEnergy() => maxEnergy;
    public override void Tick()
    {

    }
}
