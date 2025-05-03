using UnityEngine;

public class CPUTile : Tile
{

    [Header("Parametres de la tile de computo")]
    [SerializeField] private double efficiency;
    [SerializeField] private double percapacityUsed;
    [SerializeField] private double cpuVariableEnergyConsume;
    [SerializeField] private double computingPower;

    public override void Tick()
    {

    }

    public override double EnergyConcumption()
    {
        double energy = percapacityUsed * cpuVariableEnergyConsume;
        return base.EnergyConcumption() + energy;
    }
    public double getComputingPower() => computingPower;
}
    public enum CpuUsage
{
    Virtualitzation,
    AiTraining
}