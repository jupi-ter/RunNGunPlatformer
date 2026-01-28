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
    protected int horizontalRecoil = 0;

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

    public void Fire(Entity entity, int hFlip)
    {
        if (!canShoot) return;
        cooldownCounter = cooldownFrames;
        this.hFlip = hFlip;
        //recoil
        entity.Position -= new Vector2(horizontalRecoil * hFlip, 0);
        OnFire(entity);
    }

    public abstract void OnFire(Entity entity);

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
    public Machinegun()
    {
        damage = 1f;
        cooldownFrames = 7;
        sprite = SpriteManager.GetSprite(SpriteNames.Machinegun);
        IsAutomatic = true;
        horizontalRecoil = 2;
        Offset = new Vector2(0, 0);
    }

    public override void OnFire(Entity entity)
    {
        float angleDegrees = hFlip == 1 ? 0 : 180;
        var origin = entity.Position + entity.GetCenter();
        Bullet bullet = new(origin, 5f, angleDegrees, damage);
        EntityManager.Register(bullet);
        //Console.WriteLine($"Bullet spawned at: {position}, angle: {angleDegrees}, hFlip: {hFlip}");
    }

}
