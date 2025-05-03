using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public static GameObject Board;

    public float gameSpeed = 1f;

    public float totalPowerDraw = 0f;

    public float totalCoolingDraw = 0f;

    public float totalNetworkDraw = 0f;

    private bool _inGame = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Board = GameObject.Find("Board");
        if (Board == null)
        {
            _inGame = false;
        }
        else
        {
            _inGame = true;
        }
    }


    private void FixedUpdate()
    {
        if (_inGame)
        {
            ;
        }
    }
}

