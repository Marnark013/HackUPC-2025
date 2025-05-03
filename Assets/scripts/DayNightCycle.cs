using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class DayNightCycle : MonoBehaviour
{
    [Header("References")]
    [Tooltip("Must be a fullscreen Image in a Canvas set to Screen Space - Overlay")]
    [SerializeField] private Image overlayImage;

    [Tooltip("Your TimeController instance")]
    [SerializeField] private TimeController timeController;

    [Header("Day/Night Colors")]
    [Tooltip("0 = midnight, 0.5 = noon, 1 = midnight")]
    [SerializeField] private Gradient dayNightGradient;

    private float dayLength; // in seconds

    private void Start()
    {

        if (timeController == null)
            timeController = TimeController.Instance;

        timeController.OnTick += HandleTick;

        // Compute how long one in-game day lasts
        dayLength = timeController.GetWeekDuration() / 7f;

        // Make sure the overlay is transparent at start
        if (overlayImage == null)
            overlayImage = GetComponent<Image>();
        overlayImage.color = dayNightGradient.Evaluate(0f);
    }



    private void OnDisable()
    {
        timeController.OnTick -= HandleTick;
    }

    private void HandleTick(float tickDelta)
    {
        // Calculate fraction of the current day (0-1)
        float dayTimer = timeController.DayTimer; // you still need to increment DayTimer in TimeController!
        float timeOfDay = Mathf.Repeat(dayTimer / dayLength, 1f);

        // Evaluate your gradient
        Color c = dayNightGradient.Evaluate(timeOfDay);

        // Apply to overlay
        overlayImage.color = c;
    }
}
