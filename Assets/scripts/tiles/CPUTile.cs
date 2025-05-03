public class CPUTile : Tile
{
    public double Eficiency;
    private double TrowhtputUsed;
    private double PercapacityUsed;
    private double cpuVariableEnergyConsume;
    public double SpaceNeeded;
    private double trowhtput;
    public int units;
    public CpuUsage Usage;
    public override void Tick()
    {

    }

    public double getTrowhtputUsed()
    {
        return TrowhtputUsed;
    }
    public double getFreeTrowhtput()
    {
        return trowhtput - TrowhtputUsed;
    }
    public override double EnergyConcumption()
    {
        double energy = PercapacityUsed * cpuVariableEnergyConsume;
        return base.EnergyConcumption() + energy;
    }
    public double getTrowhtput()
    {
        return trowhtput;
    }
}
public enum CpuUsage
{
    Virtualitzation,
    AiTraining
}
