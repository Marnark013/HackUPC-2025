using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }


    [SerializeField] public Queue<Packet> PacketQueue;

    public static GameObject Board;

    public float gameSpeed = 1f;

    private bool _inGame = false;

    public bool _isPaused = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            PacketQueue = new();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartLevel() 
    {
        _inGame = true;
    } 

    public void EndLevel()
    {
        _inGame = false;
    }
    public bool IsInGame()
    {
        return _inGame;
    }
    public void PauseGame()
    {
        _isPaused = true;
    }
    public void ResumeGame()
    {
        _isPaused = false;
    }
    public bool IsPaused()
    {
        return _isPaused;
    }
}

