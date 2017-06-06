using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class artusSword : MonoBehaviour {

	[SerializeField]
	private int amountFiguresNeedes = 4;

	public void figureAdded(){
		if (--amountFiguresNeedes == 0) {
			//make sword throwable (it already is interactable aso.)
			gameObject.AddComponent<Throwable> ();
			//manually instantiate both unity events because of some weird bug they are
			//sometimes not automatically added. That causes the throwable script not to work properly
			gameObject.GetComponent<Throwable> ().onPickUp = new UnityEngine.Events.UnityEvent ();
			gameObject.GetComponent<Throwable> ().onDetachFromHand = new UnityEngine.Events.UnityEvent ();
		}
	}
}
