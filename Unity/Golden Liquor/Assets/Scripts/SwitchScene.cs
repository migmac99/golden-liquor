using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour {

    public GameObject LoadingController;

    void Update () {
        if (Input.GetKeyDown (KeyCode.Space)) {
            LoadingController.GetComponent<LoadScene> ().LoadLevel ("Barber-Shop");
        }

        if (Input.GetKeyDown (KeyCode.LeftShift)) {
            LoadingController.GetComponent<LoadScene> ().LoadLevel ("Golden-Liquor");
        }

    }
}