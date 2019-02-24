using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SwitchScene : MonoBehaviour {

    public GameObject LoadingController;
    [Space (10)]
    public string DesiredScene;
    [Space (10)]
    public GameObject Door_L;
    public GameObject Door_R;
    [Space (5)]
    public GameObject Panel;
    public GameObject TriggerText;

    private Animator PanelAnimator;
    private Animator DoorAnimator;

    void Start () {
        PanelAnimator = Panel.GetComponent<Animator> ();
        DoorAnimator = GetComponent<Animator> ();
    }

    void Update () {
        if (PanelAnimator.GetBool ("Active")) {
            if (Input.GetKeyDown (KeyCode.Space)) {
                DoorAnimator.SetBool ("Open", true);
                LoadingController.GetComponent<LoadScene> ().LoadLevel (DesiredScene);
            }
        }
    }

    // Reusable timer that will execute CODE_HERE after the timer is done --> used in fight timers and such
    // This is creating a CoRoutine which runs independently of the function it is called from
    // StartCoroutine (Countdown (3f, () => {CODE_HERE}));
    IEnumerator Countdown (float seconds, Action onComplete) {
        yield return new WaitForSecondsRealtime (seconds);
        onComplete ();
    }

    private void OnTriggerEnter (Collider other) {
        if (other.gameObject.layer == LayerMask.NameToLayer ("Player")) {
            Panel.SetActive (true);
            PanelAnimator.SetBool ("Active", true);
        }
    }

    private void OnTriggerExit (Collider other) {
        if (other.gameObject.layer == LayerMask.NameToLayer ("Player")) {
            PanelAnimator.SetBool ("Active", false);
            StartCoroutine (Countdown (1f, () => { Panel.SetActive (false); }));
        }
    }
}