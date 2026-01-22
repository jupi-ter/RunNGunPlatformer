using System.Numerics;
namespace RatGame;

public class Player : Entity
{
    private Animation? walkAnim = null;
    private Animation? idleAnim = null;
    private Animation? jumpAnim = null;
    private AnimationController animController = new();

    private float moveSpeed = 2f;
    private float gravity = 1f;
    private float jumpHeight = 30f;
    private bool isGrounded= false;

    public Player(Vector2 position) : base(position)
    {
    }

    public override void OnInit()
    {
        walkAnim = new Animation("Walk", SpriteManager.GetSprites(SpriteNames.PlayerWalk), 0.2f, true);
        idleAnim = new Animation("Idle", SpriteManager.GetSprites(SpriteNames.PlayerIdle), 0.2f, true);
        jumpAnim = new Animation("Jump", SpriteManager.GetSprites(SpriteNames.PlayerJump), 0.2f, true);

        animController.Play(idleAnim);
        
        CollisionOBB obb = new(new Vector2(10f, 10f), this);
        Collision = obb;
        Collision.SetPosition(Position);

        base.OnInit();
    }

    public override void Update()
    {
        UpdateCurrentAnimation();
        Movement();
    }

    private void Movement()
    {
        int hInput = Utils.ToInt(InputManager.IsActionDown(InputActions.Right)) 
                - Utils.ToInt(InputManager.IsActionDown(InputActions.Left));
        
        float hsp = hInput * moveSpeed;
        float vsp = gravity;
        
        if (isGrounded)
        {
            if (hInput == 0)
                animController.Play(idleAnim);
            else
                animController.Play(walkAnim);
        }
        else
        {
            animController.Play(jumpAnim);
        }

        if (InputManager.IsActionPressed(InputActions.Up) && isGrounded)
        {
            vsp -= jumpHeight;
        }

        // horizontal collision check
        float onePixelX = MathF.Sign(hsp);
        if (CollisionManager.PlaceMeeting<Wall>(this, Position + new Vector2(hsp, 0)) != null)
        {
            // move as close as we can
            while (CollisionManager.PlaceMeeting<Wall>(this, Position + new Vector2(onePixelX, 0)) == null)
            {
                Position.X += onePixelX;
            }
            hsp = 0;
        }
        Position.X += hsp;
        
        // vertical collision check
        float onePixelY = MathF.Sign(vsp);
        if (CollisionManager.PlaceMeeting<Wall>(this, Position + new Vector2(0, vsp)) != null)
        {
            while (CollisionManager.PlaceMeeting<Wall>(this, Position + new Vector2(0, onePixelY)) == null)
            {
                Position.Y += onePixelY;
            }
            vsp = 0;
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
        Position.Y += vsp;
        
        // update collision after all movement
        Collision?.SetPosition(Position);
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
    }
}