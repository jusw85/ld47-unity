using UnityEngine;

namespace Jusw85.Common.Demo
{
    [RequireComponent(typeof(KinematicPlatformController))]
    public class DemoPlayerInput : MonoBehaviour
    {
        [SerializeField] private float horizontalDeadzone = 0.2f;
        [SerializeField] private float verticalDeadzone = 0.2f;

        private KinematicPlatformController platformController;

        private void Awake()
        {
            platformController = GetComponent<KinematicPlatformController>();
        }

        private void Update()
        {
            Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            if (Mathf.Abs(moveInput.x) > horizontalDeadzone)
            {
                int walkDir = Mathf.Sign(moveInput.x) > 0 ? 1 : -1;
                platformController.Walk(walkDir);
            }
            else
            {
                platformController.Walk(0);
            }

            bool upKeyPressed = moveInput.y > verticalDeadzone;
            bool jumpKeyPressed = Input.GetButtonDown("Jump");
            bool jumpPressed = jumpKeyPressed || upKeyPressed;
            if (jumpPressed)
            {
                platformController.Jump();
            }
        }
    }
}