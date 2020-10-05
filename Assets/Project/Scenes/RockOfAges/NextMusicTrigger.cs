using System;
using Jusw85.Common;
using k;
using Prime31;
using Prime31.ZestKit;
using UnityEngine;

public class NextMusicTrigger : MonoBehaviour
{
    private SoundKit sk;
    [SerializeField] private AudioClip clip;
    private bool triggered;

    private Animator[] treeAnimators;
    private Animator[] grassAnimators;

    private Material skyboxMaterial;
    private float atmosphereThickness = 1.0f;

    [SerializeField] private Rigidbody2D rock;
    [SerializeField] private float rockMass;
    [SerializeField] private SissyphusMovement sissyphusMovement;

    [SerializeField] private float maxButtonBoost = 8;
    [SerializeField] private float maxMoveSpeed = 7;
    
    private CustomTweenTarget tweenTarget;
    private void Start()
    {
        sk = Toolbox.Instance.TryGet<SoundKit>();

        GameObject treeObj = GameObject.FindGameObjectWithTag(Tags.TREES);
        GameObject grassObj = GameObject.FindGameObjectWithTag(Tags.GRASS);

        treeAnimators = treeObj.GetComponentsInChildren<Animator>();
        grassAnimators = grassObj.GetComponentsInChildren<Animator>();

        skyboxMaterial = RenderSettings.skybox;
        skyboxMaterial.SetFloat("_AtmosphereThickness", atmosphereThickness);
        tweenTarget = new CustomTweenTarget(skyboxMaterial);
    }

    private void Update()
    {
        if (!sk.backgroundSound.audioSource.isPlaying)
        {
            // if (!triggered)
            // {
            //     triggered = true;
            //
            // }

            foreach (Animator animator in treeAnimators)
            {
                animator.SetTrigger("Dying");
            }

            foreach (Animator animator in grassAnimators)
            {
                animator.SetTrigger("Dying");
            }

            FloatTween tween = new FloatTween(tweenTarget, 1.0f, 5.0f, 3.0f);
            tween.start();
            sk.playBackgroundMusic(clip, 1.0f, false);

            // gameObject.SetActive(false);

            rock.mass = rockMass;
            sissyphusMovement.InputDisabled = true;
            StartCoroutine(CoroutineUtils.DelaySeconds(() =>
            {
                rock.mass = 1f;
                sissyphusMovement.InputDisabled = false;
                sissyphusMovement.MaxMoveSpeed = maxMoveSpeed;
                sissyphusMovement.MaxButtonBoost = maxButtonBoost;
                
                FloatTween tween2 = new FloatTween(tweenTarget, 5.0f, 1.0f, 3.0f);
                tween2.start();
                
                foreach (Animator animator in treeAnimators)
                {
                    animator.SetTrigger("Alive");
                }

                foreach (Animator animator in grassAnimators)
                {
                    animator.SetTrigger("Alive");
                }
            }, 11f));
            // ITween<float> tween = PropertyTweens.floatPropertyTo(this, "atmosphereThickness", 5, 3.0f);
            // tween.start();
            // float atmosphereThickness = 1f;
        }

        // atmosphereThickness = Mathf.Clamp(atmosphereThickness, 0f, 5f);
        // skyboxMaterial.SetFloat("_AtmosphereThickness", atmosphereThickness);
    }

    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     sk.playBackgroundMusic(clip, 1.0f);
    //     gameObject.SetActive(false);
    // }

    public class CustomTweenTarget : AbstractTweenTarget<Material, float>
    {
        public static readonly int MATERIAL_FLASHAMOUNT_ID = Shader.PropertyToID("_AtmosphereThickness");

        public override void setTweenedValue(float value)
        {
            _target.SetFloat(MATERIAL_FLASHAMOUNT_ID, value);
        }

        public override float getTweenedValue()
        {
            return _target.GetFloat(MATERIAL_FLASHAMOUNT_ID);
        }

        public CustomTweenTarget(Material material)
        {
            _target = material;
        }
    }
}