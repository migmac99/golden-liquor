using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {
    public Transform target;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    [Space (30)]
    public bool CutsceneEnabled;
    [Space (10)]
    public Camera MainCamera;
    public WaypointManager PathToFollow;
    public int CurrentWaypointID;
    [Space (10)]
    public float FollowSpeed;
    public float timer;
    public float FollowReachDistance = 1.0f;

    private Vector3 velocity;
    private float ref_velocity;
    private float FollowDistance = 0f;

    void LateUpdate () {
        if ((!Menu.Instance.CutsceneHasPlayed) && (CutsceneEnabled)) {
            if (CurrentWaypointID >= PathToFollow.path_objs.Count) {
                CurrentWaypointID = 0;
                MainCamera.orthographicSize = 4;
                Menu.Instance.CutsceneHasPlayed = true;
            } else {
                float position_x = Mathf.SmoothDamp (transform.position.x, PathToFollow.path_objs[CurrentWaypointID].position.x, ref velocity.x, timer);
                float position_y = Mathf.SmoothDamp (transform.position.y, PathToFollow.path_objs[CurrentWaypointID].position.y, ref velocity.y, timer);
                float position_z = Mathf.SmoothDamp (transform.position.z, PathToFollow.path_objs[CurrentWaypointID].position.z, ref velocity.z, timer);

                if (CurrentWaypointID == PathToFollow.path_objs.Count - 1) {
                    MainCamera.orthographicSize = Mathf.SmoothDamp (MainCamera.orthographicSize, 2, ref ref_velocity, timer);
                }

                transform.position = new Vector3 (position_x, position_y, position_z);
                FollowDistance = Vector3.Distance (PathToFollow.path_objs[CurrentWaypointID].position, transform.position);

                if (FollowDistance <= FollowReachDistance) {
                    CurrentWaypointID++;
                }
            }
        } else {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp (transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }

    }
}