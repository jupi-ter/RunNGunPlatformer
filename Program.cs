using Raylib_cs;

namespace RatGame;

class Program
{
    static void Main()
    {
        Raylib.InitWindow(Constants.SCREEN_WIDTH, Constants.SCREEN_HEIGHT, "ratEngine");
        Raylib.SetTargetFPS(60);

        InputManager.BindKey(InputActions.Left, KeyboardKey.Left);
        InputManager.BindKey(InputActions.Right, KeyboardKey.Right);
        InputManager.BindKey(InputActions.Up, KeyboardKey.Up);
        InputManager.BindKey(InputActions.Down, KeyboardKey.Down);

        CollisionManager.Initialize();

        float halfWidth = Constants.SCREEN_WIDTH * 0.5f;
        float halfHeight = Constants.SCREEN_HEIGHT * 0.5f;

        // test only camera
        GameCamera camera = new(Constants.SCREEN_WIDTH, Constants.SCREEN_HEIGHT);
        camera.SetZoom(4f);

        LevelLoader.LoadLevel(LevelNames.Test);

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
