namespace RatGame;

public static class TimerManager
{
    private static SwapbackList<Timer> currentTimers = new();

    public static void Update()
    {
        if (currentTimers.Count < 1) return;

        for (int i = 0; i < currentTimers.Count; i++)
        {
            var timer = currentTimers[i];
            timer.Increment();
            if (timer.Completed)
                currentTimers.RemoveAt(i);
                i--;
        }
    }

    public static void AddTimer(Timer timer)
    {
        currentTimers.Add(timer);
    }
}

public class Timer
{
    private int frameTarget;
    private int counter;
    public bool Completed => counter >= frameTarget;
    private Action onComplete;

    public Timer(int frameTarget, Action action)
    {
        this.frameTarget = frameTarget;
        onComplete = action;
    }

    public void Increment()
    {
        if (Completed) return;

        if (counter < frameTarget)
            counter++;

        if (Completed)
            onComplete();
    }
}