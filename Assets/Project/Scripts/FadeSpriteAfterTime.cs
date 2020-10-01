using Jusw85.Common;
using Prime31.ZestKit;
using UnityEngine;

public class FadeSpriteAfterTime : MonoBehaviour
{
    [SerializeField] private float initialDelay;
    [SerializeField] private float fadeTime;
    private SpriteRenderer rend;

    private void Awake()
    {
        rend = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        StartCoroutine(CoroutineUtils.DelaySeconds(() => { rend.ZKalphaTo(0, fadeTime).start(); }, initialDelay));
    }
}