using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager> {

    [SerializeField] public Vector3 CurrentPlayerPosition;
    [SerializeField] public int DesiredIndexPosition;
    [SerializeField] public Vector3[] DesiredPlayerPosition;
    [Space (10)]
    [SerializeField] public bool UsePosition;

    protected virtual void Start () {
        UsePosition = false;
    }
}