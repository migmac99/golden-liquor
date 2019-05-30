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
}