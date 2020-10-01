using UnityEngine;

namespace Jusw85.Common
{
    /// <summary>
    /// Usage:
    /// - Set script execution after other scripts have updated frameInfo, but before Physics2D.Simulate
    /// - Make sure API Compatibility Level >= .NET 4
    ///
    /// Future:
    /// Allow acceleration to be set and persist between frames
    /// Allow different jump physics such as variable jump, or constant velocity for fixed time frame
    /// Clamp vertical and horizontal velocity to prevent infinite falling speed
    /// Add callbacks such as isJumpingThisFrame
    /// Add other ways to define jump physics, such as locking one var to define the other two
    /// Currently walking is fixed velocity, add option for smoothing start and end
    /// Allow double jumps
    /// Handle Slopes
    /// Possibly use scriptable objects as composable behaviours e.g. double jump, walk through walls, etc.
    /// </summary>
    [RequireComponent(typeof(Raycaster), typeof(Rigidbody2D))]
    public class KinematicPlatformController : MonoBehaviour
    {
        #region Member Variables

        [SerializeField] private float jumpHeight = 4;
        [SerializeField] private float timeToJumpApex = 0.4f;
        [SerializeField] private float walkVelocity = 8;
        [SerializeField] private float earlyJumpTimeTolerance = 0.1f;
        [SerializeField] private float lateJumpTimeTolerance = 0.1f;
        private float gravity;
        private float jumpVelocity;

        #endregion

        #region Cached Variables

        private Raycaster raycaster;
        private Rigidbody2D rb2d;
        private float dt;

        #endregion

        #region Internal State Variables

        private FrameInfo frameInfo;
        private Vector2 velocity;
        private bool isGrounded;
        private int jumpCount;
        private float earlyJumpCountdownTimer = -1f;
        private float lateJumpCountdownTimer = -1f;

        #endregion

        private void Awake()
        {
            raycaster = GetComponent<Raycaster>();
            rb2d = GetComponent<Rigidbody2D>();
            UpdateGravity();
        }

        private void Update()
        {
            dt = Time.deltaTime;
            earlyJumpCountdownTimer = Mathf.Clamp(earlyJumpCountdownTimer - dt, -1f, float.MaxValue);
            lateJumpCountdownTimer = Mathf.Clamp(lateJumpCountdownTimer - dt, -1f, float.MaxValue);

            UnwrapFrameInfo();
            frameInfo = new FrameInfo();

            Vector2 acceleration = new Vector2(0, gravity);
            Vector2 u = velocity;
            Vector2 v = u + (acceleration * dt);
            Vector2 ds = 0.5f * (u + v) * dt;

            Raycaster.CollisionInfo collisions = raycaster.Collide(ds);

            rb2d.position += collisions.vec;
            bool prevIsGrounded = isGrounded;
            isGrounded = collisions.below;
            if (isGrounded)
            {
                jumpCount = 0;
            }
            if (prevIsGrounded && !isGrounded)
            {
                lateJumpCountdownTimer = lateJumpTimeTolerance;
            }

            velocity = v;
            if (collisions.below || collisions.above)
            {
                velocity.y = 0;
            }
        }

        private void OnValidate()
        {
            UpdateGravity();
            earlyJumpTimeTolerance = Mathf.Clamp(earlyJumpTimeTolerance, 0, float.MaxValue);
            lateJumpTimeTolerance = Mathf.Clamp(lateJumpTimeTolerance, 0, float.MaxValue);
        }

        private void UpdateGravity()
        {
            gravity = (2 * -jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
            jumpVelocity = -(gravity * timeToJumpApex);
        }

        private void UnwrapFrameInfo()
        {
            velocity.x = frameInfo.walkDir * walkVelocity;
            if (frameInfo.jump)
            {
                if (jumpCount < 1 && !isGrounded && lateJumpCountdownTimer >= 0f)
                {
                    DoJump();
                }
                else
                {
                    earlyJumpCountdownTimer = earlyJumpTimeTolerance;
                }
            }

            if (jumpCount < 1 && isGrounded && earlyJumpCountdownTimer >= 0f)
            {
                DoJump();
            }

            return;

            void DoJump()
            {
                jumpCount++;
                velocity.y = jumpVelocity;
                earlyJumpCountdownTimer = -1f;
                lateJumpCountdownTimer = -1f;
            }
        }

        private struct FrameInfo
        {
            public int walkDir;
            public bool jump;
        }

        #region Public Methods

        public void Walk(int walkDir)
        {
            frameInfo.walkDir = walkDir;
        }

        public void Jump()
        {
            frameInfo.jump = true;
        }

        #endregion
    }
}