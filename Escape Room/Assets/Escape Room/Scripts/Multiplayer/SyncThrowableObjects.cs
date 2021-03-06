﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class SyncThrowableObjects : SyncInteractableObjects
{
    private Interactable interactableScript;

    void Awake()
    {
        interactableScript = GetComponent<Interactable>();
        interactableScript.onAttachedToHand += InteractableScript_onAttachedToHand;
        interactableScript.onDetachedFromHand += InteractableScript_onDetachedFromHand;
    }



    void Update()
    {
        if (!photonView.isMine && GetComponent<PhotonView>().ownerId != 0)
        {
            transform.position = Vector3.Lerp(transform.position, this.correctPlayerPos, Time.deltaTime * 15);
            transform.rotation = Quaternion.Lerp(transform.rotation, this.correctPlayerRot, Time.deltaTime * 15);

        }
    }


    private void InteractableScript_onAttachedToHand(Hand hand)
    {
        GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.player.ID);
        GetComponent<PhotonView>().RPC("SetRigidbodyToGrabbed", PhotonTargets.All);

    }

    private void InteractableScript_onDetachedFromHand(Hand hand)
    {
        GetComponent<PhotonView>().RPC("SetRigidbodyToDetached", PhotonTargets.All);
    }



    [PunRPC]
    public void SetRigidbodyToGrabbed()
    {
        if (!photonView.isMine)
        {
            Debug.Log(gameObject.name + " gegriffen");
            if (GetComponent<Rigidbody>() != null)
            {
                GetComponent<Rigidbody>().isKinematic = true;
                GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.None;
            }
            
        }
    }

    [PunRPC]
    private void SetRigidbodyToDetached()
    {
        if (!photonView.isMine)
        {
            Debug.Log(gameObject.name + " losgelassen");
            if (GetComponent<Rigidbody>() != null)
            {
                GetComponent<Rigidbody>().isKinematic = false;
                GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.Interpolate;
            }
           
        }
    }
}
