using Raylib_cs;

namespace RatGame;

public static class SpriteManager
{
    //public SpriteManager() {}
    private static Dictionary<string, Sprite> loadedSprites = new();

    public static Sprite GetSprite(SpriteDefinition sprDef)
    {
        return LoadSprite(sprDef.Name, $"{sprDef.Path}.png");
    }

    private static Sprite LoadSprite(string name, string path)
    {
        if (loadedSprites.TryGetValue(name, out Sprite? value))
        {
            return value;
        }

        Texture2D texture = Raylib.LoadTexture(path);
        Sprite sprite = new Sprite(texture, name);
        loadedSprites[name] = sprite;
        return sprite;
    }

    public static Sprite[] GetSprites(SpriteDefinition sprDef)
    {
        Sprite[] sprites = new Sprite[sprDef.Frames];

        for (int i = 0; i < sprDef.Frames; i++)
        {
            string frameName = $"{sprDef.Name}_{i}";
            string framePath = $"{sprDef.Path}_{i}.png";

            sprites[i] = LoadSprite(frameName, framePath);
        }

        return sprites;
    }

    public static void UnloadSprite(SpriteDefinition sprDef)
    {
        if (loadedSprites.ContainsKey(sprDef.Name))
        {
            Raylib.UnloadTexture(loadedSprites[sprDef.Name].Texture);
            loadedSprites.Remove(sprDef.Name);
        }
    }

    public static void UnloadAllSprites()
    {
        foreach (var sprite in loadedSprites.Values)
        {
            Raylib.UnloadTexture(sprite.Texture);
        }

        loadedSprites.Clear();    
    }

}

public class Sprite(Texture2D texture, string name)
{
    public Texture2D Texture = texture;
    public string Name = name;

    //getter methods of sprite width and height needed, extract from texture.
}