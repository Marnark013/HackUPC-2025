public class DiskTile : Tile
{
    public double MaxData;
    public double ActualData;
    public int Units;
    public DisKTypes Type;
    public int SpaceProvided;

    public override void Tick()
    {

    }
}

public enum DisKTypes
{
    HDD,
    SSD,
    NVME,
    TAPE
}