using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ignitable : Photon.MonoBehaviour {

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
			syncStick ();
		}
	}

	private void destroyAfterBurnedDown(){
		Destroy (gameObject);
		flame.GetComponent<ParticleSystem> ().Stop ();
		GetComponent<Light> ().enabled = false;
		cage.GetComponent<cageHandler> ().fireEvent ();
	}

	public void syncStick()
	{		
		photonView.RPC("lightUpStick", PhotonTargets.All);
	}

	//------------------------------------------------------------------------

	[PunRPC]
	public void lightUpStick()
	{
		alreadyActive = true;
		GetComponent<Light> ().enabled = true;
		flame.GetComponent<ParticleSystem> ().Play ();
		Invoke ("destroyAfterBurnedDown", 20);
	}
}
