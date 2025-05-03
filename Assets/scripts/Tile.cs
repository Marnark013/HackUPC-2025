using UnityEngine;

public abstract class Tile : MonoBehaviour
{
    public Vector2Int GridPosition { get; private set; }
    public double EnergyGenerator;
    public double NeededEnergy;
    public string Name;
    public int FixCost;
    public int price;
    public virtual double EnergyConcumption()
    {
        return EnergyGenerator - NeededEnergy;
    }
    
    public virtual void Initialize(Vector2Int gridPosition)
    {
        GridPosition = gridPosition;
    }

    public abstract void Tick(); // Called every frame or game update
}
