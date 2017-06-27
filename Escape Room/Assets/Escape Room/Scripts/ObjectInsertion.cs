using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class ObjectInsertion : MonoBehaviour {

	public GameObject fittingObject;
	//public GameObject solveableObject;

	public float fittingObjectLocalXPosition = 0;
	public float fittingObjectLocalYPosition = 0;
	public float fittingObjectLocalZPosition = 0;

	public int fittingObjectXRotation = 0;
	public int fittingObjectYRotation = 0;
	public int fittingObjectZRotation = 0;


	void OnTriggerEnter(Collider collider)
	{
		if (collider.gameObject == fittingObject)
		{
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

			//add sword to hierarchy
			fittingObject.transform.SetParent(gameObject.transform);

			fittingObject.transform.localPosition = new Vector3 (fittingObjectLocalXPosition, fittingObjectLocalYPosition, fittingObjectLocalZPosition);
			fittingObject.transform.localRotation = Quaternion.Euler (fittingObjectXRotation, fittingObjectYRotation, fittingObjectZRotation);
		
			/*if (solveableObject != null) 
			{
				//solveableObject.GetComponent<RüdigerMystery> ().onObjectActivated(gameObject);
			}*/
		}
	}
}
