using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour {

    [Space (10)]
    [Header ("╔═══════════════[Follow Path]══════════════════════════════════════════════════════════════════════════════════════════")]
    [Space (10)]
    public bool Active;
    [Space (10)]
    public WaypointManager PathToFollow;
    public int CurrentWaypointID;
    [Space (10)]
    public float FollowSpeed;
    public float timer;
    public float FollowReachDistance = 1.0f;

    private Vector3 velocity;
    private float FollowDistance = 0f;

    void Update () {
        float position_x = Mathf.SmoothDamp (transform.position.x, PathToFollow.path_objs[CurrentWaypointID].position.x, ref velocity.x, timer);
        float position_y = Mathf.SmoothDamp (transform.position.y, PathToFollow.path_objs[CurrentWaypointID].position.y, ref velocity.y, timer);
        float position_z = Mathf.SmoothDamp (transform.position.z, PathToFollow.path_objs[CurrentWaypointID].position.z, ref velocity.z, timer);

        transform.position = new Vector3 (position_x, position_y, position_z);
        FollowDistance = Vector3.Distance (PathToFollow.path_objs[CurrentWaypointID].position, transform.position);

        if (FollowDistance <= FollowReachDistance) {
            CurrentWaypointID++;
        }
        if (CurrentWaypointID >= PathToFollow.path_objs.Count) {
            CurrentWaypointID = 0;
        }
    }
}