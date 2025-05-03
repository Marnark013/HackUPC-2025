public class ElectricityTile : Tile
{
    private double EnergyKWh;
    public void setEnergy(double energy)
    {
        EnergyKWh = energy;
    }
    public double getGeneratedEnergy() {  return EnergyKWh; }
    public override void Tick()
    {

    }
}
