using Jusw85.Common;
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
    [SerializeField] private AudioEvent gruntAudio;
    [SerializeField] [MinMaxRange(1, 10)] private RangedFloat gruntInterval = new RangedFloat(2, 3);
    private float currentGruntInterval;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        soundkit = Toolbox.Instance.TryGet<SoundKit>();
        currentGruntInterval = Random.Range(gruntInterval.Min, gruntInterval.Max);
    }

    private void Update()
    {
        float heightDampening = (transform.position.y - initialY) * heightDampeningFactor;
        buttonBoost -= 1.0f * Time.deltaTime;
        
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

        rb2d.velocity = velocity;
    }
}