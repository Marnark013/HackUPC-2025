using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class LevelDataManager : MonoBehaviour
{
    public static LevelDataManager Instance { get; private set; }

    public Queue<Packet> PacketQueue;

    public float CurrentMoney;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            PacketQueue = new();
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
