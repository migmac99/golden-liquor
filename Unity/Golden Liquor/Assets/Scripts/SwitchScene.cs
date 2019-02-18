using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour {
    void Update () {
        if (Input.GetKeyDown (KeyCode.Space)) {
            SceneManager.LoadScene ("Barber-Shop");
        }

        if (Input.GetKeyDown (KeyCode.LeftShift)) {
            SceneManager.LoadScene ("Golden-Liquor");
        }

    }
}