using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {

    [Space (10)]
    [Header ("╔═══════════════[Basic Rotation]══════════════════════════════════════════════════════════════════════════════════════════")]
    [Space (10)]
    public bool Active;
    [Space (10)]
    public float RotationSpeed;
    [Space (10)]
    public bool _X;
    public bool _Y;
    public bool _Z;

    void Update () {
        #region Rotation
        if (_X) {
            transform.Rotate (RotationSpeed * Time.deltaTime, 0, 0);
        }
        if (_Y) {
            transform.Rotate (0, RotationSpeed * Time.deltaTime, 0);
        }
        if (_Z) {
            transform.Rotate (0, 0, RotationSpeed * Time.deltaTime);
        }
        #endregion
    }
}