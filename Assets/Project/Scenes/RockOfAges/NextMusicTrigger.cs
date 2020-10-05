using System;
using Jusw85.Common;
using Prime31;
using UnityEngine;

public class NextMusicTrigger : MonoBehaviour
{
    private SoundKit sk;
    [SerializeField] private AudioClip clip;
    private void Start()
    {
        sk = Toolbox.Instance.TryGet<SoundKit>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        sk.playBackgroundMusic(clip, 1.0f);
        gameObject.SetActive(false);
    }
}