using System.Reflection;

namespace RatGame;

public static class SpriteNames
{
    public static SpriteDefinition PlayerWalk = new (path: "guy_walk", name: "GuyWalk", frames: 5);
    public static SpriteDefinition PlayerIdle = new (path: "guy_idle", name: "GuyIdle", frames: 5);
    public static SpriteDefinition PlayerJump = new (path: "guy_jump", name: "GuyJump", frames: 3);
    public static SpriteDefinition Wall = new (path: "wall", name: "Wall");
}

public class SpriteDefinition(string path, string name, int frames = 1)
{
    private readonly string basePath = "assets/sprites/";
    private readonly string relativePath = path;
    public string Name = name;
    public int Frames = frames;

    public string Path => $"{basePath}{relativePath}";
}