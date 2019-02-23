using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {

    public GameObject RotatingSquare;
    public GameObject OverlayFade;

    private Animator FadeAnimator;

    public void Start () {
        FadeAnimator = OverlayFade.GetComponent<Animator> ();

        RotatingSquare.SetActive (false);
        FadeAnimator.SetBool ("Loading", false);
    }

    public void LoadLevel (string sceneName) {
        RotatingSquare.SetActive (true);
        FadeAnimator.SetBool ("Loading", true);
        StartCoroutine (Countdown (1.5f, () => { StartCoroutine (LoadAsynchronously (sceneName)); }));
    }

    // Reusable timer that will execute CODE_HERE after the timer is done --> used in fight timers and such
    // This is creating a CoRoutine which runs independently of the function it is called from
    // StartCoroutine (Countdown (3f, () => {CODE_HERE}));
    IEnumerator Countdown (float seconds, Action onComplete) {
        yield return new WaitForSecondsRealtime (seconds);
        onComplete ();
    }

    // Loads any given scene (via string) asynchronously, meaning it loads it in the background without unity stopping the current scene
    IEnumerator LoadAsynchronously (string sceneName) {
        AsyncOperation operation = SceneManager.LoadSceneAsync (sceneName);
        while (!operation.isDone) {
            //Debug.Log (operation.progress);
            yield return null;
        }
    }
}