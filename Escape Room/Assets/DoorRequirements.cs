using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valve.VR.InteractionSystem{

	public class DoorRequirements : MonoBehaviour {

		[SerializeField]
		private GameObject[] requiredLights;

		private bool unlocked;
		
		// Update is called once per frame
		void Update () {
			if (!unlocked) {
				unlocked = true;
				foreach (GameObject go in requiredLights) {
					if (!go.GetComponent<Light> ().enabled)
						unlocked = false;
				}
				if (unlocked) {
					GetComponent<CircularDrive> ().maxAngle = 0;
				}
			}
		}
	}

}
