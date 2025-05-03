using UnityEngine;

public class Packet : MonoBehaviour
{
    private static float _storageSize;
    private static float _computeSize;
    private static bool _persistent;

    public Packet(float storageSize, float computeSize, bool persistent)
    {
        _storageSize = storageSize;
        _computeSize = computeSize;
        _persistent = persistent;
    }

    public float GetStorageSize()
    {
        return _storageSize;
    }

    public float GetComputeSize()
    {
        return _computeSize;
    }

    public bool IsPersistent()
    {
        return _persistent;
    }
}
