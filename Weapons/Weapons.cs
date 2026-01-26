using System.Numerics;
using System.Security;
using Raylib_cs;

namespace RatGame;

public abstract class Weapon()
{
    protected float damage;
    protected int cooldownFrames;
    protected int cooldownCounter;
    protected bool canShoot => cooldownCounter <= 0;
    protected Sprite? sprite;
    protected float angleDegrees;

    public Vector2 Position;
    public Vector2 Offset;
    public bool IsAutomatic;

    public virtual void Update()
    {
        if (cooldownCounter > 0)
            cooldownCounter--;
    }
    public virtual void Fire(Vector2 position, float angleDegrees)
    {
        if (!canShoot) return;
        cooldownCounter = cooldownFrames;
    }

    public void Draw()
    {
        if (sprite != null)
            Raylib.DrawTextureEx(sprite.Texture, Position, angleDegrees, 1f, Color.White);
    }
}

public class Machinegun : Weapon
{
    public Machinegun()
    {
        damage = 1f;
        cooldownFrames = 7;
        sprite = SpriteManager.GetSprite(SpriteNames.Machinegun);
    }

    public override void Fire(Vector2 position, float angleDegrees)
    {
        base.Fire(position, angleDegrees);

        Bullet bullet = new(position, 5f, angleDegrees);
    }

}
