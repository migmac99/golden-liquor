using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager> {

    protected virtual void Start () {
        SceneManager.LoadScene ("Golden-Liquor");
    }
}