using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SissyphusMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5;
    private Rigidbody2D rb2d;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        Vector2 velocity = rb2d.velocity; 
        velocity.x = moveInput.x * moveSpeed;
        rb2d.velocity = velocity;
    }
}