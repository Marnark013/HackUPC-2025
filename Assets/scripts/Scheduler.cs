using UnityEngine;
using System.Collections.Generic;

public class Scheduler : MonoBehaviour
{
    private PowerManager powerManager;
    public double availablePower; // = getGeneratedPower();
    public double neededPower; // = getPower();
    public double routerSpeed; // = getSpeed(); 
    public double taskSpeed; // = getSpeed();
    

    private void Start(){
        powerManager = PowerManager.Instance;
        availablePower = powerManager.getGeneratedPower();
        neededPower = powerManager.coumputePowerNeeded();
    }
    // Comprova si power es suficient per el que necessiten els servers
    //if true, s'apaga tot(espavil)
    public bool IsPowerEnough()
    {
        //pesudocded eh cal fer les funcions de getPower i tal
        return availablePower >= neededPower;
    }

    public void AssignTaskToServer()
    {
        //Per fer 
        Packet packet = LevelDataManager.Instance.PacketQueue.Dequeue();
        routerSpeed = RouterManager.Instance.getBandwidth();
        taskSpeed = packet.GetComputeSize();
        CpuManager.Instance.addToCpu(taskSpeed);

    }

    // Esta el router cardat?
    //if true fer un tractament més lent
    public bool IsRouterBottleneck()
    {   
        Queue<Packet> q = LevelDataManager.Instance.PacketQueue;
        Packet packet = q.Dequeue();
        routerSpeed = RouterManager.Instance.getBandwidth();
        taskSpeed = packet.GetComputeSize();
        return taskSpeed > routerSpeed;
    }

    //Mira si hi ha algun disc o server trencat per si sha de avisar al usuari 
    //if true, (ja shauria dhaver fet abans pero bno el server o disc torna inoperatiu)
    public bool IsONEDiskBroken()
    {

        return false;
        //bool isBroken = false;
       // for(tots els servers veure si estan trencats and not isBroken){
       //     if trencat = true;
       // }
       //return isBroken;
    }


    //Mira si sha programat un global shutdown
    public bool IsGlobalcShutdown()
    {

        return false;
        //return globalShutdown;
    }


    // Main scheduler logic
    public void Schedule()
    {
        if (!IsPowerEnough())
        {
            Debug.Log("Falta energia");
            return;
        }

        if (IsRouterBottleneck())
        {
            Debug.Log("tenim bottleneck al router");
            return;
        }
       //i es van comprobant parametres ...
    }
}