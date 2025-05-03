using UnityEngine;
using System.Linq;
using System.Collections.Generic;
public class CpuManager : MonoBehaviour
{
    public static CpuManager Instance { get; private set; }
    private List<CPUTile> Cpus; 
    void Awake()
    {
        if (Instance == null)
        {
            Cpus = new List<CPUTile>();
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void addCpu(CPUTile tile)
    {
        Cpus.Add(tile);
        Cpus = Cpus.OrderBy(t => t.getComputingPower()).ToList();
    }

    public bool addToCpu(int neededTrougthput)
    {
        int left = 0;
        int right = Cpus.Count - 1;

        while (left <= right)
        {
            int mid = (left + right) / 2;
            double midPower = Cpus[mid].getComputingPower();

            if (midPower == neededTrougthput)
            {
                //compute 
                return true;
            }
            else if (midPower < neededTrougthput)
                left = mid + 1;
            else
                right = mid - 1;
        }

        return false; // Not found
    }

}
   
