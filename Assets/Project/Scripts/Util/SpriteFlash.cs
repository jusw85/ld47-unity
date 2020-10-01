using Prime31.ZestKit;
using UnityEngine;

public class SpriteFlash
{
    public static readonly int MATERIAL_FLASHAMOUNT_ID = Shader.PropertyToID("_FlashAmount");
    public static readonly int MATERIAL_FLASHCOLOR_ID = Shader.PropertyToID("_FlashColor");

    public static Color GetMaterialFlashColor(SpriteRenderer spriteRenderer)
    {
        return spriteRenderer.material.GetColor(MATERIAL_FLASHCOLOR_ID);
    }

    public static void SetMaterialFlashColor(SpriteRenderer spriteRenderer, Color c)
    {
        spriteRenderer.material.SetColor(MATERIAL_FLASHCOLOR_ID, c);
    }

    public static FloatTween CreateTween(SpriteRenderer spriteRenderer, float from, float to, float duration,
        EaseType easeType = EaseType.QuartIn)
    {
        FloatTween tween = new FloatTween();
        SpriteFlashTweenTarget tweenTarget = new SpriteFlashTweenTarget(spriteRenderer);

        tween.setRecycleTween(false);
        tween.initialize(tweenTarget, to, duration);
        tween.setFrom(from).setEaseType(easeType);
        return tween;
    }

    private class SpriteFlashTweenTarget : AbstractTweenTarget<SpriteRenderer, float>
    {
        public override void setTweenedValue(float value)
        {
            _target.material.SetFloat(MATERIAL_FLASHAMOUNT_ID, value);
        }

        public override float getTweenedValue()
        {
            return _target.material.GetFloat(MATERIAL_FLASHAMOUNT_ID);
        }

        public SpriteFlashTweenTarget(SpriteRenderer spriteRenderer)
        {
            _target = spriteRenderer;
        }
    }


    private class DemoSpriteFlash : MonoBehaviour
    {
        private FloatTween tween;

        private void Start()
        {
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            tween = CreateTween(sr, 1.0f, 0.0f, 1.0f, EaseType.Linear);
            tween.start();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                if (tween.isRunning())
                {
                    tween.stop(true, true);
                }

                tween.jumpToElapsedTime(0f);
                tween.start();
            }
        }
    }
}