using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleRangeScript : MonoBehaviour {
    public bool NPC;

    void OnTriggerEnter (Collider other) {
        if (other.CompareTag ("Player")) {
            if (!NPC) {
                transform.parent.gameObject.transform.parent.GetComponent<Collectible> ().TouchingPlayer (true);
            } else {
                transform.parent.GetComponent<NPC> ().TouchingPlayer (true);
            }
        }
    }

    void OnTriggerExit (Collider other) {
        if (other.CompareTag ("Player")) {
            if (!NPC) {
                transform.parent.gameObject.transform.parent.GetComponent<Collectible> ().TouchingPlayer (false);
            } else {
                transform.parent.GetComponent<NPC> ().TouchingPlayer (false);
            }
        }
    }
}