using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerNavigation : MonoBehaviour {

    public LayerMask WhatCanBeClickedOn;
    public LayerMask BuildingTag;

    [Range (0, 1f)] public float ObjAlpha;

    private NavMeshAgent _navMeshAgent;

    void Start () {
        _navMeshAgent = GetComponent<NavMeshAgent> ();
    }

    // Reusable timer that will execute CODE_HERE after the timer is done --> used in fight timers and such
    // This is creating a CoRoutine which runs independently of the function it is called from
    // StartCoroutine (Countdown (3f, () => {CODE_HERE}));
    IEnumerator Countdown (float seconds, Action onComplete) {
        yield return new WaitForSecondsRealtime (seconds);
        onComplete ();
    }

    void Update () {

        Ray MyRay = Camera.main.ScreenPointToRay (Input.mousePosition);
        RaycastHit HitInfo;

        if (Input.GetMouseButtonDown (0)) {
            if (Physics.Raycast (MyRay, out HitInfo, 1000, WhatCanBeClickedOn)) {
                _navMeshAgent.SetDestination (HitInfo.point);
            }
        }

        if (Physics.Raycast (MyRay, out HitInfo, 1000, BuildingTag)) {
            Renderer[] renderers = HitInfo.transform.gameObject.GetComponentsInChildren<Renderer> ();
            foreach (var r in renderers) {
                if (r.material.color.a == 1) {
                    r.material.color = new Color (1, 1, 1, ObjAlpha);
                    Debug.Log ("SSSS");
                } else {
                    //StartCoroutine (Countdown (0f, () => { r.material.color = new Color (1, 1, 1, 1f); }));
                }
                Debug.Log ("AAAA");
            }
            Debug.Log ("ZZZZ");
        }
    }
}