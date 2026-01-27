using System;
using System.Numerics;
using Raylib_cs;

namespace RatGame;

public abstract class Weapon()
{
    protected float damage;
    protected int cooldownFrames;
    protected int cooldownCounter;
    protected bool canShoot => cooldownCounter <= 0;
    protected Sprite? sprite;
    protected int hFlip = 1;

    public Vector2 Position;
    public Vector2 Offset;
    public bool IsAutomatic;

    public void SetPosition(Vector2 position)
    {
        Vector2 flippedOffset = new Vector2(Offset.X * hFlip, Offset.Y);
        Position = position + flippedOffset;
    }

    public virtual void Update()
    {
        if (cooldownCounter > 0)
            cooldownCounter--;
    }

    public virtual void Fire(Vector2 position, int hFlip)
    {
        if (!canShoot) return;
        cooldownCounter = cooldownFrames;
        this.hFlip = hFlip;
    }

    public void SetFlip(int hFlip)
    {
        this.hFlip = hFlip;
    }

    public void Draw()
    {
        if (sprite != null)
        {
            Rectangle source = new Rectangle(0, 0, sprite.Texture.Width * hFlip, sprite.Texture.Height);
            Rectangle dest = new Rectangle(MathF.Round(Position.X), MathF.Round(Position.Y), Math.Abs(sprite.Texture.Width), Math.Abs(sprite.Texture.Height));
            Raylib.DrawTexturePro(sprite.Texture, source, dest, Vector2.Zero, 0f, Color.White);
        }
    }
}

public class Machinegun : Weapon
{
    public  Machinegun()
    {
        damage = 1f;
        cooldownFrames = 7;
        sprite = SpriteManager.GetSprite(SpriteNames.Machinegun);
        Offset = new Vector2(0, 0);
    }

    public override void Fire(Vector2 position, int hFlip)
    {
        base.Fire(position, hFlip);

        float angleDegrees = hFlip == 1 ? 0 : 180;
        Bullet bullet = new(position, 5f, angleDegrees, damage);
        EntityManager.Register(bullet);

        Console.WriteLine($"Bullet spawned at: {position}, angle: {angleDegrees}, hFlip: {hFlip}");
    }

}
