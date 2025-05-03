using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    [Header("Pan Settings")]
    [Tooltip("How fast the camera moves in response to drag.")]
    [SerializeField, Min(0f)]
    private float panSensitivity = 0.1f;

    [Tooltip("How quickly the camera interpolates to the target.")]
    [SerializeField, Min(0f)]
    private float smoothSpeed = 5f;

    [Header("Zoom Settings")]
    [Tooltip("Zoom speed multiplier for scroll wheel.")]
    [SerializeField, Min(0f)]
    private float zoomSpeed = 5f;

    [Tooltip("Minimum zoom: orthographic size or field of view.")]
    [SerializeField, Min(0f)]
    private float minZoom = 5f;

    [Tooltip("Maximum zoom: orthographic size or field of view.")]
    [SerializeField, Min(0f)]
    private float maxZoom = 20f;

    private Camera cam;
    private Vector3 dragOrigin;
    private Vector3 targetPosition;
    private float targetZoom;

    private void Awake()
    {
        cam = GetComponent<Camera>();
        targetPosition = transform.position;
        // Initialize zoom target from current camera setting
        targetZoom = cam.orthographic
            ? cam.orthographicSize
            : cam.fieldOfView;
    }

    private void Update()
    {
        HandlePanning();
        HandleZoom();
    }

    private void LateUpdate()
    {
        // Smoothly interpolate position
        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            smoothSpeed * Time.deltaTime
        );

        // Smoothly interpolate zoom
        if (cam.orthographic)
        {
            cam.orthographicSize = Mathf.Lerp(
                cam.orthographicSize,
                targetZoom,
                smoothSpeed * Time.deltaTime
            );
        }
        else
        {
            cam.fieldOfView = Mathf.Lerp(
                cam.fieldOfView,
                targetZoom,
                smoothSpeed * Time.deltaTime
            );
        }
    }

    private void HandlePanning()
    {
        if (Input.GetMouseButtonDown(1))
        {
            dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(1))
        {
            Vector3 currentWorldPos = cam.ScreenToWorldPoint(Input.mousePosition);
            Vector3 difference = dragOrigin - currentWorldPos;
            targetPosition += difference * panSensitivity;
        }
    }

    private void HandleZoom()
    {
        float scroll = Input.mouseScrollDelta.y;
        if (Mathf.Approximately(scroll, 0f))
            return;

        // Calculate new zoom target
        targetZoom -= scroll * zoomSpeed;

        // Clamp to bounds
        targetZoom = Mathf.Clamp(targetZoom, minZoom, maxZoom);
    }
}
