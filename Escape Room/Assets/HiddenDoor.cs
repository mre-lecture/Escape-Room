using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valve.VR.InteractionSystem{
	
	public class HiddenDoor : MonoBehaviour {

		[SerializeField]
		private GameObject hiddenContent;

		public int eventsRequiredToOpenDoor = 1;
		private int eventsFired = 0;

		public void fireEvent(){
			if (++eventsFired == eventsRequiredToOpenDoor) {
				transform.Rotate(Vector3.right * 90);
				hiddenContent.AddComponent<Throwable>();
				//hiddenContent.GetComponent<Throwable> ().attachmentFlags = hiddenContent.GetComponent<Throwable> ().attachmentFlags | Hand.AttachmentFlags.DetachOthers;
				hiddenContent.GetComponent<Throwable> ().onPickUp = new UnityEngine.Events.UnityEvent ();
				hiddenContent.GetComponent<Throwable> ().onDetachFromHand = new UnityEngine.Events.UnityEvent ();
			}
		}
	}

}
