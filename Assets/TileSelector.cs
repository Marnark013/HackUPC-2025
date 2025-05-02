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
            Tile newTile = Instantiate(_selectedPrefab, transform.position, Quaternion.identity);
            newTile.Initialize(new Vector2Int(0, 0)); // Replace with actual grid position
            ObjectGrabController grabController = newTile.GetComponent<ObjectGrabController>();
            if (grabController != null)
            {
                grabController.SetGrabbed(); // Make sure the tile is grabbable
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
