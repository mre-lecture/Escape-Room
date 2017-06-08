using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class wallCollider : MonoBehaviour {

	[SerializeField]
	private GameObject hand1,hand2,fallbackHand;

	[SerializeField]
	private Rigidbody rigidbody;

	private bool onHand,resetCubeDueToCollision;
	private bool collision;

	private float oldPositionX, oldPositionY, oldPositionZ, newPositionX, newPositionY, newPositionZ;
	private float oldRotationX, oldRotationY, oldRotationZ, newRotationX, newRotationY, newRotationZ;

	private Hand handOnObject;

	private void HandAttachedUpdate( Hand hand )
	{		
		if (gameObject.transform.hasChanged) {			
			oldPositionX = newPositionX;
			oldPositionY = newPositionY;
			oldPositionZ = newPositionZ;
			oldRotationX = newRotationX;
			oldRotationY = newRotationY;
			oldRotationZ = newRotationZ;

			newPositionX = gameObject.transform.position.x;
			newPositionY = gameObject.transform.position.y;
			newPositionZ = gameObject.transform.position.z;
			newRotationX = gameObject.transform.eulerAngles.x;
			newRotationY = gameObject.transform.eulerAngles.y;
			newRotationZ = gameObject.transform.eulerAngles.z;

			gameObject.transform.hasChanged = false;
		} 
	}

	private void OnAttachedToHand( Hand hand )
	{
		gameObject.transform.parent = null;
		onHand = true;		
		handOnObject = hand;
	}

	private void OnDetachedFromHand( Hand hand )
	{
		onHand = false;	
		handOnObject = null;
	}

	private void OnTriggerEnter(Collider collider)
	{
		if (onHand && collider.gameObject.tag.Equals("collidingTexture")) {
			resetCubeDueToCollision = true;
			//GetComponent<Throwable> ().hack = true;
			//gameObject.transform.parent = null;
			collision = true;
		}
	}

	private void OnTriggerExit(Collider collider){
		if (onHand && collider.gameObject.tag.Equals("collidingTexture")) {
			resetCubeDueToCollision = false;
			//GetComponent<Throwable> ().hack = false;
			collision=false;
		}
	}

	private IEnumerator handleCollision(){
		yield return new WaitForEndOfFrame();
		Debug.Log ("RESET");


		gameObject.transform.position = new Vector3 (oldPositionX, oldPositionY, oldPositionZ);
		gameObject.transform.eulerAngles = new Vector3 (oldRotationX, oldRotationY, oldRotationZ);

		newPositionX = oldPositionX;
		newPositionY = oldPositionY;
		newPositionZ = oldPositionZ;
		newRotationX = oldRotationX;
		newRotationY = oldRotationY;
		newRotationZ = oldRotationZ;

		/*
		//detach from hand
		gameObject.transform.parent = null;
		//detach object from hand (depending on which hand it is in)
		hand1.GetComponent<Hand>().DetachObject(gameObject,true);
		hand2.GetComponent<Hand>().DetachObject(gameObject,true);
		fallbackHand.GetComponent<Hand>().DetachObject(gameObject,true);

		GetComponent<Throwable> ().onPickUp = new UnityEngine.Events.UnityEvent ();
		GetComponent<Throwable> ().onDetachFromHand = new UnityEngine.Events.UnityEvent ();
		*/
	}

	void Update(){
		//Debug.Log(gameObject.)
		//TODO: Die Funktion HandAttachedUpdate in Throwable konkuriert mit dieser Funktion, da sie die akutelle Position immer auf die Hand setzt.
		//TODO: ne das alleine wars nicht (hab zwischenzeitlich den bool hack eingeführt)...vllt weils ein child von der hand ist?
		//TODO: besser, aber auch noch nicht die ultimative Lösung
		if (resetCubeDueToCollision) {
			//gameObject.transform.parent = null;
			gameObject.transform.position = new Vector3 (oldPositionX, oldPositionY, oldPositionZ);
			gameObject.transform.eulerAngles = new Vector3 (oldRotationX, oldRotationY, oldRotationZ);
		} else if(handOnObject!=null){
			gameObject.transform.position = new Vector3 (handOnObject.transform.position.x, handOnObject.transform.position.y, handOnObject.transform.position.z);
		}
	}


}
