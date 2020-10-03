using UnityEngine;

public class SissyphusMovement : MonoBehaviour
{
    [SerializeField] private float initialMoveSpeed = 5f;
    [SerializeField] private float minMoveSpeed = 2f;
    [SerializeField] private float heightDampening = 0.5f;
    
    private float moveSpeed;
    private Rigidbody2D rb2d;
    private float initialY = -3f;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float newMoveSpeed = initialMoveSpeed - ((transform.position.y - initialY) * heightDampening);
        moveSpeed = Mathf.Clamp(newMoveSpeed, minMoveSpeed, initialMoveSpeed);
            
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 velocity = rb2d.velocity;
        velocity.x = moveInput.x * moveSpeed;
        if (moveInput.x == 0f && velocity.y > 0)
        {
            velocity.y = 0f;
        }

        rb2d.velocity = velocity;
    }
}