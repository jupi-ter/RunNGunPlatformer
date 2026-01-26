using System.Net;
using RatGame;

public static class EntityManager
{
    private static List<Entity> registeredEntities = [];

    public static T? GetFirstInstanceOf<T>() where T : Entity
    {
        foreach (var entity in registeredEntities)
        {
            if (entity is T t)
                return t;
        }

        return null;
    }

    public static void Register(Entity entity)
    {
        registeredEntities.Add(entity);
    }

    public static void UpdateAll()
    {
        foreach (var entity in registeredEntities)
        {
            if (entity.IsActive && !entity.IsDestroyed)
                entity.Update();
        }
    }

    public static void DrawAll()
    {
        foreach (var entity in registeredEntities)
        {
            if (entity.Visible && !entity.IsDestroyed)
                entity.Draw();
        }
    }
    
    public static void Cleanup()
    {
        registeredEntities.RemoveAll(e => e.IsDestroyed);   
    }

    public static void ClearAll()
    {
        registeredEntities.Clear();
    }
}