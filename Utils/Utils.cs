namespace RatGame;

public static class Constants
{
    public static int SCREEN_WIDTH = 640;
    public static int SCREEN_HEIGHT = 480;
    public static int GAME_WIDTH = 160;
    public static int GAME_HEIGHT = 120;
    public static int TILE_WIDTH = 10;
    public static int TILE_HEIGHT = 10;
}

public static class Utils
{
    public static int ToInt(bool value)
    {
        return value ? 1 : 0;
    }

    public static float DegreesToRadians(float degrees) => degrees * ((float)Math.PI) / 180f;
}