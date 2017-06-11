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

	private Hand playerHand;
	private Vector3 internalVelocity;
	private Vector3 colliderPosition;
	private Vector3 pos;

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

			internalVelocity = new Vector3 (newPositionX - oldPositionX, newPositionY - oldPositionY, newPositionZ - oldPositionZ);
			gameObject.transform.hasChanged = false;
		} 
	}

	private void OnAttachedToHand( Hand hand )
	{
		gameObject.transform.parent = null;
		onHand = true;		
		playerHand = hand;
	}

	private void OnDetachedFromHand( Hand hand )
	{
		onHand = false;	
		playerHand = null;
	}

	private void OnTriggerEnter(Collider collider)
	{
		if (onHand && collider.gameObject.tag.Equals("collidingTexture")) {
			resetCubeDueToCollision = true;
			collision = true;

			bool movingPositiveX = internalVelocity.x > 0;
			bool movingPositiveY = internalVelocity.y > 0;
			bool movingPositiveZ = internalVelocity.z > 0;

			float checkPosX = gameObject.GetComponent<Renderer> ().bounds.max.x;
			float checkPosY = gameObject.GetComponent<Renderer> ().bounds.max.y;
			float checkPosZ = gameObject.GetComponent<Renderer> ().bounds.max.z;

			if(!movingPositiveX)
				checkPosX = gameObject.GetComponent<Renderer> ().bounds.min.x;			
			if(!movingPositiveY)
				checkPosY = gameObject.GetComponent<Renderer> ().bounds.min.y;
			if(!movingPositiveZ)
				checkPosZ = gameObject.GetComponent<Renderer> ().bounds.min.z;

			pos = new Vector3 (checkPosX, checkPosY, checkPosZ);
			//TODO: Eine Kollision beschränkt eigentlich nur in eine Richtung die Bewegung --> nur in diese Prüfen
			//TODO: Das Objekt dann nicht auf die Extremwerte (Äußeren Werte setzen, da sonst der Mittelpunkt da hin kommt und sodurch das Objekt in der Wand steckt)
			Debug.Log("Koordinatenwerte in Berührungsrichtung: "+pos);
			while (collider.bounds.Intersects(gameObject.GetComponent<Renderer>().bounds)) {
				gameObject.transform.position = new Vector3 (gameObject.transform.position.x - internalVelocity.x, gameObject.transform.position.y - internalVelocity.y, gameObject.transform.position.z - internalVelocity.z);
				Debug.Log ("korrigierte Werte: " + pos);
			}

			//handleCollision ();
			Debug.Log ("Cube has to be set to " + pos + " to not touch the colliding object");
		}
		Debug.Log ("Internal Velocity on collision: " + internalVelocity);

	}

	private void OnTriggerExit(Collider collider){
		if (onHand && collider.gameObject.tag.Equals("collidingTexture")) {
			resetCubeDueToCollision = false;
			collision=false;
		}
	}

	private void handleCollision(){		
		Debug.Log ("RESET");
	
		gameObject.transform.position = pos;

		newPositionX = pos.x;
		newPositionY = pos.y;
		newPositionZ = pos.z;
	}

	void Update(){	

		if (resetCubeDueToCollision) {
			//gameObject.transform.parent = null;
			//gameObject.transform.position = new Vector3 (oldPositionX, oldPositionY, oldPositionZ);
			//gameObject.transform.eulerAngles = new Vector3 (oldRotationX, oldRotationY, oldRotationZ);
		} else if(playerHand!=null){
			gameObject.transform.position = new Vector3 (playerHand.transform.position.x, playerHand.transform.position.y, playerHand.transform.position.z);
		}

	}


}
