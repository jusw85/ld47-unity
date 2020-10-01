using System;
using Jusw85.Common;
using UnityEngine;

[RequireComponent(typeof(Raycaster))]
public class DynamicPlatformController : MonoBehaviour
{
    #region Member Variables

    [SerializeField] private float walkVelocity = 8;
    [SerializeField] private float jumpVelocity = 20;
    [SerializeField] private Vector2 groundCheckProtrusion = new Vector2(0, -0.05f);
    [SerializeField] private float earlyJumpTimeTolerance = 0.1f;
    [SerializeField] private float lateJumpTimeTolerance = 0.1f;
    public event Action IsJumpingThisFrameCallback;
    public event Action IsLandingThisFrameCallback;

    #endregion

    #region Cached Variables

    private Rigidbody2D rb2d;
    private Raycaster raycaster;

    #endregion

    #region Internal State Variables

    private float walkDir;
    private Vector2 velocity;
    private bool isGrounded;
    private int jumpCount;
    private float jumpActuatedTime = -1f;
    private float touchedGroundTime = -1f;
    private float leftGroundTime = -1f;
    private bool isJumpingThisFrame;

    [SerializeField] private float gravityScale = 1.5f;

    #endregion

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        raycaster = GetComponent<Raycaster>();
    }

    private bool jumpReleased;

    private void Update()
    {
        // jumpReleased = Input.GetButtonUp("Jump");
        jumpReleased = !Input.GetButton("Jump");
        // jumpReleased = Input.GetKeyUp("k");
    }

    /// <summary>
    /// TODO: Consider when to reset frame variables
    /// Use case: Multiple FixedUpdates over a single Update, or vice versa
    /// https://docs.unity3d.com/Manual/ExecutionOrder.html
    /// </summary>
    private void FixedUpdate()
    {
        // Update internal state
        Raycaster.CollisionInfo collisions = raycaster.Collide(groundCheckProtrusion);
        bool prevIsGrounded = isGrounded;
        isGrounded = collisions.below;
        if (isGrounded)
        {
            jumpCount = 0;
        }

        if (prevIsGrounded && !isGrounded)
        {
            leftGroundTime = Time.time;
        }
        else if (!prevIsGrounded && isGrounded)
        {
            touchedGroundTime = Time.time;
            IsLandingThisFrameCallback?.Invoke();
        }

        // Update velocity
        velocity = rb2d.velocity;
        velocity.x = walkDir * walkVelocity;
        if (isJumpingThisFrame)
        {
            if (isGrounded)
            {
                DoJump();
            }
            else if (jumpCount < 1 &&
                     jumpActuatedTime - leftGroundTime <= lateJumpTimeTolerance)
            {
                DoJump();
            }
        }
        else if (isGrounded && jumpActuatedTime > 0 &&
                 touchedGroundTime - jumpActuatedTime <= earlyJumpTimeTolerance)
        {
            DoJump();
        }

        rb2d.velocity = velocity;

        // Reset transient frame variables
        isJumpingThisFrame = false;


        // velocity = rb2d.velocity;
        // Debug.Log(rb2d.gravityScale);
        // bool jumpReleased = Input.GetButtonUp("Jump") || Input.GetAxisRaw("Vertical") <= 0;
        // if (Mathf.Approximately(velocity.y, 0))
        if (isGrounded)
        {
            // Debug.Log("IsGrounded");
            rb2d.gravityScale = 1.0f;
        }
        else if (velocity.y < 0 || (velocity.y > 0 && jumpReleased))
            // else if (jumpReleased)
        {
            // Debug.Log("Other");
            rb2d.gravityScale = gravityScale;
            // rb2d.gravityScale = 1.0f;
        }

        return;

        void DoJump()
        {
            jumpReleased = false;
            // rb2d.gravityScale = gravityScale;

            jumpCount++;
            velocity.y = jumpVelocity;
            jumpActuatedTime = -1f;
            IsJumpingThisFrameCallback?.Invoke();
        }
    }

    #region Public Methods

    public void Walk(float walkDir)
    {
        this.walkDir = Mathf.Clamp(walkDir, -1f, 1f);
    }

    public void Jump()
    {
        isJumpingThisFrame = true;
        jumpActuatedTime = Time.time;
    }

    public float WalkDir => walkDir;

    public bool IsGrounded => isGrounded;

    public Vector2 Velocity => velocity;

    public float JumpActuatedTime => jumpActuatedTime;

    #endregion
}