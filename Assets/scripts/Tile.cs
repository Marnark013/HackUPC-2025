using UnityEngine;

public abstract class Tile : MonoBehaviour
{

    public Vector2Int GridPosition { get; private set; }
    [Header("Settings generals d'una Tile")]
    [SerializeField] private double baseEnergy;
    [SerializeField] private string tileName;
    [SerializeField] private int operationCost;
    [SerializeField] private int basePrice;
    [SerializeField] private float temperature;



    public double getBaseEnergy() => baseEnergy;
    public string getTileName() => tileName;
    public int getOperationCost() => operationCost;
    public int getBasePrice() => basePrice;
    public float getTemperature() => temperature;
    public virtual double EnergyConcumption()
    {
        return baseEnergy;
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
