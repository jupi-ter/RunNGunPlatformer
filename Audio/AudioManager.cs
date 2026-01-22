using Raylib_cs;
namespace RatGame;

public static class AudioManager
{
    private static Dictionary<string, Sound> sounds = [];
    private static Dictionary<string, Music> musicTracks = [];
    private static Music? currentMusic = null;

    private static float masterVolume = 1f;
    private static float soundVolume = 1f;
    private static float musicVolume = 1f;

    public static void Initialize()
    {
        Raylib.InitAudioDevice();
        Raylib.SetMasterVolume(masterVolume);
    }

    public static Sound GetSound(SoundDefinition def)
    {
        if (sounds.ContainsKey(def.Name))
            return sounds[def.Name];
        
        Sound sound = Raylib.LoadSound(def.Path);
        sounds[def.Name] = sound;
        return sound;
    }
    
    public static Music GetMusic(MusicDefinition def)
    {
        if (musicTracks.ContainsKey(def.Name))
            return musicTracks[def.Name];
        
        Music music = Raylib.LoadMusicStream(def.Path);
        musicTracks[def.Name] = music;
        return music;
    }
    
    public static void PlaySound(SoundDefinition def, float volumeMultiplier = 1.0f)
    {
        Sound sound = GetSound(def);
        float finalVolume = def.DefaultVolume * volumeMultiplier * soundVolume;
        Raylib.SetSoundVolume(sound, finalVolume);
        Raylib.PlaySound(sound);
    }
    
    public static void PlayMusic(MusicDefinition def)
    {
        Music music = GetMusic(def);
        
        if (currentMusic != null)
            Raylib.StopMusicStream(currentMusic.Value);
        
        currentMusic = music;
        music.Looping = def.Loop;
        Raylib.SetMusicVolume(music, def.DefaultVolume * musicVolume);
        Raylib.PlayMusicStream(music);
    }
    
    public static void StopMusic()
    {
        if (currentMusic != null)
        {
            Raylib.StopMusicStream(currentMusic.Value);
            currentMusic = null;
        }
    }
    
    public static void Update()
    {
        if (currentMusic != null)
            Raylib.UpdateMusicStream(currentMusic.Value);
    }
    
    public static void SetMasterVolume(float volume)
    {
        masterVolume = Math.Clamp(volume, 0f, 1f);
        Raylib.SetMasterVolume(masterVolume);
    }
    
    public static void SetSoundVolume(float volume)
    {
        soundVolume = Math.Clamp(volume, 0f, 1f);
    }
    
    public static void SetMusicVolume(float volume)
    {
        musicVolume = Math.Clamp(volume, 0f, 1f);
        if (currentMusic != null)
            Raylib.SetMusicVolume(currentMusic.Value, musicVolume);
    }
    
    public static void UnloadAll()
    {
        foreach (var sound in sounds.Values)
            Raylib.UnloadSound(sound);
        
        foreach (var music in musicTracks.Values)
            Raylib.UnloadMusicStream(music);
        
        sounds.Clear();
        musicTracks.Clear();
        
        Raylib.CloseAudioDevice();
    }
}