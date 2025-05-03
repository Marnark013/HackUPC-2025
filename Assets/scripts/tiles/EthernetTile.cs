public class EthernetTile : Tile
{
    public double bandwith;
    public void upgraderooter(double upgrade)
    {
        this.bandwith += upgrade;
    }
    public double getBandwith() {  return bandwith; }
    public override void Tick()
    {

    }
}
