namespace RatGame;

public static class Utils
{
    public static int ToInt(bool value)
    {
        return value ? 1 : 0;
    }

    public static float DegreesToRadians(float degrees) => degrees * ((float)Math.PI) / 180f;
}