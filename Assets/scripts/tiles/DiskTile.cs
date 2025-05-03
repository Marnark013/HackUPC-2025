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

    public int setData(int data) { 
        if(ActualData + data > MaxData)
        {
            int aux = ActualData
            ActualData = MaxData;
            return data -(MaxData - aux);
        }
        else
        {
            ActualData += data;
            return 0;

        }
    }
}



public enum DisKTypes
{
    HDD,
    SSD,
    NVME,
    TAPE
}