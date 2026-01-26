using System.Numerics;
using Raylib_cs;
namespace RatGame;

public static class Renderer
{
    private static int gameWidth;
    private static int gameHeight;
    private static RenderTexture2D renderTarget;

    public static void Initialize(int width, int height)
    {
        gameWidth = width;
        gameHeight = height;
        renderTarget = Raylib.LoadRenderTexture(gameWidth, gameHeight);
    }

    public static void BeginRender()
    {
        Raylib.BeginTextureMode(renderTarget);
        Raylib.ClearBackground(Color.White);
    }

    public static void EndRender()
    {
        Raylib.EndTextureMode();

        int windowWidth = Raylib.GetScreenWidth();
        int windowHeight = Raylib.GetScreenHeight();

        float scaleX = (float)windowWidth / gameWidth;
        float scaleY = (float)windowHeight / gameHeight;
        float scale = MathF.Min(scaleX, scaleY);

        float scaledWidth = gameWidth * scale;
        float scaledHeight = gameHeight * scale;
        float offsetX = (windowWidth - scaledWidth) / 2f;
        float offsetY = (windowHeight - scaledHeight) / 2f;

        Rectangle source = new (0, 0, gameWidth, -gameHeight);
        Rectangle dest = new(offsetX, offsetY, scaledWidth, scaledHeight);

        Raylib.DrawTexturePro(
            renderTarget.Texture,
            source,
            dest,
            Vector2.Zero,
            0f,
            Color.White
        );
    }

    public static void Unload()
    {
        Raylib.UnloadRenderTexture(renderTarget);
    }
}
