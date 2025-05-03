using UnityEngine;

public class TextHover : MonoBehaviour
{
    [Tooltip("How far (in units) the text moves up and down")]
    public float amplitude = 5f;
    [Tooltip("How fast the text oscillates")]
    public float frequency = 1f;

    Vector3 _startPos;

    void Awake()
    {
        // Remember the starting position
        _startPos = transform.localPosition;
    }

    void Update()
    {
        // Compute new Y offset using a sine wave
        float yOffset = Mathf.Sin(Time.time * frequency * Mathf.PI * 2f) * amplitude;
        // Apply it on top of the start position
        transform.localPosition = _startPos + new Vector3(0f, yOffset, 0f);
    }
}
