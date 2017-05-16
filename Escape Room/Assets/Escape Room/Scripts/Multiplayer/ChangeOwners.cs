using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeOwners : Photon.MonoBehaviour {

    public Valve.VR.InteractionSystem.Interactable interactableObject;

    // Use this for initialization
    void Start () {
        interactableObject.onAttachedToHand += Toss_onAttachedToHand;
	}

    private void Toss_onAttachedToHand(Valve.VR.InteractionSystem.Hand hand)
    {
        interactableObject.GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.player.ID);
    }
}
