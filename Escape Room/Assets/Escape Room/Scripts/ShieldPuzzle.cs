using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Valve.VR.InteractionSystem;

public class ShieldPuzzle : Photon.MonoBehaviour {

    public GameObject kingsShield;

    public GameObject[] solutionOrderShields;
    private GameObject[] executedOrder;
	private float[] shieldMinAngles;
	private float[] shieldMaxAngles;
    
    private int progress = 0;


    void Start ()
    {
        executedOrder = new GameObject[solutionOrderShields.Length];
		shieldMinAngles = new float[solutionOrderShields.Length];
		shieldMaxAngles = new float[solutionOrderShields.Length];

		for (int i = 0; i < solutionOrderShields.Length; i++)
		{
			shieldMinAngles [i] = solutionOrderShields [i].GetComponent<CircularDrive> ().minAngle;
			shieldMaxAngles [i] = solutionOrderShields [i].GetComponent<CircularDrive> ().maxAngle;
		}
    }


    public void onShieldRotated(GameObject shield)
    {

		if (shield.GetComponent<CircularDrive>().minAngle < 0)
		{
			// disable rotation -> make min and max angle equal
			shield.GetComponent<CircularDrive>().maxAngle = shield.GetComponent<CircularDrive>().minAngle;
		}
		else
		{
			shield.GetComponent<CircularDrive>().minAngle = shield.GetComponent<CircularDrive>().maxAngle;
		}

		executedOrder[progress] = shield;
		photonView.RPC("onShieldRotatedRPC", PhotonTargets.All);

    }

	[PunRPC]
	private void onShieldRotatedRPC(){
		Debug.Log ("onShieldRotated");




		progress++;

		if (progress >= solutionOrderShields.Length)
		{
			if (isPuzzleSolved()) {
				unlockKingsShield();
			}
			else
			{
				resetPuzzle();
			}
		}
	}


    private bool isPuzzleSolved()
    {
        bool puzzleSolved = true;

        for (int i = 0; i < solutionOrderShields.Length; i++)
        {
            if (solutionOrderShields[i] != executedOrder[i])
            {
                puzzleSolved = false;
            }
        }

        return puzzleSolved;
    }


    private void unlockKingsShield()
    {   
        kingsShield.AddComponent<Interactable>();
        kingsShield.AddComponent<Throwable>();

		kingsShield.GetComponent<Throwable>().onPickUp = new UnityEngine.Events.UnityEvent();
		kingsShield.GetComponent<Throwable>().onDetachFromHand = new UnityEngine.Events.UnityEvent();

        kingsShield.AddComponent<SyncThrowableObjects>();
        photonView.ObservedComponents.Add(GetComponent <SyncThrowableObjects>());
        //manually instantiate both unity events because of some weird bug they are
        //sometimes not automatically added. That causes the throwable script not to work properly
    }


    private void resetPuzzle()
    {
        /*foreach (GameObject shield in solutionOrderShields)
        {
            progress = 0;
            
            //detach shield, if attached to a hand
            Hand[] hands = FindObjectsOfType<Hand>();
            foreach (Hand hand in hands)
            {
                hand.DetachObject(shield, true);
            }

			// reset angles
			shield.GetComponent<CircularDrive>().maxAngle = 0;
            shield.GetComponent<CircularDrive>().outAngle = 0;


            //rotate shields back
            shield.GetComponent<Animation>().Play();
        }*/

		for (int i = 0; i < solutionOrderShields.Length; i++) 
		{
			progress = 0;
			GameObject shield = solutionOrderShields[i];

			//detach shield, if attached to a hand
			Hand[] hands = FindObjectsOfType<Hand>();
			foreach (Hand hand in hands)
			{
				hand.DetachObject(shield, true);
			}

			if (shieldMinAngles[i] < 0) 
			{
				shield.GetComponent<CircularDrive>().maxAngle = shieldMaxAngles[i];
				shield.GetComponent<CircularDrive>().outAngle = shieldMaxAngles[i];
			}
			else
			{
				shield.GetComponent<CircularDrive>().minAngle = shieldMinAngles[i];
				shield.GetComponent<CircularDrive>().outAngle = shieldMinAngles[i];
			}


			//rotate shields back
			shield.GetComponent<Animation>().Play();
		}
			
    }
}
