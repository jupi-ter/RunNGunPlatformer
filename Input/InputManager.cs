using Raylib_cs;
namespace RatGame;

public static class InputManager
{
    private static Dictionary<string, List<KeyboardKey>> keyboardBindings = new();
    private static Dictionary<string, List<MouseButton>> mouseBindings = new();
    
    public static void BindKey(string action, params KeyboardKey[] keys)
    {
        if (!keyboardBindings.ContainsKey(action))
            keyboardBindings[action] = [];
        keyboardBindings[action].AddRange(keys);
    }
    
    public static void BindMouse(string action, params MouseButton[] buttons)
    {
        if (!mouseBindings.ContainsKey(action))
            mouseBindings[action] = [];
        mouseBindings[action].AddRange(buttons);
    }
    
    public static bool IsActionPressed(string action)
    {
        if (keyboardBindings.TryGetValue(action, out List<KeyboardKey>? kBindings))
        {
            foreach (var key in kBindings)
            {
                if (Raylib.IsKeyPressed(key))
                    return true;
            }
        }
        
        if (mouseBindings.TryGetValue(action, out List<MouseButton>? mBindings))
        {
            foreach (var button in mBindings)
            {
                if (Raylib.IsMouseButtonPressed(button))
                    return true;
            }
        }
        
        return false;
    }
    
    public static bool IsActionDown(string action)
    {
        if (keyboardBindings.TryGetValue(action, out List<KeyboardKey>? kBindings))
        {
            foreach (var key in kBindings)
            {
                if (Raylib.IsKeyDown(key))
                    return true;
            }
        }
        
        if (mouseBindings.TryGetValue(action, out List<MouseButton>? mBindings))
        {
            foreach (var button in mBindings)
            {
                if (Raylib.IsMouseButtonDown(button))
                    return true;
            }
        }
        
        return false;
    }
    
    public static bool IsActionReleased(string action)
    {
        if (keyboardBindings.TryGetValue(action, out List<KeyboardKey>? kBindings))
        {
            foreach (var key in kBindings)
            {
                if (Raylib.IsKeyReleased(key))
                    return true;
            }
        }
        
        if (mouseBindings.TryGetValue(action, out List<MouseButton>? mBindings))
        {
            foreach (var button in mBindings)
            {
                if (Raylib.IsMouseButtonReleased(button))
                    return true;
            }
        }
        
        return false;
    }
}