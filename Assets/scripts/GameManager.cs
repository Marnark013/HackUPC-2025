using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public static GameObject Board;

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
            Debug.LogError("Board not found in the scene.");
        }
        else
        {
            Debug.Log("Board found: " + Board.name);
        }
    }
}
