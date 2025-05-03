using UnityEngine;
using System.Collections.Generic;
public class RouterManager : MonoBehaviour
{
    public static RouterManager Instance { get; private set; }
    private List<EthernetTile> routers;
    void Awake()
    {
        if (Instance == null)
        {
            routers = new List<EthernetTile>();
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void SetRouter(EthernetTile tile)
    {
        routers.Add(tile);
    }

    public double getBandwidth() 
    { 
        double Bandwidth = 0;
        routers.RemoveAll(item => item == null);
        foreach (EthernetTile tile in routers)
        {
            Bandwidth += tile.getBandwith();
        }
        return Bandwidth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
