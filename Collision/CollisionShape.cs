using System.Numerics;

namespace RatGame;

public abstract class CollisionShape
{
    protected Vector2 position = Vector2.Zero;
    protected Vector2 offset = Vector2.Zero;
    protected float rotationDegrees;
    
    public float RotationDegrees { get => rotationDegrees; }

    public Vector2 Origin => position + offset;
    public Entity? Owner = null;

    public CollisionShape(Entity owner)
    {
        Owner = owner;
        CollisionManager.Register(this);
    }

    public virtual void SetPosition(Vector2 position) { this.position = position; }
    public virtual void SetOffset(Vector2 offset) { this.offset = offset; }
    public virtual void SetRotation(float rotationDegrees) { this.rotationDegrees = rotationDegrees; }
    public virtual void AddRotation(float rotationDegrees) { this.rotationDegrees += rotationDegrees; }
    
    public abstract bool CollidesWith(CollisionShape other);
    public abstract bool CollidesWithCircle(CollisionCircle other);
    public abstract bool CollidesWithOBB(CollisionOBB other);
}