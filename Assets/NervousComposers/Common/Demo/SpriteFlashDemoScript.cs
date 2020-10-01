using Prime31.ZestKit;
using UnityEngine;

/// <summary>
/// Set SpriteRenderer material to SpriteFlash
/// </summary>
public class SpriteFlashDemoScript : MonoBehaviour
{
    [SerializeField] private Color flashColour = Color.white;
    [SerializeField] private float duration = 1.0f;
    [SerializeField] private EaseType easeType = EaseType.Linear;

    private SpriteRenderer spriteRenderer;
    private FloatTween tween;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        tween = SpriteFlash.CreateOneWayTween(spriteRenderer, 1.0f, 0.0f, duration, easeType);
    }

    public void Flash()
    {
        SpriteFlash.SetMaterialFlashColor(spriteRenderer, flashColour);

        if (tween.isRunning())
        {
            tween.stop(true, true);
        }

        tween.jumpToElapsedTime(0f);
        tween.setDuration(duration);
        tween.setEaseType(easeType);
        tween.start();
    }
}