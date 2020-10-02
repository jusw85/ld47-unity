using k;
using UnityEngine;

public class StartScreen : MonoBehaviour
{
    public void NextScreen()
    {
        GameObject.FindWithTag(Tags.MAIN_GAME)?.GetComponent<MainGame>()?.ExitStart();
    }

    // private MyFadeTransition fader;
    //
    // private void Awake()
    // {
    //     fader = new MyFadeTransition()
    //     {
    //         nextScene = Scenes.nextSceneIndex(),
    //     };
    // }
    //
    // private void Update()
    // {
    //     if (Input.anyKeyDown)
    //     {
    //         enabled = false;
    //         TransitionKit.instance.transitionWithDelegate(fader);
    //         // SceneManager.LoadScene(Scenes.DYNAMIC_PLATFORMING, LoadSceneMode.Single);
    //     }
    // }
    //
    // private class MyFadeTransition : TransitionKitDelegate
    // {
    //     public Color fadeToColor = Color.black;
    //     public float duration = 0.5f;
    //     public float fadedDelay = 0.2f;
    //     public int nextScene = -1;
    //
    //     public Shader shaderForTransition()
    //     {
    //         return Shader.Find("prime[31]/Transitions/Fader");
    //     }
    //
    //     public Mesh meshForDisplay()
    //     {
    //         return null;
    //     }
    //
    //     public Texture2D textureForDisplay()
    //     {
    //         return null;
    //     }
    //
    //     public IEnumerator onScreenObscured(TransitionKit transitionKit)
    //     {
    //         transitionKit.transitionKitCamera.clearFlags = CameraClearFlags.Nothing;
    //         transitionKit.material.color = fadeToColor;
    //
    //         MainGame mainGame = null;
    //         AsyncOperation op;
    //         if (nextScene >= 0)
    //         {
    //             op = SceneManager.LoadSceneAsync(nextScene);
    //             op.completed += operation =>
    //             {
    //                 GameObject obj = GameObject.FindWithTag(Tags.MAIN_GAME);
    //                 mainGame = obj.GetComponent<MainGame>();
    //                 mainGame.SelfInit = false;
    //             };
    //         }
    //
    //         yield return transitionKit.StartCoroutine(transitionKit.tickProgressPropertyInMaterial(duration));
    //
    //         transitionKit.makeTextureTransparent();
    //
    //         if (fadedDelay > 0)
    //             yield return new WaitForSeconds(fadedDelay);
    //
    //         // we dont transition back to the new scene unless it is loaded
    //         if (nextScene >= 0)
    //             yield return transitionKit.StartCoroutine(transitionKit.waitForLevelToLoad(nextScene));
    //         
    //         mainGame?.Init();
    //
    //         yield return transitionKit.StartCoroutine(transitionKit.tickProgressPropertyInMaterial(duration, true));
    //     }
    // }
}