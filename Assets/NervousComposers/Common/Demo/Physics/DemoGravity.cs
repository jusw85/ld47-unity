using UnityEngine;

namespace Jusw85.Common.Demo
{
    public class DemoGravity : MonoBehaviour
    {
        public bool autoSimulation;
        public float walkSpeed = 10f;
        public float gravity = -10f;
        private Rigidbody2D rb2d;

        private void Awake()
        {
            Physics2D.autoSimulation = autoSimulation;
            Physics2D.gravity = new Vector2(0, gravity);
            rb2d = GetComponent<Rigidbody2D>();
            rb2d.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
            rb2d.interpolation = RigidbodyInterpolation2D.Interpolate;
        }

        private void Update()
        {
            var moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            var v = rb2d.velocity;
            v.x = moveInput.x * walkSpeed;
            rb2d.velocity = v;
        }

        private void LateUpdate()
        {
            if (!autoSimulation)
            {
                Physics2D.Simulate(Time.deltaTime);
            }
        }
    }
}