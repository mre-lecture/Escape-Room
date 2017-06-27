using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valve.VR.InteractionSystem{

	public class torchRequirements : MonoBehaviour {

		[SerializeField]
		private GameObject[] requiredTorches;

		[SerializeField]
		private GameObject light;

		[SerializeField]
		private GameObject flame;

		private bool unlocked;

		void Update(){
			if (!unlocked) {				
				unlocked = true;
				foreach (GameObject go in requiredTorches) {					
					if (go.transform.localRotation.z < .17f)
						unlocked = false;					
				}
                if (unlocked)
                {
                    light.GetComponent<Light>().enabled = true;
                    flame.GetComponent<ParticleSystem>().Play();
                    
                    //disable rotation of correct used torches
                    foreach (GameObject go in requiredTorches)
                    {                        
						go.GetComponent<CircularDrive> ().outAngle = 20;
						go.GetComponent<CircularDrive> ().minAngle = 20;
                        go.transform.eulerAngles = new Vector3(0, 180, 20);
                    }
                }
			}
		}
	}

}
