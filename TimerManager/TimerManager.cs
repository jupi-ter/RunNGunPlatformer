namespace RatGame;

public static class TimerManager
{
    private static List<Timer> currentTimers = [];

    public static void Update()
    {
        if (currentTimers.Count < 1) return;

        for (int i = currentTimers.Count - 1; i >= 0; i--)
        {
            var timer = currentTimers[i];
            timer.Increment();
        }
        currentTimers.RemoveAll(t => t.Completed);
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