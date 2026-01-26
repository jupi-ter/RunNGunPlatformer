using Raylib_cs;

namespace RatGame;

public class AnimationController
{
    private Animation? currentAnimation;
    private int imageIndex = 0;
    private float frameCounter = 0f;
    private bool isPlaying = false;

    public Sprite? CurrentSprite => currentAnimation?.Frames[imageIndex];
    public bool IsFinished => currentAnimation != null && !currentAnimation.Loop && imageIndex >= currentAnimation.Frames.Length - 1;

    public void Play(Animation? animation, bool restart = false)
    {
        if (animation == null) return;

        if (currentAnimation != animation || restart)
        {
            currentAnimation = animation;
            frameCounter = 0f;
            imageIndex = 0;
        }
        isPlaying = true;
    }

    public void Update()
    {
        if (currentAnimation == null || !isPlaying) return;

        frameCounter += currentAnimation.ImageSpeed;

        while (frameCounter >= 1f)
        {
            frameCounter -= 1f;
            imageIndex++;

            if (imageIndex >= currentAnimation.Frames.Length)
            {
                if (currentAnimation.Loop)
                {
                    imageIndex = 0;
                }
                else
                {
                    imageIndex = currentAnimation.Frames.Length - 1;
                    isPlaying = false;
                }
            }
        }
    }

    public void Stop()
    {
        isPlaying = false;
        frameCounter = 0f;
        imageIndex = 0;
    }

    public void Pause()
    {
        isPlaying = false;
    }

    public void Resume()
    {
        isPlaying = true;
    }
}

public class Animation(string name, Sprite[] frames, float speed, bool loop)
{
    public string Name = name;
    public Sprite[] Frames = frames;
    public float ImageSpeed = speed;
    public bool Loop = loop;
}