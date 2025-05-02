using UnityEngine;

public abstract class Tile : MonoBehaviour
{
    public Vector2Int GridPosition { get; private set; }

    public virtual void Initialize(Vector2Int gridPosition)
    {
        GridPosition = gridPosition;
    }

    public abstract void Tick(); // Called every frame or game update
}
