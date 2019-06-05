using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager> {

    public string StartScene;

    protected virtual void Start () {
        SceneManager.LoadScene (StartScene);
    }
}