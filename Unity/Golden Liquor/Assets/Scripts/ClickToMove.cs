using UnityEngine;

public class ClickToMove : MonoBehaviour {

    public GameObject Player;

    void Update () {
        if (Input.GetMouseButtonDown (0)) {
            //Player.GetComponent<PlayerNavigation>().SetDestination();
        }
    }
}