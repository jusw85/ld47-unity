// using Jusw85.Common;
// using k;
// using Prime31;
// using Prime31.ZestKit;
// using UnityEngine;
//
// public class EnemyMover : MonoBehaviour
// {
//     [SerializeField] private float walkSpeed = 2f;
//     [SerializeField] private int maxHp = 10;
//     [SerializeField] private float knockBackForce = 2f;
//     [SerializeField] private float knockBackRecoverTime = 0.5f;
//     [SerializeField] private GameObject snailDie;
//
//     private Rigidbody2D rb2d;
//     private SpriteRenderer rend;
//     private int hp;
//
//     private FloatTween tween;
//     private Crystal.FlashTweenTarget tweenTarget;
//
//     private void Awake()
//     {
//         rb2d = GetComponent<Rigidbody2D>();
//         rend = GetComponent<SpriteRenderer>();
//         hp = maxHp;
//
//         tween = new FloatTween();
//         tweenTarget = new Crystal.FlashTweenTarget(rend);
//         tween.setRecycleTween(false);
//     }
//
//     private void Start()
//     {
//         rb2d.velocity = new Vector2(walkSpeed, 0);
//         if (WalkSpeed > 0)
//         {
//             Vector3 v = transform.localScale;
//             v.x *= -1;
//             transform.localScale = v;
//         }
//     }
//
//     private float dampVelocity;
//
//     private void FixedUpdate()
//     {
//         Vector2 v = rb2d.velocity;
//         v.x = Mathf.SmoothDamp(v.x, walkSpeed, ref dampVelocity, knockBackRecoverTime);
//         rb2d.velocity = v;
//     }
//
//     // private bool isDying = false;
//     [SerializeField] private AudioEvent hitClip;
//     [SerializeField] private AudioEvent deathClip;
//
//     public void Hurt()
//     {
//         hitClip?.Play(Toolbox.Instance.Get<SoundKit>());
//         // if (isDying) return;
//         if (tween.isRunning()) tween.stop(true, true);
//         tween.initialize(tweenTarget, 0f, 0.2f);
//         tween.setFrom(1.0f).start();
//
//         if (--hp <= 0)
//         {
//             deathClip?.Play(Toolbox.Instance.Get<SoundKit>());
//             // isDying = true;
//             GameObject obj = Instantiate(snailDie, transform.position, Quaternion.identity);
//             obj.transform.localScale = transform.localScale;
//
//             if (tween.isRunning()) tween.stop(true, true);
//             Destroy(gameObject);
//         }
//         else
//         {
//             AddKnockback();
//         }
//     }
//
//     public void StopTween()
//     {
//         if (tween.isRunning()) tween.stop(true, true);
//     }
//
//     private void AddKnockback()
//     {
//         bool isFacingRight =
//             GameObject.FindWithTag(Tags.PLAYER)?.GetComponent<PlayerInput>()?.IsFacingRight ?? true;
//         Vector2 force = (isFacingRight ? Vector2.right : Vector2.left) * knockBackForce;
//         rb2d.AddForce(force, ForceMode2D.Impulse);
//     }
//
//     public float WalkSpeed
//     {
//         get => walkSpeed;
//         set => walkSpeed = value;
//     }
// }