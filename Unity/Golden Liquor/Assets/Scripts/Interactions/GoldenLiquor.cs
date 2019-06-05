using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoldenLiquor : MonoBehaviour {

    bool Locked = true;

    public GameObject LoadingController;

    public GameObject Panel;
    public GameObject TriggerText;

    public GameObject ChatboxCanvas;

    private Animator PanelAnimator;
    private Animator DoorAnimator;

    void Start () {
        PanelAnimator = Panel.GetComponent<Animator> ();
        DoorAnimator = GetComponent<Animator> ();
    }

    void Update () {

        if (PanelAnimator.GetBool ("Active")) {
            if (Input.GetKeyDown (KeyCode.Space)) {
                RefreshLocked ();
                if (!Locked) {
                    DoorAnimator.SetBool ("Open", true);
                    StartCoroutine (Coroutines.Instance.Countdown (Menu.Instance.ChatBox_Time, () => { LoadingController.GetComponent<LoadScene> ().LoadLevel ("GameOverWin"); }));
                } else {
                    ChatboxCanvas.GetComponent<ChatBox> ().TextContent = "YOU DO NOT HAVE THE MARKS";
                    ChatboxCanvas.GetComponent<ChatBox> ().Show ();
                    StartCoroutine (Coroutines.Instance.Countdown (Menu.Instance.ChatBox_Time, () => { ChatboxCanvas.GetComponent<ChatBox> ().Show (false); }));
                }
            }
        }
    }

    void RefreshLocked () {
        if ((Menu.Instance.Tattoo[0]) && (Menu.Instance.Tattoo[1]) && (Menu.Instance.Tattoo[2])) {
            Locked = false;
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