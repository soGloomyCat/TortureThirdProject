using System;

public class Timer
{
    private const float TriggerValue = 1;

    public event Action Started;
    public event Action Stopped;
    public event Action Completed;
    public event Action Updated;

    public float TotalTime { get; private set; }
    public float TimeLeft { get; private set; }

    public void StartCountdown(float issueTime)
    {
        TotalTime = issueTime;
        TimeLeft = 0;
        Started?.Invoke();
    }

    public void Stop()
    {
        Stopped?.Invoke();
    }

    public void Tick(float tick)
    {
        TimeLeft += tick;
        Updated?.Invoke();

        if (TimeLeft >= TriggerValue)
        {
            TimeLeft = 0;
            Completed?.Invoke();
        }
    }
}
