using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleRangeScript : MonoBehaviour {
    void OnTriggerEnter (Collider other) {
        if (other.CompareTag ("Player")) {
            transform.parent.gameObject.transform.parent.GetComponent<Collectible> ().TouchingPlayer (true);
        }
    }

    void OnTriggerExit (Collider other) {
        if (other.CompareTag ("Player")) {
            transform.parent.gameObject.transform.parent.GetComponent<Collectible> ().TouchingPlayer (false);
        }
    }
}