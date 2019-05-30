using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {
    public bool Active;

    public float RotationSpeed;

    public bool _X;
    public bool _Y;
    public bool _Z;

    void Update () {
        if (_X) {
            transform.Rotate (RotationSpeed * Time.deltaTime, 0, 0);
        }
        if (_Y) {
            transform.Rotate (0, RotationSpeed * Time.deltaTime, 0);
        }
        if (_Z) {
            transform.Rotate (0, 0, RotationSpeed * Time.deltaTime);
        }
    }
}