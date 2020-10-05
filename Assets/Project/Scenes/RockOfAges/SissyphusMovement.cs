using Jusw85.Common;
using k;
using MyBox;
using Prime31;
using UnityEngine;

public class SissyphusMovement : MonoBehaviour
{
    [SerializeField] private float initialMoveSpeed = 5f;
    [SerializeField] private float minMoveSpeed = 2f;
    [SerializeField] private float heightDampeningFactor = 0.5f;
    [SerializeField] private Collider2D rockTestCollider;
    [SerializeField] private Collider2D rockCollider;
    [SerializeField] private RockRotation rockRotation;
    [SerializeField] private float buttonBoostValue = 0.5f;
    [SerializeField] private GameObject dustParticle;
    [SerializeField] private Transform dustSource;

    private float moveSpeed;
    private Rigidbody2D rb2d;
    private float initialY = -3f;
    float buttonBoost;

    private SoundKit soundkit;
    [SerializeField, HideInInspector] private Raycaster raycaster;
    [SerializeField, HideInInspector] private Animator animator;
    [SerializeField] private AudioEvent gruntAudio;
    [SerializeField] [MinMaxRange(1, 10)] private RangedFloat gruntInterval = new RangedFloat(2, 3);
    private float currentGruntInterval;


    [SerializeField] private float maxButtonBoost = 5f;
    [SerializeField] private float maxHeightDampen = 5f;
    [SerializeField] private float maxMoveSpeed = 10f;
    [SerializeField] private float buttonBoostDecay = 1f;

    // https://github.com/TwoTailsGames/Unity-Built-in-Shaders/blob/master/DefaultResourcesExtra/Skybox-Procedural.shader
    private Material skyboxMaterial;
    [SerializeField] private AnimationCurve skyboxYCurve;

    private float dustCooldown = 0.5f;
    private float currentDustCooldown;
    private bool inputDisabled;

    public bool InputDisabled
    {
        get => inputDisabled;
        set => inputDisabled = value;
    }

    public float MaxButtonBoost
    {
        get => maxButtonBoost;
        set => maxButtonBoost = value;
    }

    public float MaxMoveSpeed
    {
        get => maxMoveSpeed;
        set => maxMoveSpeed = value;
    }

    private void Start()
    {
        raycaster = GetComponent<Raycaster>();
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        soundkit = Toolbox.Instance.TryGet<SoundKit>();
        currentGruntInterval = Random.Range(gruntInterval.Min, gruntInterval.Max);
        skyboxMaterial = RenderSettings.skybox;
    }

    private void Update()
    {
        bool isPushingRock = rockTestCollider.IsTouching(rockCollider);
        if (!isPushingRock)
        {
            moveSpeed = 3;
        }
        else
        {
            rockRotation.RotateTransform();
        }

        float heightDampening = (transform.position.y - initialY) * heightDampeningFactor;
        buttonBoost -= buttonBoostDecay * Time.deltaTime;

        // float atmosphereThickness = skyboxYCurve.Evaluate(transform.position.y);
        // atmosphereThickness = Mathf.Clamp(atmosphereThickness, 0f, 5f);
        // skyboxMaterial.SetFloat("_AtmosphereThickness", atmosphereThickness);
        if (Input.GetButtonDown("Fire1"))
        {
            buttonBoost += buttonBoostValue;
        }

        heightDampening = Mathf.Clamp(heightDampening, heightDampening, maxHeightDampen);
        buttonBoost = Mathf.Clamp(buttonBoost, 0f, maxButtonBoost);
        float newMoveSpeed = initialMoveSpeed + buttonBoost - heightDampening;
        moveSpeed = Mathf.Clamp(newMoveSpeed, minMoveSpeed, maxMoveSpeed);

        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (moveInput.x < 0)
        {
            moveInput.x = 0;
        }

        Vector2 velocity = rb2d.velocity;
        velocity.x = moveInput.x * moveSpeed;

        if (inputDisabled)
        {
            velocity.x = 0f;
        }

        if (moveInput.x == 0f && velocity.y > 0)
        {
            velocity.y = 0f;
        }

        rb2d.velocity = velocity;

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

        if (isPushingRock)
        {
            animator.SetBool(AnimatorParams.IS_WALKING, true);
            animator.SetBool(AnimatorParams.IS_PUSHING, true);
        }
        else
        {
            animator.SetBool(AnimatorParams.IS_PUSHING, false);
            animator.SetBool(AnimatorParams.IS_WALKING, Mathf.Abs(velocity.x) > 0 || Mathf.Abs(moveInput.x) > 0);
        }

        currentDustCooldown -= Time.deltaTime;
        if (velocity.y <= -0.5 && currentDustCooldown <= 0)
        {
            currentDustCooldown = dustCooldown;
            Instantiate(dustParticle, dustSource.transform.position, Quaternion.identity);
        }
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

    public float ButtonBoost
    {
        get => buttonBoost;
        set => buttonBoost = value;
    }
}