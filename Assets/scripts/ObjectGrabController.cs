using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ObjectGrabController : MonoBehaviour
{
    [Header("Drag Settings")]
    [Tooltip("How fast the object chases the mouse (higher = snappier)")]
    public float smoothSpeed = 10f;
    public bool grabbable = true;

    [Header("Snap Settings")]
    [Tooltip("Whether to snap to integer grid on release")]
    public bool snapToGrid = true;
    [Tooltip("How long the snap animation lasts (seconds)")]
    public float snapDuration = 0.2f;

    Camera _cam;
    bool _grabbed;
    Vector3 _snapTarget;
    Coroutine _snapRoutine;

    void Awake()
    {
        _cam = Camera.main;
    }

    void OnMouseDown()
    {
        if (!grabbable) return;
        _grabbed = true;

        if (_snapRoutine != null)
        {
            StopCoroutine(_snapRoutine);
            _snapRoutine = null;
        }
    }

    void OnMouseUp()
    {
        _grabbed = false;
        if (snapToGrid)
        {
            Vector3 p = transform.position;
            _snapTarget = new Vector3(
                Mathf.Round(p.x),
                Mathf.Round(p.y),
                p.z
            );
            _snapRoutine = StartCoroutine(SmoothSnap());
        }
    }

    void Update()
    {
        if (_grabbed)
        {
            Vector3 screenPos = Input.mousePosition;
            screenPos.z = Mathf.Abs(_cam.transform.position.z - transform.position.z);
            Vector3 targetPos = _cam.ScreenToWorldPoint(screenPos);

            transform.position = Vector3.Lerp(
                transform.position,
                targetPos,
                smoothSpeed * Time.deltaTime
            );
        }
    }

    IEnumerator SmoothSnap()
    {
        Vector3 startPos = transform.position;
        float elapsed = 0f;

        while (elapsed < snapDuration)
        {
            transform.position = Vector3.Lerp(
                startPos,
                _snapTarget,
                elapsed / snapDuration
            );
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = _snapTarget;
        _snapRoutine = null;
    }
}
