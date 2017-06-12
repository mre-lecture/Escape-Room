using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class wallCollider : MonoBehaviour {
	
	private bool onHand;
	private Hand playerHand;
	private bool isXAxisLimited, isYAxisLimited, isZAxisLimited;
	private Collider limitatingColliderXAxis, limitatingColliderYAxis, limitatingColliderZAxis;
	private float XAxisLimitDifference, YAxisLimitDifference, ZAxisLimitDifference;
	private float oldPositionX, oldPositionY, oldPositionZ, newPositionX, newPositionY, newPositionZ;

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
		if (onHand && collider.gameObject.GetComponent<limitatingStructure>() != null) {			

			//update limitation and set position of object using the new limitations 
			//TODO: in schleife nur die neue Position berechnen und einmal am Ende setzen --> dann muss nur eine Positionsänderung synchronisiert werden --> vermutlich deutlich weniger traffic !!!
			if (collider.gameObject.GetComponent<limitatingStructure> ().axis.ToString().Equals("X")) {				
				isXAxisLimited = true;
				limitatingColliderXAxis = collider;
				while (collider.bounds.Intersects(gameObject.GetComponent<Renderer>().bounds)) {
					gameObject.transform.position = new Vector3 (gameObject.transform.position.x - 0.1f, gameObject.transform.position.y, gameObject.transform.position.z);
				}
				XAxisLimitDifference = (collider.bounds.max.x-(collider.bounds.max.x - collider.bounds.min.x))-playerHand.transform.position.x;
			} else if (collider.gameObject.GetComponent<limitatingStructure> ().axis.ToString().Equals("Y")) {				
				isYAxisLimited = true;
				limitatingColliderYAxis = collider;
				while (collider.bounds.Intersects(gameObject.GetComponent<Renderer>().bounds)) {
					gameObject.transform.position = new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y - 0.1f, gameObject.transform.position.z);
				}
				YAxisLimitDifference = (collider.bounds.max.y-(collider.bounds.max.y - collider.bounds.min.y))-playerHand.transform.position.y;
			} else if(collider.gameObject.GetComponent<limitatingStructure> ().axis.ToString().Equals("Z")) {				
				isZAxisLimited = true;
				limitatingColliderZAxis = collider;
				while (collider.bounds.Intersects(gameObject.GetComponent<Renderer>().bounds)) {
					gameObject.transform.position = new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z - 0.1f);
				}
				ZAxisLimitDifference = (collider.bounds.max.z-(collider.bounds.max.z - collider.bounds.min.z))-playerHand.transform.position.z;
			}
		}
	}

	void Update(){	
		//update internal used positions
		oldPositionX = newPositionX;
		oldPositionY = newPositionY;
		oldPositionZ = newPositionZ;

		newPositionX = playerHand.transform.position.x;
		newPositionY = playerHand.transform.position.y;
		newPositionZ = playerHand.transform.position.z;
		
		//update limitations and check if they still have to be applied
		if (isXAxisLimited) {
			XAxisLimitDifference += oldPositionX - newPositionX;
		}
		if (isYAxisLimited) {
			YAxisLimitDifference += oldPositionY - newPositionY;
		}
		if (isZAxisLimited) {
			ZAxisLimitDifference += oldPositionZ - newPositionZ;
		}

		if(playerHand!=null){
			Vector3 oldPos = gameObject.transform.position;
			Vector3 newPos = new Vector3 (playerHand.transform.position.x, playerHand.transform.position.y, playerHand.transform.position.z);

			if (isXAxisLimited && XAxisLimitDifference<0) {
				newPos.x = oldPos.x;
			}
			if (isYAxisLimited && YAxisLimitDifference<0) {
				newPos.y = oldPos.y;
			}
			if (isZAxisLimited && ZAxisLimitDifference<0) {
				newPos.z = oldPos.z;
			}
			gameObject.transform.position = newPos;
		}
	}


}
