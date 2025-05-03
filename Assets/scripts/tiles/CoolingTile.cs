using UnityEngine;

public class CoolingTile : Tile
{

    [Header("Parametres de la tile de refrigeració")]
    [SerializeField] private double refrigerationCapacity;
    [SerializeField] private int range;
    [SerializeField] private double coolingEfficiency;

    public int getRank()
    {
        return range;
    }
    
    public double getRefrigerationCapacity() => refrigerationCapacity;
    public double getCoolingEfficiency() => coolingEfficiency;
    public int getRange() => range;


    public override void Tick()
    {

    }
}
