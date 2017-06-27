using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class ButtonBehavior : Photon.MonoBehaviour
{
    public const float DISTANCE = 0.05f;
    public bool pressed = false;

    [SerializeField]
    private GameObject activeButton;

    // For Mouse testing
    private void OnMouseDown()
    {
        if (pressed)
        {
            activeButton.transform.position += new Vector3( DISTANCE, 0, 0);
            pressed = false;
            return;
        }
        else
        {
            activeButton.transform.position += new Vector3(-DISTANCE, 0, 0);
            pressed = true;
        }

    }

    private void HandHoverUpdate(Hand hand)
    {
        if (hand.GetStandardInteractionButtonDown())
        {
            pressButton();
        }
    }

    // For Trigger Enter - Check Collider
    private void pressButton()
    {
        
		photonView.RPC("ButtonPressed", PhotonTargets.All);
    }

	[PunRPC]
	void ButtonPressed(){
		if (pressed)
		{
			activeButton.transform.position += new Vector3(DISTANCE, 0, 0);
			pressed = false;
		}
		else
		{
			activeButton.transform.position += new Vector3(-DISTANCE, 0, 0);
			pressed = true;
		}
	}
}