using System.Net;
using RatGame;

public static class EntityManager
{
    private static List<Entity> registeredEntities = [];
    private static List<Entity> queuedEntities = [];

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
        queuedEntities.Add(entity);
    }

    public static void UpdateAll()
    {
        if (queuedEntities.Count > 0)
        {
            foreach (var entity in queuedEntities)
            {
                entity.Init();
            }
            
            registeredEntities.AddRange(queuedEntities);
            queuedEntities.Clear();
        }

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
        queuedEntities.Clear();
    }
}