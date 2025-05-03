using UnityEngine;

public abstract class Tile : MonoBehaviour
{
    public Vector2Int GridPosition { get; private set; }
    public double NeededEnergy;
    public string Name;
    public int FixCost;
    public int price;
    public virtual double EnergyConcumption()
    {
        return NeededEnergy;
    }
    
    public virtual void Initialize(Vector2Int gridPosition)
    {
        GridPosition = gridPosition;
    }

    void Start()
    {
        if (TimeController.Instance != null)
        {
            TimeController.Instance.OnTick += HandleTick;
        }
        else
        {
            Debug.LogError("TimeController instance is null. Make sure it is initialized before PacketGenerator.");
        }
    }

    private void OnDisable()
    {
        if (TimeController.Instance != null)
        {
            TimeController.Instance.OnTick -= HandleTick;
        }
    }

    private void HandleTick(float delta)
    {
        Tick();
    }

    public abstract void Tick(); // Called every frame or game update
}
