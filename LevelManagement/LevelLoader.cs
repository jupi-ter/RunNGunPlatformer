using System.Numerics;
namespace RatGame;

public static class LevelNames
{
    public static LevelDefinition Test = new("test", "Test");
}

public class LevelDefinition(string path, string name)
{
    private const string basePath = "assets/levels/";
    private const string fileExtension = ".rat";
    private string relativePath = path;
    public string Name = name;
    public string Path => $"{basePath}{relativePath}{fileExtension}";
}

/*
.rat format specification

# Only specify non-empty tiles
# format: alias x y

p 320 240
w 470 640
w 200 300
e 400 200
c 150 350
*/

public static class LevelLoader
{
    private static List<Entity> currentLevelEntities = [];
    public static Entity CreateEntity(char alias, Vector2 pos)
    {
        return alias switch
        {
            'p' => new Player(pos),
            'w' => new Wall(pos),
            _ => throw new Exception($"Unknown alias: {alias}")
        };
    }

    public static void LoadLevel(LevelDefinition level)
    {
        // read file
        string[] lines = File.ReadAllLines(level.Path);

        // parse file
        // here we can parse any metadata we may need.
        // currently i will assume every file has only necessary data.

        for (int i = 0; i < lines.Length; i++)
        {
            String[] currentLine = lines[i].Split(' '); 

            if (currentLine.Length != 3)
            {
                // console doesn't have a Console.Warning, Console.Error excepts and terminates the program,
                // and writing exceptions is tiresome.
                Console.WriteLine($"[WARNING] Expected 3 values (alias x y), got {currentLine.Length} at line {i+1}");
                continue;
            }

            try
            {
                char alias = (currentLine[0])[0];
                float x = float.Parse(currentLine[1]);
                float y = float.Parse(currentLine[2]);
                Vector2 position = new(x,y);

                // instantiate level entities
                Entity entity = CreateEntity(alias, position);
                EntityManager.Register(entity);
                currentLevelEntities.Add(entity);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }

    public static void UnloadLevel()
    {
        // clear references and set up for destruction.
        // i don't do it right now because we need collision destructors,
        // so we don't have phantom collisions in the manager.
    }
}
