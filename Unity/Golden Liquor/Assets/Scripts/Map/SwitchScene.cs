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
    [Space (10)]
    public int DesiredIndexPosition;
    public bool UseDesiredPosition;

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
                if (UseDesiredPosition) {
                    PlayerManager.Instance.UsePosition = true;
                    PlayerManager.Instance.DesiredIndexPosition = DesiredIndexPosition;
                }
            }
        }
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
            StartCoroutine (Coroutines.Instance.Countdown (1f, () => { Panel.SetActive (false); }));
        }
    }
}