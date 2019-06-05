using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager> {

    public bool StartInMenu;

    protected virtual void Start () {
        if (StartInMenu) {
            SceneManager.LoadScene ("StartMenu");
        } else {
            SceneManager.LoadScene ("Golden-Liquor");
        }
    }
}