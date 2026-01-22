using System.Numerics;

namespace RatGame;

public static class CollisionManager {

    private const int CELL_SIZE = 32;
    private static List<CollisionShape> collisionShapes = [];
    private static SpatialGrid? spatialGrid = null;

    public static void Initialize(int cellSize = CELL_SIZE)
    {
        spatialGrid = new(cellSize);
    }

    public static void Register(CollisionShape shape)
    {
        collisionShapes.Add(shape);
    }

    public static void Unregister(CollisionShape shape)
    {
        collisionShapes.Remove(shape);
    }

    public static void Update()
    {
        if (spatialGrid != null)
        {
            spatialGrid.Clear();
            foreach (var shape in collisionShapes)
                spatialGrid.Insert(shape);
            
            foreach (var cell in spatialGrid.GetAllCells())
            {
                for (int i = 0; i < cell.Count; i++)
                {
                    for (int j = i + 1; j < cell.Count; j++)
                    {
                        if (CheckCollision(cell[i], cell[j]))
                        {
                            cell[i].Owner?.OnCollision(cell[j].Owner);
                            cell[j].Owner?.OnCollision(cell[i].Owner);
                        }
                    }
                }
            }
        }
    }

    public static bool CheckCollision(CollisionShape shape1, CollisionShape shape2)
    {
        return shape1.CollidesWith(shape2);
    }

    public static T? PlaceMeeting<T>(Entity entity, Vector2 positionToCheck) where T : Entity
    {
        if (entity.Collision == null) return null;

        Vector2 originalPosition = entity.Position;

        entity.Position = positionToCheck;
        entity.Collision.SetPosition(positionToCheck);

        foreach (var shape in collisionShapes)
        {
            if (shape.Owner == entity || shape.Owner is not T) continue;
            
            if (entity.Collision.CollidesWith(shape))
            {
                entity.Position = originalPosition;
                entity.Collision.SetPosition(originalPosition);
                return (T)shape.Owner;
            }
        }

        entity.Position = originalPosition;
        entity.Collision.SetPosition(originalPosition);
        return null;
    }
}
