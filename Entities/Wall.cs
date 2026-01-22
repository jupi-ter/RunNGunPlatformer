using System.Numerics;

namespace RatGame;

public class Wall : Entity
{
    public Wall(Vector2 position) : base(position)
    {
    }

    public override void OnInit()
    {
        CurrentSprite = SpriteManager.GetSprite(SpriteNames.Wall);
        CollisionOBB obb = new(new Vector2(10f, 10f), this);
        Collision = obb;
        Collision.SetPosition(Position);
        base.OnInit();
    }
}
