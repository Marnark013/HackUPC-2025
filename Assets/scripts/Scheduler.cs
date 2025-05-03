using UnityEngine;

public class Scheduler : MonoBehaviour
{
    public float availablePower; // = getGeneratedPower();
    public float neededPower; // = getPower();
    public float routerSpeed; // = getSpeed(); 
    public float taskSpeed; // = getSpeed();

    // Comprova si power es suficient per el que necessiten els servers
    //if true, s'apaga tot(espavil)
    public bool IsPowerEnough()
    {
        //pesudocded eh cal fer les funcions de getPower i tal
        return availablePower >= neededPower;
    }

    // Esta el router cardat?
    //if true fer un tractament més lent
    public bool IsRouterBottleneck()
    {
        // Pseudocoded, again cal fer get speed...
        //POtser avisar que es necessiten mes routers o es fara un tractament
        //mes lent
        //hem dit de mirar una cua on guardem els paquets que arriben (en principi només un)
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