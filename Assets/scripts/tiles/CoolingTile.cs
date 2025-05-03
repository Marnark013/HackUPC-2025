public class CoolingTile : Tile
{
    public double RefrigerationPower;
    private int rank;
    private int MaxRank;
    private double coolingEficienci;

    public int getRank()
    {
        return rank;
    }
    public double getCoolingEficienci()
    {
        return coolingEficienci;
    }
    public void upgradeRank()
    {
        if(MaxRank> rank) ++rank;
    }

    public override void Tick()
    {

    }
}
