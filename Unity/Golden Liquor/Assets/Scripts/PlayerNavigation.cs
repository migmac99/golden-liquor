using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerNavigation : MonoBehaviour {

    public LayerMask WhatCanBeClickedOn;
    public LayerMask BuildingTag;

    [Range (0, 1f)] public float ObjAlpha;

    public GameObject CameraToMouse_Object;
    public GameObject PlayerToCamera_Object;

    private NavMeshAgent _navMeshAgent;

    void Start () {
        _navMeshAgent = GetComponent<NavMeshAgent> ();
    }

    void Update () {
        #region CameraToMouseRay Camera sends raycast to mouse and checks for buildings in between + Player moves where clicked

        Ray CameraToMouseRay = Camera.main.ScreenPointToRay (Input.mousePosition);
        RaycastHit CameraToMouseHitInfo;

        if (Input.GetMouseButtonDown (0)) { //if mouse clicked then go to mouse position
            if (Physics.Raycast (CameraToMouseRay, out CameraToMouseHitInfo, 1000, WhatCanBeClickedOn)) {
                _navMeshAgent.SetDestination (CameraToMouseHitInfo.point);
            }
        }

        if (Physics.Raycast (CameraToMouseRay, out CameraToMouseHitInfo, 1000, BuildingTag)) { //if mouse on top of building make transparent
            Renderer[] renderers = CameraToMouseHitInfo.transform.gameObject.GetComponentsInChildren<Renderer> ();
            foreach (var r in renderers) {
                if (r.material.color.a == 1) {
                    r.material.color = new Color (1, 1, 1, ObjAlpha);
                    CameraToMouse_Object = CameraToMouseHitInfo.transform.gameObject;
                }
            }
        }

        if (CameraToMouse_Object != null) { //avoid null exception error
            if (Physics.Raycast (CameraToMouseRay, out CameraToMouseHitInfo, 1000)) { //If mouse not on building make transparency 1
                if (CameraToMouseHitInfo.transform.name != CameraToMouse_Object.name) {
                    Renderer[] renderers = CameraToMouse_Object.gameObject.GetComponentsInChildren<Renderer> ();
                    foreach (var r in renderers) {
                        if (r.material.color.a != 1) {
                            r.material.color = new Color (1, 1, 1, 1);
                            CameraToMouse_Object = null;
                        }
                    }
                }
            }
        }
        Debug.DrawRay (CameraToMouseRay.origin, CameraToMouseRay.direction * 1000, Color.red);
        #endregion
        #region PlayerToCameraRay Player sends raycast to camera and checks for buildings in between

        Ray PlayerToCameraRay = Camera.main.ScreenPointToRay (transform.position);
        RaycastHit PlayerToCameraHitInfo;

        if (Physics.Raycast (PlayerToCameraRay.origin, transform.position - PlayerToCameraRay.origin, out PlayerToCameraHitInfo, 1000, BuildingTag)) {
            Renderer[] renderers = PlayerToCameraHitInfo.transform.gameObject.GetComponentsInChildren<Renderer> ();
            foreach (var r in renderers) {
                if (r.material.color.a == 1) {
                    r.material.color = new Color (1, 1, 1, ObjAlpha);
                    PlayerToCamera_Object = PlayerToCameraHitInfo.transform.gameObject;
                }
            }
        }

        if (PlayerToCamera_Object != null) { //avoid null exception error
            if (Physics.Raycast (PlayerToCameraRay.origin, transform.position - PlayerToCameraRay.origin, out CameraToMouseHitInfo, 1000)) { //If mouse not on building make transparency 1
                if (CameraToMouseHitInfo.transform.name != PlayerToCamera_Object.name) {
                    Renderer[] renderers = PlayerToCamera_Object.gameObject.GetComponentsInChildren<Renderer> ();
                    foreach (var r in renderers) {
                        if (r.material.color.a != 1) {
                            r.material.color = new Color (1, 1, 1, 1);
                            PlayerToCamera_Object = null;
                        }
                    }
                }
            }
        }
        Debug.DrawRay (PlayerToCameraRay.origin, transform.position - PlayerToCameraRay.origin, Color.red);
        #endregion
    }
}