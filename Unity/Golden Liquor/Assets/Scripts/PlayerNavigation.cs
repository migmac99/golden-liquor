using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerNavigation : MonoBehaviour {

    public LayerMask WhatCanBeClickedOn;
    public LayerMask BuildingTag;

    [Range (0, 1f)] public float ObjAlpha;

    public GameObject TransparentObject;

    private NavMeshAgent _navMeshAgent;

    void Start () {
        _navMeshAgent = GetComponent<NavMeshAgent> ();
    }

    void Update () {

        Ray MyRay = Camera.main.ScreenPointToRay (Input.mousePosition);
        RaycastHit HitInfo;

        if (Input.GetMouseButtonDown (0)) { //if mouse clicked then go to mouse position
            if (Physics.Raycast (MyRay, out HitInfo, 1000, WhatCanBeClickedOn)) {
                _navMeshAgent.SetDestination (HitInfo.point);
            }
        }

        if (Physics.Raycast (MyRay, out HitInfo, 1000, BuildingTag)) { //if mouse on top of building make transparent
            Renderer[] renderers = HitInfo.transform.gameObject.GetComponentsInChildren<Renderer> ();
            foreach (var r in renderers) {
                if (r.material.color.a == 1) {
                    r.material.color = new Color (1, 1, 1, ObjAlpha);
                    TransparentObject = HitInfo.transform.gameObject;
                }
            }
        }

        if (TransparentObject != null) { //avoid null exception error
            if (Physics.Raycast (MyRay, out HitInfo, 1000)) { //If mouse not on building make transparency 1
                if (HitInfo.transform.name != TransparentObject.name) {
                    Renderer[] renderers = TransparentObject.gameObject.GetComponentsInChildren<Renderer> ();
                    foreach (var r in renderers) {
                        if (r.material.color.a != 1) {
                            r.material.color = new Color (1, 1, 1, 1);
                            TransparentObject = null;
                        }
                    }
                }
            }
        }

    }
}