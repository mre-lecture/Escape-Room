using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Valve.VR.InteractionSystem;

public class ShieldPuzzle : MonoBehaviour {

    public GameObject kingsShield;

    public GameObject[] solutionOrderShields;
    private GameObject[] executedOrder;
    
    private int progress = 0;


    void Start ()
    {
        executedOrder = new GameObject[solutionOrderShields.Length];
    }


    public void onShieldRotated(GameObject shield)
    {
		// disable rotation -> make min and max angle equal
		shield.GetComponent<CircularDrive>().maxAngle = shield.GetComponent<CircularDrive>().minAngle;

        executedOrder[progress] = shield;
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
        //manually instantiate both unity events because of some weird bug they are
        //sometimes not automatically added. That causes the throwable script not to work properly
        kingsShield.GetComponent<Throwable>().onPickUp = new UnityEngine.Events.UnityEvent();
        kingsShield.GetComponent<Throwable>().onDetachFromHand = new UnityEngine.Events.UnityEvent();
    }


    private void resetPuzzle()
    {
        foreach (GameObject shield in solutionOrderShields)
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
        }
    }
}
