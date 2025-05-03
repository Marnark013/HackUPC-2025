public class CPUTile : Tile
{
    public double Eficiency;
    public double SpaceNeeded;
    private double trowhtput;
    public int units;
    public CpuUsage Usage;
    public override void Tick()
    {

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