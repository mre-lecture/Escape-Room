using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CriticalGameObject : MonoBehaviour {

	private Vector3 respawnPosition, respawnRotation;

	void Awake(){
		respawnPosition = gameObject.transform.position;
		respawnRotation = gameObject.transform.eulerAngles;
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "resetGround") {
			gameObject.transform.position = respawnPosition;
			gameObject.transform.eulerAngles = respawnRotation;
		}
	}
}
