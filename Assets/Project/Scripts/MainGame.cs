using System.Collections;
using Jusw85.Common;
using k;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Notes:
/// https://docs.unity3d.com/ScriptReference/SceneManagement.EditorSceneManager-preventCrossSceneReferences.html
/// </summary>
public class MainGame : MonoBehaviour
{
    private HUDManager hudManager;

    private void Start()
    {
        IEnumerator loadHud = CoroutineUtils.Chain(this,
            CoroutineUtils.Do(() => { TryLoadSceneAdditive(Scenes.UI); }),
            CoroutineUtils.Do(() =>
            {
                hudManager = GameObject.FindWithTag(Tags.HUDMANAGER)?.GetComponent<HUDManager>();
            }),
            CoroutineUtils.Do(() => { TryLoadSceneAdditive(Scenes.LEVEL1); }),
            CoroutineUtils.Do(() =>
            {
                Scene level1 = SceneManager.GetSceneByName(Scenes.LEVEL1);
                SceneManager.SetActiveScene(level1);
            })
        );

        StartCoroutine(loadHud);
    }

    private void TryLoadSceneAdditive(string name)
    {
        Scene hudScene = SceneManager.GetSceneByName(name);
        if (!(Application.isEditor && hudScene.isLoaded))
        {
            SceneManager.LoadScene(name, LoadSceneMode.Additive);
        }
    }


    // [SerializeField] private AudioClip bgm;
    // private HUDManager hudManager;
    // private SoundKit soundKit;
    //
    // private bool selfInit = true;
    // private bool isInitialised;
    // public event Action InitialisedCallback;
    //
    // public bool SelfInit
    // {
    //     get => selfInit;
    //     set => selfInit = value;
    // }
    //
    // private void Start()
    // {
    //     soundKit = Toolbox.Instance.Get<SoundKit>();
    //
    //     enabled = false;
    //     if (SelfInit)
    //     {
    //         Init();
    //     }
    // }
    //
    // public void Init()
    // {
    //     LoadHUD();
    //     soundKit.playBackgroundMusic(bgm, 1.0f);
    // }
    //
    // private void LoadHUD()
    // {
    //     Scene hudScene = SceneManager.GetSceneByName(Scenes.HUD);
    //
    //     if (Application.isEditor && hudScene.isLoaded)
    //     {
    //         InitHUD();
    //         enabled = true;
    //     }
    //     else
    //     {
    //         AsyncOperation op = SceneManager.LoadSceneAsync(Scenes.HUD, LoadSceneMode.Additive);
    //         op.completed += operation =>
    //         {
    //             InitHUD();
    //             enabled = true;
    //             
    //             isInitialised = true;
    //             InitialisedCallback?.Invoke();
    //         };
    //     }
    //     return;
    //
    //     void InitHUD()
    //     {
    //         Camera[] cams = FindObjectsOfType<Camera>();
    //         foreach (Camera cam in cams)
    //         {
    //             if (cam.gameObject.scene.name.Equals(Scenes.HUD))
    //             {
    //                 cam.gameObject.SetActive(false);
    //                 break;
    //             }
    //         }
    //
    //         GameObject obj = GameObject.FindWithTag(Tags.HUDMANAGER);
    //         if (obj != null)
    //         {
    //             hudManager = obj.GetComponent<HUDManager>();
    //         }
    //         else
    //         {
    //             Debug.LogError("HUDManager is null!");
    //         }
    //     }
    // }
    //
    // [SerializeField] private float totalTime;
    // private bool isEnding;
    // private void Update()
    // {
    //     if (!isEnding)
    //     {
    //         totalTime -= Time.deltaTime;
    //         if (totalTime <= 0f)
    //         {
    //             totalTime = 0f;
    //             Win();
    //         }
    //         hudManager.SetTime(Mathf.FloorToInt(totalTime));
    //     }
    // }
    //
    // public void Lose()
    // {
    //     isEnding = true;
    //     PlayerInput pi = GameObject.FindWithTag(Tags.PLAYER)?.GetComponent<PlayerInput>();
    //     pi.IsEnding = true;
    //     Crystal c = GameObject.FindWithTag(Tags.CRYSTAL)?.GetComponent<Crystal>();
    //     c.IsEnding = true;
    //     hudManager.Lose();
    //     StartCoroutine(CoroutineUtils.DelaySeconds(() =>
    //     {
    //         FadeTransition tr = new FadeTransition {nextScene = 4};
    //         TransitionKit.instance.transitionWithDelegate(tr);
    //         // SceneManager.LoadScene(Scenes.LOSE);
    //     }, 8f));
    // }
    //
    // private void Win()
    // {
    //     isEnding = true;
    //     PlayerInput pi = GameObject.FindWithTag(Tags.PLAYER)?.GetComponent<PlayerInput>();
    //     pi.IsEnding = true;
    //     Crystal c = GameObject.FindWithTag(Tags.CRYSTAL)?.GetComponent<Crystal>();
    //     c.IsEnding = true;
    //     hudManager.Win();
    //     StartCoroutine(CoroutineUtils.DelaySeconds(() =>
    //     {
    //         FadeTransition tr = new FadeTransition {nextScene = 3};
    //         TransitionKit.instance.transitionWithDelegate(tr);
    //     }, 8f));
    // }
    //
    // public HUDManager HudManager => hudManager;
    //
    // public bool IsInitialised => isInitialised;
}