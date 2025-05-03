public class CPUTile : Tile
{
    public double Eficiency;
    public double SpaceNeeded;
    public int units;
    public CpuUsage Usage;
    public override void Tick()
    {

    }
}
public enum CpuUsage
{
    Virtualitzation,
    AiTraining
}