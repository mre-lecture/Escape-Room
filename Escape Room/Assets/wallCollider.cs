using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using AssemblyCSharp;

public class wallCollider : MonoBehaviour {	

	private Hand playerHand;

	private Vector3 oldObjectPos;
	private Vector3 newObjectPos;
	private Vector3 oldHandPos;
	private Vector3 newHandPos;

	private WallCollidingAxis xAxisCollider, yAxisCollider, zAxisCollider;

	private void Awake(){
		oldObjectPos = new Vector3 ();
		newObjectPos = new Vector3 ();
		oldHandPos = new Vector3 ();
		newHandPos = new Vector3 ();
		xAxisCollider = new WallCollidingAxis (WallCollidingAxis.Axis.X);
		yAxisCollider = new WallCollidingAxis (WallCollidingAxis.Axis.Y);
		zAxisCollider = new WallCollidingAxis (WallCollidingAxis.Axis.Z);
	}

	private void OnAttachedToHand( Hand hand )
	{
		gameObject.transform.parent = null;
		playerHand = hand;
	}

	private void OnDetachedFromHand( Hand hand )
	{		
		playerHand = null;
	}

	private void OnTriggerEnter(Collider collider)
	{		
		if (playerHand!=null && collider.gameObject.GetComponent<limitatingStructure>() != null) {
			if (collider.gameObject.GetComponent<limitatingStructure> ().axis.ToString().Equals("X")) {	
				handleCollision (xAxisCollider, collider);				
			} else if (collider.gameObject.GetComponent<limitatingStructure> ().axis.ToString().Equals("Y")) {				
				handleCollision (yAxisCollider, collider);					
			} else if(collider.gameObject.GetComponent<limitatingStructure> ().axis.ToString().Equals("Z")) {				
				handleCollision (zAxisCollider, collider);	
			}
		}
	}

	void Update(){	
		if(playerHand!=null){			
			//update internal used positions
			oldObjectPos = gameObject.transform.position;
			newObjectPos = new Vector3 (playerHand.transform.position.x, playerHand.transform.position.y, playerHand.transform.position.z);
			oldHandPos = newHandPos;
			newHandPos = new Vector3 (playerHand.transform.position.x, playerHand.transform.position.y, playerHand.transform.position.z);

			//set rotation of controller to this object
			gameObject.transform.rotation = playerHand.transform.rotation;

			//check if limitating wall still exists (in some puzzles colliding objects are burned and destroyed)
			xAxisCollider.setLimited(xAxisCollider.isLimited() && xAxisCollider.getLimitatingCollider() != null);
			yAxisCollider.setLimited(yAxisCollider.isLimited() && yAxisCollider.getLimitatingCollider() != null);
			zAxisCollider.setLimited(zAxisCollider.isLimited() && zAxisCollider.getLimitatingCollider() != null);

			//update collider distance
			xAxisCollider.setColliderDistance(xAxisCollider.getColliderDistance()+(newHandPos.x - oldHandPos.x));
			yAxisCollider.setColliderDistance(yAxisCollider.getColliderDistance()+(newHandPos.y - oldHandPos.y));
			zAxisCollider.setColliderDistance(zAxisCollider.getColliderDistance()+(newHandPos.z - oldHandPos.z));

			//do NOT / UNDO move in certain direction if it is limited atm
			if (xAxisCollider.isCurrentlyLimited()) {				
				newObjectPos.x = oldObjectPos.x;
			}
			if (yAxisCollider.isCurrentlyLimited()) {				
				newObjectPos.y = oldObjectPos.y;
			}
			if (zAxisCollider.isCurrentlyLimited()) {
				newObjectPos.z = oldObjectPos.z;
			}

			//update object position
			gameObject.transform.position = newObjectPos;
		}
	}

	private void handleCollision(WallCollidingAxis wallCollidingAxis, Collider collider){
		wallCollidingAxis.setLimited (true);
		wallCollidingAxis.setLimitatingCollider (collider);
		wallCollidingAxis.moveToClosestPositionToCollider (gameObject);
		wallCollidingAxis.setColliderDistance (calculateInitialColliderDistance(wallCollidingAxis, collider));
	}

	private float calculateInitialColliderDistance(WallCollidingAxis wallCollidingAxis, Collider collider){
		if (wallCollidingAxis.getAxis () == WallCollidingAxis.Axis.X)
			return playerHand.transform.position.x - (collider.bounds.max.x - (collider.bounds.max.x - collider.bounds.min.x));
		else if (wallCollidingAxis.getAxis () == WallCollidingAxis.Axis.Y) 
			return playerHand.transform.position.y - (collider.bounds.max.y - (collider.bounds.max.y - collider.bounds.min.y));
		else 
			return playerHand.transform.position.z-(collider.bounds.max.z-(collider.bounds.max.z - collider.bounds.min.z));
	}
}