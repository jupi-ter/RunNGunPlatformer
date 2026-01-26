using System.Numerics;
namespace RatGame;

public class Bullet : Entity
{
    public float Speed;
    private AnimationController animController;

    public Bullet(Vector2 position, float speed, float rotation) : base(position)
    {
        Speed = speed;
        RotationDegrees = rotation;
        animController = new(this);
    }

    public override void OnInit()
    {
        Animation anim = new("Walk", SpriteManager.GetSprites(SpriteNames.Bullet), 1f, false);
        animController.Play(anim);
        base.OnInit();
    }

    public override void Update()
    {
        //if (outsideScreen) destroy;
        animController.Update();

        float rotationRads = Utils.DegreesToRadians(RotationDegrees);

        Vector2 newPosition = new(
            MathF.Cos(rotationRads) * Speed,
            MathF.Sin(rotationRads) * Speed
        );
        Position += newPosition;
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