using Jusw85.Common;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    [SerializeField] private float delay;

    private void Start()
    {
        StartCoroutine(CoroutineUtils.DelaySeconds(() => { Destroy(gameObject); }, delay));
    }
}