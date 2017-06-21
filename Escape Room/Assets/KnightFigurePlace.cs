using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class KnightFigurePlace : MonoBehaviour {

	[SerializeField]
	private int KnightYRotation = 0;

	[SerializeField]
	private GameObject hand1,hand2,fallbackHand;

	[SerializeField]
	private artusSword swordScript;

	[SerializeField]
	private GameObject positionPlaceholder;

	private bool holdsFigure;

	void OnTriggerEnter(Collider collider){		
		if (collider.gameObject.tag == "Knight" && !holdsFigure) {	
			//disable this spot for other figures
			holdsFigure = true;
			//detach from hand
			collider.gameObject.transform.parent = null;
			//detach object from hand (depending on which hand it is in)
			hand1.GetComponent<Hand>().DetachObject(collider.gameObject,true);
			hand2.GetComponent<Hand>().DetachObject(collider.gameObject,true);
			fallbackHand.GetComponent<Hand>().DetachObject(collider.gameObject,true);
			//remove throwable script
			Destroy (collider.gameObject.GetComponent<Throwable>());
			Destroy (collider.gameObject.GetComponent<Interactable>());
			Destroy (collider.gameObject.GetComponent<Rigidbody>());
			//set knight position
			collider.gameObject.transform.position = positionPlaceholder.transform.position;
			//set knight rotation
			collider.gameObject.transform.eulerAngles = new Vector3(270,KnightYRotation,0);
			//update sword
			swordScript.figureAdded();
		}
	}
}
