using UnityEngine;
using UnityEngine.Events;

[DisallowMultipleComponent]
public class TimeController : MonoBehaviour
{
    [Tooltip("Duration of one in-game week, in real-time seconds.")]
    [SerializeField] private float weekDuration = 60f;


    [SerializeField] private float elapsedTime;
    [SerializeField] private float weekTimer;
    public static TimeController Instance { get; private set; }

    public float ElapsedTime
    {
        get => elapsedTime;
        private set => elapsedTime = value;
    }

    public float WeekTimer
    {
        get => weekTimer;
        private set => weekTimer = value;
    }

    public UnityEvent<float> OnTimeUpdated;

    public UnityEvent OnWeekFinished;


    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void Update()
    {
        if (GameManager.Instance.IsInGame() && !GameManager.Instance.IsPaused())
        {
            float delta = Time.deltaTime * GameManager.Instance.gameSpeed;

            ElapsedTime += delta;
            WeekTimer += delta;

            OnTimeUpdated?.Invoke(ElapsedTime);

            if (WeekTimer >= weekDuration)
            {
                WeekTimer -= weekDuration;
                OnWeekFinished?.Invoke();
            }
        }
    }

    public void ResetTimers()
    {
        ElapsedTime = 0f;
        WeekTimer = 0f;
        OnTimeUpdated?.Invoke(ElapsedTime);
    }


   public float GetElapsedTime()
    {
        return ElapsedTime;
    }
    public float GetWeekTimer()
    {
        return WeekTimer;
    }
    public int GetCurrentWeek()
    {
        return Mathf.FloorToInt(ElapsedTime / weekDuration);
    }

    public float GetWeekDuration()
    {
        return weekDuration;
    }
}
