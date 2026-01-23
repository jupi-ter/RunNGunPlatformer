using System.Numerics;
using System.Runtime.InteropServices;
namespace RatGame;

public class Player : Entity
{
    private Animation? walkAnim = null;
    private Animation? idleAnim = null;
    private Animation? jumpAnim = null;
    private AnimationController animController = new();

    private float groundSpeed = 2f;
    private float airSpeed = 2.5f;
    private float moveSpeed = 0f;

    private float gravity = 0.3f;
    private float jumpHeight = 4f;
    private bool isGrounded= false;
    private int coyoteCounter = 0;
    private int coyoteMax = 6;
    private int bufferCounter = 0;
    private int maxBuffer = 4;
    private float hsp = 0;
    private float vsp = 0;
    private int right = 1;

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
        MovementV2();
    }

    private void MovementV2()
    {
        GatherInput(out int move, out bool jumpKey, out bool jumpKeyReleased);
        UpdateGroundedState();
        UpdateHorizontalMovement(move);
        UpdateAnimations(move);
        UpdateVerticalMovement(jumpKey, jumpKeyReleased);
        CheckCollisionsAndMove();
    }

    private static void GatherInput(out int move, out bool jumpKey, out bool jumpKeyReleased)
    {
        int rightKey = Utils.ToInt(InputManager.IsActionDown(InputActions.Right));
        int leftKey = Utils.ToInt(InputManager.IsActionDown(InputActions.Left));
        jumpKey = InputManager.IsActionDown(InputActions.Up);
        jumpKeyReleased = InputManager.IsActionReleased(InputActions.Up);
        move = rightKey - leftKey;
    }

    private void UpdateGroundedState()
    {
        if (CollisionManager.PlaceMeeting<Wall>(this, Position + new Vector2 (0f, 1f)))
        {
            isGrounded = true;
            moveSpeed = groundSpeed;
        }
        else
        {
            isGrounded = false;
            moveSpeed = airSpeed;
        }
    }

    private void UpdateHorizontalMovement(int move)
    {
        hsp = move * moveSpeed;

        if (move != 0)
        {
            if (!InputManager.IsActionDown(InputActions.Right)) right = move;

            if (isGrounded)
            {
                //dust counter and dust spawning code goes here
            }
        }
        else
        {
            //clean dust counter    
        }
    }

    private void UpdateVerticalMovement(bool jumpKey, bool jumpKeyReleased)
    {
        if (!isGrounded)
        {
            vsp += gravity;
        }

        if (!isGrounded)
        {
            if (coyoteCounter > 0)
            {
                coyoteCounter--;
                if (jumpKey)
                    Jump();
            }
        }
        else
        {
            coyoteCounter = coyoteMax;
        }

        if (jumpKey && !CollisionManager.PlaceMeeting<Wall>(this, Position - new Vector2 (0f, jumpHeight)))
        {
            bufferCounter = maxBuffer;
        }

        if (bufferCounter > 0)
        {
            bufferCounter--;
            if (isGrounded)
                Jump();
        }

        if (jumpKeyReleased && vsp > 0)
        {
            vsp *= 0.5f;
        }
    }

    private void CheckCollisionsAndMove()
    {
        float onePixelX = MathF.Sign(hsp);
        if (CollisionManager.PlaceMeeting<Wall>(this, Position + new Vector2(hsp, 0)))
        {
            // move as close as we can
            while (!CollisionManager.PlaceMeeting<Wall>(this, Position + new Vector2(onePixelX, 0)))
            {
                Position.X += onePixelX;
            }
            hsp = 0;
        }
        Position.X += hsp;
        
        float onePixelY = MathF.Sign(vsp);
        if (CollisionManager.PlaceMeeting<Wall>(this, Position + new Vector2(0, vsp)))
        {
            while (!CollisionManager.PlaceMeeting<Wall>(this, Position + new Vector2(0, onePixelY)))
            {
                Position.Y += onePixelY;
            }
            vsp = 0;
        }
        Position.Y += vsp;
        
        // update collision after all movement
        Collision?.SetPosition(Position);
    }

    private void UpdateAnimations(int move)
    {
        if (isGrounded)
        {
            if (move == 0)
                animController.Play(idleAnim);
            else
                animController.Play(walkAnim);
        }
        else
        {
            animController.Play(jumpAnim);
        }
    }

    private void Jump()
    {
        // todo: squash and stretch
        vsp = -jumpHeight;
        bufferCounter = 0;
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