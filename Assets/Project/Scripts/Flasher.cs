using Prime31.ZestKit;
using UnityEngine;

public class Flasher : ITweenTarget<float>
{
    public static readonly int MATERIAL_FLASHAMOUNT_ID = Shader.PropertyToID("_FlashAmount");
    public static readonly int MATERIAL_FLASHCOLOR_ID = Shader.PropertyToID("_FlashColor");

    private SpriteRenderer spriteRenderer;
    // private Tween<> flashTween;

    public Color FlashColor { get; set; }

    public float TexFlashAmount
    {
        get => spriteRenderer.material.GetFloat(MATERIAL_FLASHAMOUNT_ID);
        set => spriteRenderer.material.SetFloat(MATERIAL_FLASHAMOUNT_ID, value);
    }

    public Color TexFlashColor
    {
        get => spriteRenderer.material.GetColor(MATERIAL_FLASHCOLOR_ID);
        set => spriteRenderer.material.SetColor(MATERIAL_FLASHCOLOR_ID, value);
    }

    public Flasher(SpriteRenderer spriteRenderer, float value, float duration, int numLoops)
    {
        this.spriteRenderer = spriteRenderer;

        TexFlashAmount = 0f;
        FlashColor = Color.white;
        
        // flashTween = DOTween
        //     .To(() => TexFlashAmount, x => TexFlashAmount = x, value, duration)
        //     .SetLoops(numLoops, LoopType.Yoyo)
        //     .SetAutoKill(false);
    }

    // public void Start()
    // {
    //     if (!flashTween.IsPlaying())
    //     {
    //         Restart();
    //     }
    // }
    //
    // public void Restart()
    // {
    //     TexFlashColor = FlashColor;
    //     TexFlashAmount = 0f;
    //     flashTween.Restart();
    // }
    //
    // public void Pause()
    // {
    //     if (flashTween.IsPlaying())
    //     {
    //         flashTween.Pause();
    //     }
    // }
    //
    // public void Stop()
    // {
    //     Pause();
    //     TexFlashAmount = 0f;
    // }
    
    public void setTweenedValue(float value)
    {
        TexFlashAmount = value;
    }

    public float getTweenedValue()
    {
        return TexFlashAmount;
    }

    public object getTargetObject()
    {
        return this;
    }
}