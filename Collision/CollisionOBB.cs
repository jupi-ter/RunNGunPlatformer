using System.Numerics;
namespace RatGame;

public class CollisionOBB(Vector2 dimensions, Entity owner) : CollisionShape(owner)
{
    private Vector2 dimensions = dimensions;

    public float Width { get => dimensions.X; }
    public float Height { get => dimensions.Y; }

    public override bool CollidesWith(CollisionShape other)
    {
        return other.CollidesWithOBB(this);
    }

    public override bool CollidesWithCircle(CollisionCircle other)
    {
        return other.CollidesWithOBB(this);
    }

    public override bool CollidesWithOBB(CollisionOBB other)
    {
        return CheckCollision(this, other);
    }

    public Vector2[] GetCorners()
    {
        float halfW = dimensions.X * 0.5f;
        float halfH = dimensions.Y * 0.5f;

        Vector2[] local =
        [
            new(-halfW, -halfH),
            new(halfW, -halfH),
            new(halfW, halfH),
            new(-halfW, halfH)
        ];

        float rotationRadians = Utils.DegreesToRadians(rotationDegrees);

        float cos = MathF.Cos(rotationRadians);
        float sin = MathF.Sin(rotationRadians);

        Vector2[] world = new Vector2[4];

        for (int i = 0; i < 4; i++)
        {
            Vector2 localCorner = local[i];
            float rotX = (localCorner.X * cos) - (localCorner.Y * sin);
            float rotY = (localCorner.X * sin) + (localCorner.Y * cos);
            world[i] = Origin + new Vector2(rotX, rotY);
        }

        return world;
    }

    public Vector2[] GetAxes()
    {
        float rotationRadians = Utils.DegreesToRadians(rotationDegrees);

        float cos = MathF.Cos(rotationRadians);
        float sin = MathF.Sin(rotationRadians);

        return
        [
            new(cos, sin),
            new(-sin, cos)
        ];
    }

    public bool CheckCollision(CollisionOBB obb1, CollisionOBB obb2)
    {
        Vector2[] corners1 = obb1.GetCorners();
        Vector2[] axes1 = obb1.GetAxes();

        Vector2[] corners2 = obb2.GetCorners();
        Vector2[] axes2 = obb2.GetAxes();

        foreach (var axis in axes1)
        {
            if (IsSeparatingAxes(axis, corners1, corners2))
                return false;
        }

        foreach (var axis in axes2)
        {
            if (IsSeparatingAxes(axis, corners1, corners2))
                return false;
        }

        return true;
    }

    private bool IsSeparatingAxes(Vector2 axis, Vector2[] corners1, Vector2[] corners2)
    {
        (float min1, float max1) = ProjectOntoAxis(axis, corners1);
        (float min2, float max2) = ProjectOntoAxis(axis, corners2);

        return max1 <= min2 || max2 <= min1;
    }

    private (float min, float max) ProjectOntoAxis(Vector2 axis, Vector2[] corners)
    {
        float min = Vector2.Dot(corners[0], axis);
        float max = min;

        for (int i = 0; i < corners.Length; i++)
        {
            float projection = Vector2.Dot(corners[i], axis);
            
            if (projection < min)
                min = projection;

            if (projection > max)
                max = projection;
        }

        return (min, max);
    }
}

