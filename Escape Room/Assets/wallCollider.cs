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
	private bool isXAxisCollisionInverted, isYAxisCollisionInverted, isZAxisCollisionInverted;

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
				isXAxisCollisionInverted = (collider.gameObject.transform.rotation.y == 180);
									
				while (collider.bounds.Intersects(gameObject.GetComponent<Renderer>().bounds)) {
					if(!isXAxisCollisionInverted)
						gameObject.transform.position = new Vector3 (gameObject.transform.position.x - 0.1f, gameObject.transform.position.y, gameObject.transform.position.z);
					else
						gameObject.transform.position = new Vector3 (gameObject.transform.position.x + 0.1f, gameObject.transform.position.y, gameObject.transform.position.z);
				}

				XAxisLimitDifference = playerHand.transform.position.x-(collider.bounds.max.x-(collider.bounds.max.x - collider.bounds.min.x));
				
			} else if (collider.gameObject.GetComponent<limitatingStructure> ().axis.ToString().Equals("Y")) {				
				isYAxisLimited = true;
				limitatingColliderYAxis = collider;
				isYAxisCollisionInverted = (collider.gameObject.transform.rotation.x == 0);

				while (collider.bounds.Intersects(gameObject.GetComponent<Renderer>().bounds)) {
					if(!isYAxisCollisionInverted)
						gameObject.transform.position = new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y - 0.1f, gameObject.transform.position.z);
					else
						gameObject.transform.position = new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y + 0.1f, gameObject.transform.position.z);
				}

				YAxisLimitDifference = playerHand.transform.position.y-(collider.bounds.max.y-(collider.bounds.max.y - collider.bounds.min.y));
				
			} else if(collider.gameObject.GetComponent<limitatingStructure> ().axis.ToString().Equals("Z")) {				
				isZAxisLimited = true;
				limitatingColliderZAxis = collider;
				isZAxisCollisionInverted = (collider.gameObject.transform.rotation.y == 90);

				while (collider.bounds.Intersects(gameObject.GetComponent<Renderer>().bounds)) {
					if(!isZAxisCollisionInverted)
						gameObject.transform.position = new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z - 0.1f);
					else
						gameObject.transform.position = new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + 0.1f);
				}

				ZAxisLimitDifference = playerHand.transform.position.z-(collider.bounds.max.z-(collider.bounds.max.z - collider.bounds.min.z));
			}

			Debug.Log ("isXAxisCollisionInverted: "+isXAxisCollisionInverted);
			Debug.Log ("isYAxisCollisionInverted: "+isYAxisCollisionInverted);
			Debug.Log ("isZAxisCollisionInverted: "+isZAxisCollisionInverted);
		}
	}

	void Update(){	
		//update internal used positions
		oldPositionX = newPositionX;
		oldPositionY = newPositionY;
		oldPositionZ = newPositionZ;

		//TODO: playerhand ist null wenn nichtr genutzt --> alles hier in if-abfrage darunter ziehen
		//-->oldPos/newPos dann redundant !!!
		newPositionX = playerHand.transform.position.x;
		newPositionY = playerHand.transform.position.y;
		newPositionZ = playerHand.transform.position.z;

		//set rotation of controller to this object
		gameObject.transform.rotation = playerHand.transform.rotation;
						
		//update limitations and check if they still have to be applied
		if (isXAxisLimited) {			
			XAxisLimitDifference += (newPositionX - oldPositionX);
		}
		if (isYAxisLimited) {
			YAxisLimitDifference += (newPositionY - oldPositionY);
		}
		if (isZAxisLimited) {			
			ZAxisLimitDifference += (newPositionZ - oldPositionZ);
		}

		if(playerHand!=null){
			Vector3 oldPos = gameObject.transform.position;
			Vector3 newPos = new Vector3 (playerHand.transform.position.x, playerHand.transform.position.y, playerHand.transform.position.z);

			//TODO: Faktor benutzen, um die Richtung der Wand zu bestimmen?

			if (isXAxisLimited && ((! isXAxisCollisionInverted && XAxisLimitDifference<0) || (isXAxisCollisionInverted && XAxisLimitDifference>0))) {
				Debug.Log ("! isXAxisCollisionInverted && XAxisLimitDifference<0 = "+(! isXAxisCollisionInverted && XAxisLimitDifference<0));
				Debug.Log ("isXAxisCollisionInverted && XAxisLimitDifference>0 = "+(isXAxisCollisionInverted && XAxisLimitDifference>0));
				newPos.x = oldPos.x;
			}
			if (isYAxisLimited && ((! isYAxisCollisionInverted && YAxisLimitDifference<0) || (isYAxisCollisionInverted && YAxisLimitDifference>0))) {				
				newPos.y = oldPos.y;
			}
			if (isZAxisLimited && ((! isZAxisCollisionInverted && ZAxisLimitDifference<0) || (isZAxisCollisionInverted && ZAxisLimitDifference>0))) {
				newPos.z = oldPos.z;
			}

			gameObject.transform.position = newPos;

			Debug.Log ("xAxisDifference: " + XAxisLimitDifference);
			//Debug.Log ("yAxisDifference: " + YAxisLimitDifference);
			//Debug.Log ("zAxisDifference: " + ZAxisLimitDifference);

		}
	}


}
