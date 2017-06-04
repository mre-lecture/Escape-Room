using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class lightFire : MonoBehaviour {

	[SerializeField]
	private GameObject torchHead;

	[SerializeField]
	private GameObject fireSource;

	void OnTriggerEnter(Collider collider){		
		if (collider.gameObject == torchHead) {				
			fireSource.GetComponent<FireSource> ().isBurning = true;
			fireSource.GetComponent<FireSource> ().burnTime = 100;
		}
	}
}
