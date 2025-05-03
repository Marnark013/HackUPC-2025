using UnityEngine;

public class TileSelector : MonoBehaviour
{
    public enum TileType { Electricity, Cooling, Network, Processing, Storage }

    [Header("Assign your tile prefabs here")]
    public Tile electricityPrefab;
    public Tile coolingPrefab;
    public Tile networkPrefab;
    public Tile processingPrefab;
    public Tile storagePrefab;

    private Tile _selectedPrefab;

    // Call this from your Button onClick
    public void SelectElectricity() => SetSelection(TileType.Electricity);
    public void SelectCooling() => SetSelection(TileType.Cooling);
    public void SelectNetwork() => SetSelection(TileType.Network);
    public void SelectProcessing() => SetSelection(TileType.Processing);
    public void SelectStorage() => SetSelection(TileType.Storage);

    private void SetSelection(TileType type)
    {
        switch (type)
        {
            case TileType.Electricity: _selectedPrefab = electricityPrefab; break;
            case TileType.Cooling: _selectedPrefab = coolingPrefab; break;
            case TileType.Network: _selectedPrefab = networkPrefab; break;
            case TileType.Processing: _selectedPrefab = processingPrefab; break;
            case TileType.Storage: _selectedPrefab = storagePrefab; break;
        }

        Debug.Log($"Selected tile: {type}");
        InstantiateSelected();

    }

    private void InstantiateSelected()
    {
        if (_selectedPrefab != null)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = Camera.main.nearClipPlane;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            int gridX = Mathf.RoundToInt(worldPosition.x);
            int gridY = Mathf.RoundToInt(worldPosition.y);
            Vector2Int gridPosition = new Vector2Int(gridX, gridY);

            Tile newTile = Instantiate(_selectedPrefab, new Vector3(gridX, gridY, 0), Quaternion.identity);
            newTile.Initialize(gridPosition);

            ObjectGrabController grabController = newTile.GetComponent<ObjectGrabController>();
            if (grabController != null)
            {
                if (grabController.grabbable)
                {
                    grabController.SetGrabbed();
                }
            }
            else
            {
                Debug.LogWarning("Tile prefab does not have ObjectGrabController component!");
            }
        }
        else
        {
            Debug.LogWarning("No tile selected!");
        }
    }
}
