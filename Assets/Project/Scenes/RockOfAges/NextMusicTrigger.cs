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

    private void Start()
    {
        sk = Toolbox.Instance.TryGet<SoundKit>();

        GameObject treeObj = GameObject.FindGameObjectWithTag(Tags.TREES);
        GameObject grassObj = GameObject.FindGameObjectWithTag(Tags.GRASS);

        treeAnimators = treeObj.GetComponentsInChildren<Animator>();
        grassAnimators = grassObj.GetComponentsInChildren<Animator>();

        skyboxMaterial = RenderSettings.skybox;
        skyboxMaterial.SetFloat("_AtmosphereThickness", atmosphereThickness);
    }

    private void Update()
    {
        if (!triggered && !sk.backgroundSound.audioSource.isPlaying)
        {
            triggered = true;
            sk.playBackgroundMusic(clip, 1.0f);

            foreach (Animator animator in treeAnimators)
            {
                animator.SetTrigger("Dying");
            }

            foreach (Animator animator in grassAnimators)
            {
                animator.SetTrigger("Dying");
            }

            gameObject.SetActive(false);

            CustomTweenTarget tweenTarget = new CustomTweenTarget(skyboxMaterial);
            FloatTween tween = new FloatTween(tweenTarget, 1.0f, 5.0f, 3.0f);
            tween.start();
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