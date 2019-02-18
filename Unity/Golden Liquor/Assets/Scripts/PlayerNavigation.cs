using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerNavigation : MonoBehaviour {

    public LayerMask whatCanBeClickedOn;

    private NavMeshAgent _navMeshAgent;

    void Start () {
        _navMeshAgent = GetComponent<NavMeshAgent> ();
    }

    void Update () {
        if (Input.GetMouseButtonDown (0)) {
            Ray myRay = Camera.main.ScreenPointToRay (Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast (myRay, out hitInfo, 100, whatCanBeClickedOn)) {
                _navMeshAgent.SetDestination (hitInfo.point);
            }
        }
    }
}