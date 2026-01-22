using Raylib_cs;
using System.Numerics;

namespace RatGame;

public abstract class Entity {

    // transform
    public Vector2 Position = Vector2.Zero;
    public float RotationDegrees = 0f; 
    public float ImageXScale = 1f;
    public float ImageYScale = 1f;

    // visual
    public Sprite? CurrentSprite;
    public Vector2 Origin = Vector2.Zero;
    public Color Tint = Color.White;
    public bool Visible = false;

    // update logic
    public bool IsActive = false;
    public bool IsDestroyed = false;

    //collision
    public CollisionShape? Collision = null;

    public Entity(Vector2 position)
    {
        Position = position;
        EntityManager.Register(this);
        Init();
    }

    public void Init()
    {
        OnInit();
    }

    public virtual void OnInit()
    {
        Visible = true;
        IsActive = true;
    }

    public virtual void Update() { }

    public virtual void Draw()
    {
        if (Visible && IsActive && !IsDestroyed && CurrentSprite != null) {
            // scale here is a simple float, should be a vector2, todo: fix
            // scale is a placeholder value
            Raylib.DrawTextureEx(CurrentSprite.Texture, Position, RotationDegrees, 1f, Tint);
        }
    }

    public virtual void OnCollision(Entity? other)
    {
        if (other == null) return;
    }
    
    public void SetCollision(CollisionShape collision)
    {
        Collision = collision;
        collision.Owner = this;    
    }
}