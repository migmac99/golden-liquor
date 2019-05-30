using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//REQUIRES SINGLETON.CS SCRIPT
public class Coroutines : Singleton<Coroutines> {
    // Reusable timer that will execute CODE_HERE after the timer is done --> used in fight timers and such
    // This is creating a CoRoutine which runs independently of the function it is called from
    // StartCoroutine (Coroutines.Instance.Countdown (3f, () => {CODE_HERE}));
    public IEnumerator Countdown (float seconds, Action onComplete) {
        yield return new WaitForSecondsRealtime (seconds);
        onComplete ();
    }
}