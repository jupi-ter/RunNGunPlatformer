using Raylib_cs;
using System.Numerics;
namespace RatGame;

public class GameCamera
{
    private Vector2 position = Vector2.Zero;
    private Vector2 offset;
    private float rotationDegrees = 0f;
    private float zoom = 1f;
    private float screenshake = 0f;
    private Camera2D raylibCamera;
    private Random random = new();

    public GameCamera(int screenWidth, int screenHeight)
    {
        offset = new Vector2(screenWidth / 2, screenHeight / 2);

        raylibCamera = new()
        {
            Offset = offset,
            Target = position,
            Rotation = rotationDegrees,
            Zoom = zoom
        };
    }

    public void AddShake(float amount)
    {
        screenshake += amount;
    }

    /*
    revise this, do we also want to always lerp zoom towards 1?
    not doing that leads to a lerp coroutine.
    */
    public void SetZoom(float amount)
    {
        zoom = amount;
    }

    public void Update()
    {
        if (screenshake >= 10f)
            screenshake *= 0.8f;
        if (screenshake > 0f)
            screenshake--;
        else
            screenshake = 0f;

        Vector2 shakeOffset = Vector2.Zero;

        if (screenshake > 0f)
        {
            float shakeX = ((float)random.NextDouble() * screenshake) - screenshake / 2f;
            float shakeY = ((float)random.NextDouble() * screenshake) - screenshake / 2f;
            shakeOffset = new Vector2(shakeX, shakeY);
        }

        raylibCamera.Target = position;
        raylibCamera.Offset = offset;
        raylibCamera.Rotation = rotationDegrees;
        raylibCamera.Zoom = zoom;
    }

    public void Begin()
    {
        Raylib.BeginMode2D(raylibCamera);
    }

    public void End()
    {
        Raylib.EndMode2D();
    }
    
    public void Follow(Vector2 targetPosition, float smoothing = 0.1f)
    {
        position = Vector2.Lerp(position, targetPosition, smoothing);
    }
}