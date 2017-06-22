using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ignitable : MonoBehaviour {

	[SerializeField]
	private GameObject ignitionObject;

	[SerializeField]
	private GameObject ignitionObjectFlame;

	[SerializeField]
	private GameObject flame;

	[SerializeField]
	private GameObject cage;

	private bool alreadyActive;

	void OnTriggerEnter(Collider collider){	
		if (collider.gameObject == ignitionObject && !alreadyActive && ignitionObjectFlame.GetComponent<ParticleSystem>().isPlaying) {
			alreadyActive = true;
			GetComponent<Light> ().enabled = true;
			flame.GetComponent<ParticleSystem> ().Play ();
			Invoke ("destroyAfterBurnedDown", 20);
		}
	}

	private void destroyAfterBurnedDown(){
		Destroy (gameObject);
		flame.GetComponent<ParticleSystem> ().Stop ();
		GetComponent<Light> ().enabled = false;
		cage.GetComponent<cageHandler> ().fireEvent ();
	}
}
