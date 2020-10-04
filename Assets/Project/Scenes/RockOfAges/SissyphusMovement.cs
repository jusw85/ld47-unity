﻿using Jusw85.Common;
using k;
using MyBox;
using Prime31;
using UnityEngine;

public class SissyphusMovement : MonoBehaviour
{
    [SerializeField] private float initialMoveSpeed = 5f;
    [SerializeField] private float minMoveSpeed = 2f;
    [SerializeField] private float heightDampeningFactor = 0.5f;

    private float moveSpeed;
    private Rigidbody2D rb2d;
    private float initialY = -3f;
    float buttonBoost;

    private SoundKit soundkit;
    [SerializeField, HideInInspector] private Animator animator;
    [SerializeField] private AudioEvent gruntAudio;
    [SerializeField] [MinMaxRange(1, 10)] private RangedFloat gruntInterval = new RangedFloat(2, 3);
    private float currentGruntInterval;

    // https://github.com/TwoTailsGames/Unity-Built-in-Shaders/blob/master/DefaultResourcesExtra/Skybox-Procedural.shader
    private Material skyboxMaterial;
    [SerializeField] private AnimationCurve skyboxYCurve;
    
    private void Start()
    {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        soundkit = Toolbox.Instance.TryGet<SoundKit>();
        currentGruntInterval = Random.Range(gruntInterval.Min, gruntInterval.Max);
        skyboxMaterial = RenderSettings.skybox;
    }

    private void Update()
    {
        float heightDampening = (transform.position.y - initialY) * heightDampeningFactor;
        buttonBoost -= 1.0f * Time.deltaTime;
        
        float atmosphereThickness = skyboxYCurve.Evaluate(transform.position.y);
        atmosphereThickness = Mathf.Clamp(atmosphereThickness, 0f, 5f);
        skyboxMaterial.SetFloat("_AtmosphereThickness", atmosphereThickness);
        // float atmosphereThickness = skyboxMaterial.GetFloat("_AtmosphereThickness");
        // atmosphereThickness = Mathf.Clamp(atmosphereThickness, 0f, 5f);
        // skyboxMaterial.SetFloat("_AtmosphereThickness", atmosphereThickness + 0.1f);
        
        if (Input.GetButtonDown("Fire1"))
        {
            buttonBoost += 0.5f;
        }
        
        heightDampening = Mathf.Clamp(heightDampening, heightDampening, 10f);
        buttonBoost = Mathf.Clamp(buttonBoost, 0f, 10f);
        float newMoveSpeed = initialMoveSpeed + buttonBoost - heightDampening;
        moveSpeed = Mathf.Clamp(newMoveSpeed, minMoveSpeed, initialMoveSpeed);

        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 velocity = rb2d.velocity;
        velocity.x = moveInput.x * moveSpeed;
        if (moveInput.x == 0f && velocity.y > 0)
        {
            velocity.y = 0f;
        }

        if (moveInput.x > 0)
        {
            currentGruntInterval -= Time.deltaTime;
            if (currentGruntInterval <= 0)
            {
                currentGruntInterval = Random.Range(gruntInterval.Min, gruntInterval.Max);
                gruntAudio.Play(soundkit);
            }
        }

        
        if (!Mathf.Approximately(moveInput.x, 0))
        {
            SetIsFacingRight(moveInput.x > 0);
        }
        animator.SetBool(AnimatorParams.IS_WALKING, Mathf.Abs(velocity.x) > 0);
        rb2d.velocity = velocity;
    }
    
    private bool isFacingRight = true;
    public bool IsFacingRight => isFacingRight;
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
}