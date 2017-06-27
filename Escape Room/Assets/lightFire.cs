using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class lightFire : Photon.MonoBehaviour {

	[SerializeField]
	private GameObject torchHead;

	[SerializeField]
	private GameObject flame;

	[SerializeField]
	private GameObject light;

	void OnTriggerEnter(Collider collider){		
		if (collider.gameObject == torchHead) {		
			syncTorch ();
		}
	}

	[PunRPC]
	public void syncTorch()
	{		
		photonView.RPC("lightUpTorch", PhotonTargets.All);
	}

	//------------------------------------------------------------------------

	[PunRPC]
	public void lightUpTorch()
	{
		light.GetComponent<Light> ().enabled = true;
		flame.GetComponent<ParticleSystem> ().Play ();
	}
}
