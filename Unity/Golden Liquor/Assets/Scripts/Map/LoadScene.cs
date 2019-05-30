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

        Menu.Instance.UpdateCameraTarget ();
    }

    public void LoadLevel (string sceneName) {
        RotatingSquare.SetActive (true);
        FadeAnimator.SetBool ("Loading", true);
        StartCoroutine (Coroutines.Instance.Countdown (1.5f, () => { StartCoroutine (Coroutines.Instance.LoadAsynchronously (sceneName)); }));
    }
}