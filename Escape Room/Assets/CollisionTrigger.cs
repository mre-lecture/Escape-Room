using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Valve.VR.InteractionSystem{
	
	public class CollisionTrigger : MonoBehaviour {

		[SerializeField]
		private GameObject fittingObject;
		private Throwable throwable;

		void OnTriggerEnter(Collider collider){
			if (collider.gameObject == fittingObject) {
				throwable = collider.gameObject.GetComponent<Throwable> ();
				//TODO detach from hand first ! Also make sure it does not fall through the wall or so
				Destroy (throwable);
			} else {
				Debug.Log ("DOES NOT FIT");
			}
		}
	}

}
