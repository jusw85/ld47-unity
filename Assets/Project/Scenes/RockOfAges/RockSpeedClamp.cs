using System;
using Jusw85.Common;
using Prime31;
using UnityEngine;

public class RockSpeedClamp : MonoBehaviour
{
    [SerializeField] private float maxHorizontalVelocity = 2f;
    [SerializeField, HideInInspector] private Rigidbody2D rb2d;
    
    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector2 vel = rb2d.velocity;
        vel.x = Mathf.Clamp(vel.x, -maxHorizontalVelocity, maxHorizontalVelocity);
        rb2d.velocity = vel;
    }
}