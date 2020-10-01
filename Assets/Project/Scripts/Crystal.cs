using System;
using Jusw85.Common;
using k;
using Prime31.ZestKit;
using UnityEngine;

public class Crystal : MonoBehaviour
{
    [SerializeField] private int health;

    private HUDManager hudManager;
    private SpriteRenderer rend;
    private Animator anim;

    private FloatTween tween;
    private FlashTweenTarget tweenTarget;
    private void Awake()
    {
        rend = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        tween = new FloatTween();
        tweenTarget = new FlashTweenTarget(rend);
        // tween.initialize(tweenTarget, 0f, 1f);
        // tween.setFrom(1f);
        tween.setRecycleTween(false);
    }

    private void Start()
    {
        MainGame game = GameObject.FindWithTag(Tags.MAIN_GAME)?.GetComponent<MainGame>();
        if (game.IsInitialised)
        {
            initHudManager(game);
        }
        else
        {
            game.InitialisedCallback += () => { initHudManager(game); };
        }
    }

    private void initHudManager(MainGame game)
    {
        hudManager = game.HudManager;
        hudManager?.SetHealth(health);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isEnding)
        {
            return;
        }

        if (other.tag.Equals(Tags.ENEMY))
        {
            // Debug.Log("Crystal: touched");
            hudManager?.SetHealth(--health);
            
            if (tween.isRunning()) tween.stop(true, true);
            tween.initialize(tweenTarget, 0f, 0.1f); 
            tween.setFrom(0.5f).start();
            
            if (health <= 0)
            {
                //StartCoroutine(CoroutineUtils.DelaySeconds(() =>
               // {
                    //rend.ZKalphaTo(0, 1).start();
                //}, 2));
                anim.SetTrigger(AnimatorParams.DIE);
                GameObject.FindWithTag(Tags.MAIN_GAME)?.GetComponent<MainGame>()?.Lose();
            }

            other.gameObject.GetComponent<EnemyMover>().StopTween();
            Destroy(other.gameObject);
        }
    }

    private bool isEnding;

    public bool IsEnding
    {
        get => isEnding;
        set => isEnding = value;
    }
    
    public class FlashTweenTarget : AbstractTweenTarget<SpriteRenderer, float>
    {
        public static readonly int MATERIAL_FLASHAMOUNT_ID = Shader.PropertyToID("_FlashAmount");
        
        public override void setTweenedValue(float value)
        {
            _target.material.SetFloat(MATERIAL_FLASHAMOUNT_ID, value);
        }

        public override float getTweenedValue()
        {
            return _target.material.GetFloat(MATERIAL_FLASHAMOUNT_ID);
        }

        public FlashTweenTarget(SpriteRenderer spriteRenderer)
        {
            _target = spriteRenderer;
        }
    }
}