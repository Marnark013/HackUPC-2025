using UnityEngine;

public class PacketGenerator : MonoBehaviour
{
    [Header("Packet Generation Settings")]
    [Range(0f, 1f)]
    [SerializeField] private float packetSendProbability = 0.05f; // Probability to send packet each tick (20tps)
    [SerializeField] private int tickingCooldown = 10; // Cooldown between packet sends in ticks

    [Header("Scaling")]
    [SerializeField] private float scalingFactor = 0.01f; // Scaling factor for the packet size

    [Header("Probabilities")]
    [SerializeField] private float persistentProbability = 0.5f; // Probability of packet being persistent
    [SerializeField] private float totalWork = 2048f; // Total work done in the system (MFLOPS + Mbytes)
    [SerializeField] private float workVariance = 512f; // Amount of variance in work done
    [Header("Split Bias")]
    [Tooltip("-1 = all compute, 0 = uniform, +1 = all storage")]
    [Range(-1f, 1f)]
    public float splitBias = 0f;

    private float _scaling = 1.0f;
    private int _tickCooldown = 0;

    void Start()
    {
        if(TimeController.Instance != null)
        {
            TimeController.Instance.OnTick += TickPacket;
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
            TimeController.Instance.OnTick -= TickPacket;
        }
    }

    private void TickPacket(float delta)
    {
        if(Random.value < packetSendProbability && _tickCooldown <= 0)
        {
            sendPacket();
            Debug.Log("Packet sent!");
            _tickCooldown = tickingCooldown;
        }
        else if (_tickCooldown > 0)
        {
            _tickCooldown--;
        }
    }

    private void sendPacket()
    {
        bool persistent = Random.value < persistentProbability; // Randomly decide if the packet is persistent

        float _totalWork = Random.Range(totalWork - workVariance, totalWork + workVariance); // Randomly decide the total work done in the system
        _totalWork *= _scaling; // Scale the total work done in the system
        _totalWork = Mathf.Max(_totalWork, 0); // Ensure total work is not negative
        _scaling += scalingFactor; // Increase the scaling factor

        float r = Random.value; // Random value for scaling

        if (splitBias > 0f)
            r = Mathf.Lerp(r, 1f, splitBias);
        else if (splitBias < 0f)
            r = Mathf.Lerp(r, 0f, -splitBias);

        float _storageSize = _totalWork * r; // Scale the data size
        float _computeSize = _totalWork * (1-r); // Scale the compute size

        Packet p = new(_storageSize, _computeSize, persistent); // Create a new packet 
        LevelDataManager.Instance.PacketQueue.Enqueue(p);
    }
}
