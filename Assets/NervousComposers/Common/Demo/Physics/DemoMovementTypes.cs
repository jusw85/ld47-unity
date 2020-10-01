using UnityEngine;

namespace Jusw85.Common.Demo
{
    public class DemoMovementTypes : MonoBehaviour
    {
        public float speed = 1;
        public MovementType type;

        private Rigidbody2D rb2d;

        void Start()
        {
            Physics2D.autoSimulation = false;
            rb2d = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            var vec = Vector2.right * speed * Time.deltaTime;
            if (type == MovementType.TransformTranslate)
            {
                transform.Translate(vec);
                Physics2D.SyncTransforms();
            }
            else if (type == MovementType.Rb2dPosition)
            {
                Vector2 p = transform.position;
                rb2d.position = p + vec;
                Physics2D.Simulate(Mathf.Epsilon);
            }
        }

        public enum MovementType
        {
            TransformTranslate,
            Rb2dPosition,
        }
    }
}