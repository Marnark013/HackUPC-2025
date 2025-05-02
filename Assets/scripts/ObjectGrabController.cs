using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ObjectGrabController : MonoBehaviour
{

    [Tooltip("How fast the object moves toward the mouse (higher = snappier)")]
    public float smoothSpeed = 10f;
    public bool snapToGrid = true;
    public bool grababble = true;

    Camera _cam;
    private bool _grabbed;

    private void Awake()
    {
        _cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (_grabbed)
        {
            Vector3 mouseScreen = Input.mousePosition;
            mouseScreen.z = Mathf.Abs(_cam.transform.position.z - transform.position.z);
            Vector3 targetPos = _cam.ScreenToWorldPoint(mouseScreen);
            transform.position = targetPos;

            transform.position = Vector3.Lerp(
                transform.position,
                targetPos,
                0.1f * Time.deltaTime
            );
        }
    }

    private void snap()
    {
        Vector3 pos = transform.position;
        pos.x = Mathf.Round(pos.x);
        pos.y = Mathf.Round(pos.y);
        transform.position = pos;
    }

    void OnMouseDown()
    {
        if(grababble) _grabbed = true;
    }

    private void OnMouseUp()
    {
        _grabbed = false;
        if (snapToGrid) snap();
    }
}
