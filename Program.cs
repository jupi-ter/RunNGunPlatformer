using System.Numerics;
using System.Runtime.CompilerServices;
using Raylib_cs;

namespace RatGame;

class Program
{
    private const int screenWidth = 640;
    private const int screenHeight = 480;

    static void Main()
    {
        Raylib.InitWindow(screenWidth, screenHeight, "ratEngine");
        Raylib.SetTargetFPS(60);

        InputManager.BindKey(InputActions.Left, KeyboardKey.Left);
        InputManager.BindKey(InputActions.Right, KeyboardKey.Right);
        InputManager.BindKey(InputActions.Up, KeyboardKey.Up);
        InputManager.BindKey(InputActions.Down, KeyboardKey.Down);

        CollisionManager.Initialize();

        float halfWidth = screenWidth * 0.5f;
        float halfHeight = screenWidth * 0.5f;

        // test only camera
        GameCamera camera = new(screenWidth, screenHeight);
        camera.SetZoom(4f);

        Player player = new(position: new Vector2(halfWidth, halfHeight - 30f));
        Wall wall = new(position: new Vector2(halfWidth, halfHeight + 10f));

        while (!Raylib.WindowShouldClose())
        {
            // Update
            EntityManager.UpdateAll();
            CollisionManager.Update();

            camera.Follow(new(halfWidth, halfHeight));
            camera.Update();
            EntityManager.Cleanup();
            // Draw
            Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.RayWhite);

                //world space                
                camera.Begin();
                EntityManager.DrawAll();
                camera.End();

                //screen space, draw ui here

            Raylib.EndDrawing();
        }

        SpriteManager.UnloadAllSprites();        
        Raylib.CloseWindow();
    }
}