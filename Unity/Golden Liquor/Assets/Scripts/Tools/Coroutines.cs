using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//REQUIRES SINGLETON.CS SCRIPT
public class Coroutines : Singleton<Coroutines> {
    // Reusable timer that will execute CODE_HERE after the timer is done --> used in fight timers and such
    // This is creating a CoRoutine which runs independently of the function it is called from
    // StartCoroutine (Coroutines.Instance.Countdown (3f, () => {CODE_HERE}));
    public IEnumerator Countdown (float seconds, Action onComplete) {
        yield return new WaitForSecondsRealtime (seconds);
        onComplete ();
    }

    // Loads any given scene (via string) asynchronously, meaning it loads it in the background without unity stopping the current scene
    public IEnumerator LoadAsynchronously (string sceneName) {
        AsyncOperation operation = SceneManager.LoadSceneAsync (sceneName);
        while (!operation.isDone) {
            //Debug.Log (operation.progress);
            yield return null;
        }
    }

    // Lerps between two values over a set duration of time regardless of timescale
    // public IEnumerator changeValueOverTime (float fromVal, float toVal, float duration) {
    //     float counter = 0f;

    //     while (counter < duration) {
    //         if (Time.timeScale == 0)
    //             counter += Time.unscaledDeltaTime;
    //         else
    //             counter += Time.deltaTime;

    //         float val = Mathf.Lerp (fromVal, toVal, counter / duration);
    //         //Debug.Log ("Val: " + val);
    //         yield return null;
    //     }
    // }
}