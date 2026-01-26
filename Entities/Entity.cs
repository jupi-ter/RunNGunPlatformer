using Raylib_cs;
using System.Numerics;

namespace RatGame;

public abstract class Entity {

    // transform
    public Vector2 Position = Vector2.Zero;
    public float RotationDegrees = 0f; 
    public float ImageXScale = 1f;
    public float ImageYScale = 1f;
    protected int HFlip = 1; //(-1, 1)

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
            Rectangle source = new Rectangle(0, 0, CurrentSprite.Texture.Width * HFlip, CurrentSprite.Texture.Height);
            
            Rectangle dest = new Rectangle(
                Position.X,
                Position.Y,
                CurrentSprite.Texture.Width * ImageXScale,
                CurrentSprite.Texture.Height * ImageYScale
            );
            
            Raylib.DrawTexturePro(
                CurrentSprite.Texture,
                source,
                dest,
                Origin,
                RotationDegrees,
                Tint
            );
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