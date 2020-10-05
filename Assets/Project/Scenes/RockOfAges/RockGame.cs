using Jusw85.Common;
using k;
using Prime31;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RockGame : MonoBehaviour
{
    [SerializeField] private AudioClip bgm;
    private SoundKit soundKit;

    private void Start()
    {
        soundKit = Toolbox.Instance.TryGet<SoundKit>();
        soundKit.playBackgroundMusic(bgm, 1.0f,false);

        SceneManager.LoadSceneAsync(Scenes.EFFECTS, LoadSceneMode.Additive);
    }
}