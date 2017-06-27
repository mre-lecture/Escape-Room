using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class cageHandler : MonoBehaviour {

	[SerializeField]
	private int sticksToBeBurned;

	[SerializeField]
	private GameObject[] unlockableObjects; 

	//TODO test if this is synced properly in multiplayer
	public void fireEvent(){
		if (--sticksToBeBurned == 0) {
			foreach(GameObject go in unlockableObjects){
				go.GetComponent<Rigidbody> ().isKinematic = false;
				go.AddComponent<Throwable> ();
				go.GetComponent<Throwable>().onPickUp = new UnityEngine.Events.UnityEvent();
				go.GetComponent<Throwable>().onDetachFromHand = new UnityEngine.Events.UnityEvent();
			}
		}			
	}

}
