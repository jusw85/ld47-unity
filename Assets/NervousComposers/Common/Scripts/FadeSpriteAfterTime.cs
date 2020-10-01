using System.Collections;
using Jusw85.Common;
using Prime31.ZestKit;
using UnityEngine;

public class FadeSpriteAfterTime : MonoBehaviour
{
    [SerializeField] private float initialDelay;
    [SerializeField] private float fadeTime;
    [SerializeField] private EaseType easeType = EaseType.QuartIn;
    private SpriteRenderer rend;

    private void Awake()
    {
        rend = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        IEnumerator coroutine = CoroutineUtils.DelaySeconds(
            () => { rend.ZKalphaTo(0, fadeTime).setEaseType(easeType).start(); },
            initialDelay);
        StartCoroutine(coroutine);
    }
}