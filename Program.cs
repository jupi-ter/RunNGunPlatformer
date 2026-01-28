using Raylib_cs;

namespace RatGame;

class Program
{
    static void Main()
    {
        Raylib.InitWindow(Constants.SCREEN_WIDTH, Constants.SCREEN_HEIGHT, "ratEngine");
        Raylib.SetTargetFPS(60);

        Renderer.Initialize(Constants.GAME_WIDTH, Constants.GAME_HEIGHT);

        InputManager.BindKey(InputActions.Left, KeyboardKey.Left);
        InputManager.BindKey(InputActions.Right, KeyboardKey.Right);
        InputManager.BindKey(InputActions.Up, KeyboardKey.Up);
        InputManager.BindKey(InputActions.Down, KeyboardKey.Down);
        InputManager.BindKey(InputActions.Shoot, KeyboardKey.Z);

        CollisionManager.Initialize();

        float halfWidth = Constants.GAME_WIDTH * 0.5f;
        float halfHeight = Constants.GAME_HEIGHT * 0.5f;

        // test only camera
        GameCamera camera = new(Constants.GAME_WIDTH, Constants.GAME_HEIGHT);
        //camera.SetZoom(4f);

        LevelLoader.LoadLevel(LevelNames.Test);

        bool runOnce = true;

        while (!Raylib.WindowShouldClose())
        {
            // update 
            TimerManager.Update();
            EntityManager.UpdateAll();
            CollisionManager.Update();

            if (runOnce)
            {
                var player = EntityManager.GetFirstInstanceOf<Player>();
                if (player != null)
                {
                    player.CurrentWeapon = new Machinegun();
                    player.CurrentWeapon.Offset = new(5, 4);
                }
                runOnce = false;
            }
            camera.Follow(new(halfWidth, halfHeight));
            camera.Update();
            EntityManager.Cleanup();
            // draw
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Black);

            Renderer.BeginRender();
            camera.Begin();
            EntityManager.DrawAll();
            //CollisionManager.DebugDrawColliders();
            camera.End();

            //screen space, draw ui here
            Renderer.EndRender();
            Raylib.EndDrawing();
        }

        SpriteManager.UnloadAllSprites();        
        Raylib.CloseWindow();
    }
}
