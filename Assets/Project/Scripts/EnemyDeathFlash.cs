using Prime31.ZestKit;
using UnityEngine;

public class EnemyDeathFlash : MonoBehaviour
{
    private SpriteRenderer rend;

    private FloatTween tween;
    private Crystal.FlashTweenTarget tweenTarget;

    private void Awake()
    {
        rend = GetComponent<SpriteRenderer>();

        tween = new FloatTween();
        tweenTarget = new Crystal.FlashTweenTarget(rend);
        tween.setRecycleTween(false);
    }
    
    private void Start()
    {
        tween.initialize(tweenTarget, 0f, 0.2f); 
        tween.setFrom(1.0f).start();
    }
}