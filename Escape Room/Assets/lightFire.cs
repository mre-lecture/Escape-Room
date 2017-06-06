using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class lightFire : MonoBehaviour {

	[SerializeField]
	private GameObject torchHead;

	[SerializeField]
	private GameObject flame;

	[SerializeField]
	private GameObject light;

	void OnTriggerEnter(Collider collider){		
		if (collider.gameObject == torchHead) {		
			light.GetComponent<Light> ().enabled = true;
			flame.GetComponent<ParticleSystem> ().Play ();
		}
	}
}
