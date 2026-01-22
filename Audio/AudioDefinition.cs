using Raylib_cs;
namespace RatGame;

public abstract class AudioDefinition(string name, string relativePath)
{
    public string Name { get; } = name;
    // this path doesn't exist yet. todo: fix
    public string Path { get; } = "assets/audio/" + relativePath;
}

public class SoundDefinition(string name, string relativePath, float defaultVolume = 1.0f) : AudioDefinition(name, relativePath)
{
    public float DefaultVolume { get; set; } = defaultVolume;
}

public class MusicDefinition(string name, string relativePath, bool loop = true, float defaultVolume = 0.5f) : AudioDefinition(name, relativePath)
{
    public bool Loop { get; set; } = loop;
    public float DefaultVolume { get; set; } = defaultVolume;
}
