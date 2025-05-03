using Unity.VisualScripting;
using UnityEngine;

public class DiskTile : Tile
{

    [Header("Parametres de la tile de disc")]
    [SerializeField] private double maxCapacity;
    [SerializeField] private double currentUsage;
    [SerializeField] private double ioSpeed;
    [SerializeField] private DisKTypes type;

    public override void Tick()
    {

    }
    public double getMaxCapacity() => maxCapacity;
    public double getCurrentUsage() => currentUsage;
    public double getIoSpeed() => ioSpeed;
}

    public enum DisKTypes
{
    HDD,
    SSD,
    NVME,
    TAPE
}