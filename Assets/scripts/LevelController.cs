using UnityEngine;

public class LevelController : MonoBehaviour
{
    GameManager gameManager;
    void Start()
    {
        gameManager = GameManager.Instance;
        gameManager.StartLevel();
    }

}
