using UnityEngine;

namespace Jusw85.Common.Demo
{
    public class DemoPushing : MonoBehaviour
    {
        private Rigidbody2D rb;

        public Vector2 velocity;

        void Start()
        {
            Physics2D.gravity = Vector2.zero;
            Physics2D.SetLayerCollisionMask(LayerMask.NameToLayer("Default"), LayerMask.GetMask("Default"));
            rb = GetComponent<Rigidbody2D>();
        }

        void FixedUpdate()
        {
            rb.velocity = velocity;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            Debug.Log("OnCollisionEnter2D");
            velocity = Vector2.zero;
        }
    }
}