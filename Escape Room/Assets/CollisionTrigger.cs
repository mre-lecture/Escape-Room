using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

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
                //detach object from hand (depending on which hand it is in)

			    if (GameObject.Find("Hand1") != null)
			    {
			        GameObject.Find("Hand1").GetComponent<Hand>().DetachObject(collider.gameObject, true);
                }

			    if (GameObject.Find("Hand2") != null)
			    {
			        GameObject.Find("Hand2").GetComponent<Hand>().DetachObject(collider.gameObject, true);
			    }
                
			    if (GameObject.Find("FallbackHand") != null)
			    {
			        GameObject.Find("FallbackHand").GetComponent<Hand>().DetachObject(collider.gameObject, true);
			    }
				//remove throwable script
				Destroy (collider.gameObject.GetComponent<Throwable>());

                Destroy(collider.gameObject.GetComponent<Rigidbody>());
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
