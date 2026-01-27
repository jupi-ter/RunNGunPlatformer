using System.Numerics;
namespace RatGame;

public class Bullet : Entity
{
    public float Speed;
    public float Damage;
    private AnimationController animController = new();

    public Bullet(Vector2 position, float speed, float rotation, float damage) : base(position)
    {
        Speed = speed;
        RotationDegrees = rotation;
        Damage = damage;
    }

    public override void OnInit()
    {
        Animation anim = new("Walk", SpriteManager.GetSprites(SpriteNames.Bullet), 1f, false);
        animController.Play(anim);
        DrawFromCenter = true;
        base.OnInit();
    }

    public override void Update()
    {
        //if (outsideScreen) destroy;
        UpdateCurrentAnimation();

        float rotationRads = Utils.DegreesToRadians(RotationDegrees);

        Vector2 newPosition = new(
            MathF.Cos(rotationRads) * Speed,
            MathF.Sin(rotationRads) * Speed
        );
        Position += newPosition;
    }

    private void UpdateCurrentAnimation()
    {
        animController.Update();
        
        // Set current sprite from animation
        if (animController.CurrentSprite != null)   
        {
            if (CurrentSprite != null)
                CurrentSprite.Texture = animController.CurrentSprite.Texture;
            else
                CurrentSprite = new Sprite(animController.CurrentSprite.Texture, "");
        }
    }

    public override void OnCollision(Entity? other)
    {
        base.OnCollision(other);
        
        if (other is Wall)
        {
            IsDestroyed = true;
        }
    }
}