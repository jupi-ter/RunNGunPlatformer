using System.Numerics;

namespace RatGame;

public class CollisionCircle(float radius, Entity owner) : CollisionShape(owner)
{
    private float radius = radius;
    public float Radius { get => radius; private set => radius = value; }

    public override bool CollidesWith(CollisionShape other)
    {
        return other.CollidesWithCircle(this);
    }

    public override bool CollidesWithCircle(CollisionCircle other)
    {
        float distance = Vector2.Distance(Origin, other.Origin);
        return distance < (Radius + other.Radius);
    }

    public override bool CollidesWithOBB(CollisionOBB other)
    {
        var obb = other;
        float rotationRadians = Utils.DegreesToRadians(obb.RotationDegrees);
        Vector2 obbOrigin = obb.Origin;
        Vector2 localPoint = Origin - obbOrigin;
        
        float cos = MathF.Cos(-rotationRadians);
        float sin = MathF.Sin(-rotationRadians);
        float rotX = localPoint.X * cos - localPoint.Y * sin;
        float rotY = localPoint.X * sin + localPoint.Y * cos;
        
        float halfW = obb.Width / 2f;
        float halfH = obb.Height / 2f;
        float clampedX = Math.Clamp(rotX, -halfW, halfW);
        float clampedY = Math.Clamp(rotY, -halfH, halfH);
        
        float worldX = clampedX * MathF.Cos(rotationRadians) - clampedY * MathF.Sin(rotationRadians);
        float worldY = clampedX * MathF.Sin(rotationRadians) + clampedY * MathF.Cos(rotationRadians);
        Vector2 closestPoint = obbOrigin + new Vector2(worldX, worldY);
        
        float distance = Vector2.Distance(Origin, closestPoint);
        return distance < Radius;
    }
}
