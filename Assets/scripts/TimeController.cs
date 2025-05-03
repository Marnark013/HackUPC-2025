using System;
using UnityEngine;
using UnityEngine.Events;

[DisallowMultipleComponent]
public class TimeController : MonoBehaviour
{
    // accumulator for our custom tick
    private float _tickAccumulator;
    private float _tickInterval;

    [Tooltip("Duration of one in-game week, in real-time seconds.")]
    [SerializeField] private float weekDuration = 60f;


    [SerializeField] private float elapsedTime;
    [SerializeField] private float dayTimer;
    [SerializeField] private float weekTimer;

    public event Action<float> OnTick;
    [Tooltip("How many ticks do we want per second?")]
    [SerializeField] private float ticksPerSecond = 20f;
    public static TimeController Instance { get; private set; }

    public float ElapsedTime
    {
        get => elapsedTime;
        private set => elapsedTime = value;
    }

    public float DayTimer
    {
        get => dayTimer;
        private set => dayTimer = value;
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
            _tickInterval = 1f / ticksPerSecond;
            _tickAccumulator = 0f;
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
            DayTimer += delta;
            WeekTimer += delta;

            OnTimeUpdated?.Invoke(ElapsedTime);

            _tickAccumulator += delta;
            while (_tickAccumulator >= _tickInterval)
            {
                OnTick?.Invoke(_tickInterval);
                _tickAccumulator -= _tickInterval;
            }

            if (DayTimer >= weekDuration/7f)
            {
                DayTimer -= weekDuration/7f;
            }

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
        _tickAccumulator = 0f;
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
