using UnityEngine;

[RequireComponent(typeof(Transform))]
public class WatchSpinner : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private TimeController timeController;  // assign in inspector or auto-find

    private float weekDuration;             // length of a week in seconds

    private void Start()
    {
        // If you forgot to assign it in the Inspector, grab the singleton
        TimeController timeController = TimeController.Instance;
        // Expose the configured weekDuration from your TimeController
        weekDuration = timeController.GetWeekDuration();
    }

    private void Update()
    {
        // Only spin when the game is running
        if (!GameManager.Instance.IsInGame() || GameManager.Instance.IsPaused())
            return;

        // Get the elapsed time *within* the current week
        float currentWeekTime = timeController.WeekTimer;

        // Compute rotation: fraction of the week × 360°
        float fractionOfWeek = Mathf.Clamp01(currentWeekTime / weekDuration);
        float rotationZ = fractionOfWeek * 360f;

        transform.rotation = Quaternion.Euler(0f, 0f, -rotationZ);
    }
}
