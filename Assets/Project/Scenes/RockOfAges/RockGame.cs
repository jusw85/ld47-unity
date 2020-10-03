using Jusw85.Common;
using Prime31;
using UnityEngine;

public class RockGame : MonoBehaviour
{
    [SerializeField] private AudioClip bgm;
    private SoundKit soundKit;

    private void Start()
    {
        soundKit = Toolbox.Instance.TryGet<SoundKit>();
        soundKit.playBackgroundMusic(bgm, 1.0f);
    }
}