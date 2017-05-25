using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valve.VR.InteractionSystem{
	
	public class CollisionTrigger : MonoBehaviour {

		[SerializeField]
		private GameObject fittingObject;

		[SerializeField]
		private Vector3 finalPosition = new Vector3 (0, 0, 0);

		[SerializeField]
		private Vector3 finalRotation = new Vector3 (0, 0, 0);

		[SerializeField]
		private GameObject hiddenDoor;

		void OnTriggerEnter(Collider collider){
			if (collider.gameObject == fittingObject) {	
				//detach from hand
				collider.gameObject.transform.parent = null;
				//remove throwable script
				//TODO this prevents the hidden cube to become throwable !!!
				//-->Steam throwable script still seems to have locked the hand!
				Destroy (collider.gameObject.GetComponent<Throwable> ());
				//set final position of object
				collider.gameObject.transform.position = finalPosition;
				//set final rotation of object
				collider.gameObject.transform.eulerAngles = finalRotation;
				//fire events of corresponding hidden door that might by unlocked now
				hiddenDoor.GetComponent<HiddenDoor>().fireEvent();
			} 
		}
	}

}
