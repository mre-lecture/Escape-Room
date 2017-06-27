using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using Valve.VR.InteractionSystem;


public class WallCompassPuzzle : Photon.MonoBehaviour {


    public enum CompassDirection { North, East, South, West };
    public CompassDirection[] directionOrder;
    public GameObject unlockableFlap;
    public GameObject[] unlockableItems;

    private int progress = 0;


    private void HandHoverUpdate(Hand hand)
    {
        if (hand.GetStandardInteractionButtonUp())
        {
            applyDirection();
        }
    }


    void applyDirection()
    {
        CompassDirection direction = GetCurrentCompassDirection();
        

        if (directionOrder[progress] == direction)
        {
            progress++;
        }
        else
        {
            if (directionOrder[0] == direction)
            {
                progress = 1;
            }
            else
            {
                progress = 0;
            }
        }


        if (progress >= directionOrder.Length)
        {
            //puzzle solved -> open flap
			photonView.RPC("openFlap", PhotonTargets.All);
            addScriptsToUnlockedItems();

            //script isn`t needed anymore -> destroy
            Destroy(this);
        }
    }


    private CompassDirection GetCurrentCompassDirection()
    {
        CompassDirection direction;
        float rotation = this.gameObject.GetComponent<LinearMapping>().value;
        int section = (int)Math.Round(rotation / 0.25);


        switch (section)
        {
            case 0:
            case 4:
                direction = CompassDirection.North;
                break;
            case 1:
                direction = CompassDirection.East;
                break;
            case 2:
                direction = CompassDirection.South;
                break;
            case 3:
                direction = CompassDirection.West;
                break;
            default:
                Debug.LogError("Unknown direction. Rotation was: " + rotation + ". Section was: " + section);
                direction = CompassDirection.North;
                break;
        }

        return direction;
    }

	[PunRPC]
    private void openFlap()
    {		
		Destroy (unlockableFlap);
    }


    private void addScriptsToUnlockedItems()
    {
        foreach (GameObject unlockableItem in unlockableItems)
        {
            unlockableItem.AddComponent<Interactable>();
            unlockableItem.AddComponent<Throwable>();
            //manually instantiate both unity events because of some weird bug they are
            //sometimes not automatically added. That causes the throwable script not to work properly
            unlockableItem.GetComponent<Throwable>().onPickUp = new UnityEngine.Events.UnityEvent();
            unlockableItem.GetComponent<Throwable>().onDetachFromHand = new UnityEngine.Events.UnityEvent();
        }
    }

}
