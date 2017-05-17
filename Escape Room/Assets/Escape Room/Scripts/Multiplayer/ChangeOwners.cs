using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class ChangeOwners : Photon.MonoBehaviour {

    public Interactable interactableObject;
    

    // Use this for initialization
    void Start () {
        interactableObject.onAttachedToHand += interactableObject_onAttachedToHand;
        interactableObject.onDetachedFromHand += InteractableObject_onDetachedFromHand;

    }

    

    private void interactableObject_onAttachedToHand(Hand hand)
    {
        interactableObject.GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.player.ID);
        GetComponent<PhotonView>().RPC("SetRigidbodyToGrabbed", PhotonTargets.All);
        
    }

    private void InteractableObject_onDetachedFromHand(Hand hand)
    {
        GetComponent<PhotonView>().RPC("SetRigidbodyToDetached", PhotonTargets.All);
    }
    
}
