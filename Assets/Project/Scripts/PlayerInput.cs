using Jusw85.Common;
using k;
using Prime31;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(DynamicPlatformController))]
public class PlayerInput : MonoBehaviour
{
    [SerializeField] private AudioEvent jumpAudio;
    [SerializeField] private AudioEvent slashAudio;
    [SerializeField] private float horizontalDeadzone = 0.2f;
    [SerializeField] private float verticalDeadzone = 0.2f;

    [SerializeField, HideInInspector] private Animator animator;
    [SerializeField, HideInInspector] private DynamicPlatformController platformController;
    private SoundKit soundKit;

    private bool attackCanAttackCancel;
    private bool attackCanJumpCancel;
    private bool isAttacking;
    private bool isJumping;
    private bool isFacingRight = true;

    private float attackActuatedTime = -1f;
    [SerializeField] private float earlyAttackTimeTolerance = 0.1f;

    private void OnValidate()
    {
        animator = GetComponent<Animator>();
        platformController = GetComponent<DynamicPlatformController>();
    }

    private void Start()
    {
        soundKit = Toolbox.Instance.TryGet<SoundKit>();
        platformController.IsJumpingThisFrameCallback += () =>
        {
            isJumping = true;
            jumpAudio?.Play(soundKit);
        };
        platformController.IsLandingThisFrameCallback += () =>
        {
            isJumping = false;
            if (Time.time - attackActuatedTime <= earlyAttackTimeTolerance)
            {
                attackCanAttackCancel = false;
                animator.SetTrigger(AnimatorParams.ATTACK);
            }
        };
    }

    private void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (!Mathf.Approximately(moveInput.x, 0))
        {
            SetIsFacingRight(moveInput.x > 0);
        }

        if (!isAttacking && Mathf.Abs(moveInput.x) > horizontalDeadzone)
        {
            float walkDir = Mathf.Sign(moveInput.x) > 0 ? 1 : -1;
            platformController.Walk(walkDir);
            // SetIsFacingRight(walkDir > 0);
        }
        else
        {
            platformController.Walk(0);
        }

        bool upKeyPressed = moveInput.y > verticalDeadzone;
        bool jumpKeyPressed = Input.GetButtonDown("Jump");
        // bool jumpKeyPressed = Input.GetKeyDown("k");
        // bool jumpPressed = jumpKeyPressed || upKeyPressed;
        bool jumpPressed = jumpKeyPressed;
        if (jumpPressed)
        {
            platformController.Jump();
            // if (!isAttacking)
            // {
            //     platformController.Jump();
            // }
            // else if (attackCanAttackCancel)
            // {
            //     platformController.Jump();
            // }
        }

        bool firePress = Input.GetButtonDown("Fire1");
        if (firePress)
        {
            attackActuatedTime = Time.time;
            if (!isJumping &&
                (!isAttacking || (isAttacking && attackCanAttackCancel)))
            {
                // isAttacking = true;
                attackCanAttackCancel = false;
                animator.SetTrigger(AnimatorParams.ATTACK);
            }
        }

        animator.SetBool(AnimatorParams.RUNNING, Mathf.Abs(platformController.Velocity.x) > 0);
        animator.SetBool(AnimatorParams.IS_GROUNDED, platformController.IsGrounded);
        animator.SetFloat(AnimatorParams.V_SPEED, platformController.Velocity.y);
    }

    private void SetIsFacingRight(bool isFacingRight)
    {
        if (this.isFacingRight ^ isFacingRight)
        {
            var scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }

        this.isFacingRight = isFacingRight;
    }

    public void EnableAttackCancel()
    {
        attackCanAttackCancel = true;
    }

    // public void EnableJumpCancel()
    // {
    //     attackCanJumpCancel = true;
    // }

    public bool IsAttacking
    {
        get => isAttacking;
        set => isAttacking = value;
    }

    public void PlayAttackAudio()
    {
        slashAudio?.Play(soundKit);
    }

    public bool IsFacingRight => isFacingRight;

}