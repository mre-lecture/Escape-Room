using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class SyncMoveableObject : SyncInteractableObjects
{

    private CircularDrive circularDrive;
    private LinearMapping linearMapping;

    private float correctOutAngle;
    private float correctLinearMappingValue;


    void Start()
    {
        circularDrive = GetComponent<CircularDrive>();
        linearMapping = GetComponent<LinearMapping>();
    }

    void Update()
    {
        if (!photonView.isMine)
        {
            transform.position = Vector3.Lerp(transform.position, this.correctPlayerPos, Time.deltaTime * 15);
            transform.rotation = Quaternion.Lerp(transform.rotation, this.correctPlayerRot, Time.deltaTime * 15);
            circularDrive.outAngle = correctOutAngle;
            linearMapping.value = correctLinearMappingValue;

        }
    }


    private void HandHoverUpdate(Hand hand)
    {
        if (hand.GetStandardInteractionButtonDown())
        {
            GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.player.ID);
        }
    }

    public new void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            // We own this player: send the others our data
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
            stream.SendNext(circularDrive.outAngle);
            stream.SendNext(linearMapping.value);
        }
        else
        {
            // Network player, receive data
            correctPlayerPos = (Vector3)stream.ReceiveNext();
            correctPlayerRot = (Quaternion)stream.ReceiveNext();
            correctOutAngle = (float)stream.ReceiveNext();
            correctLinearMappingValue = (float)stream.ReceiveNext();

        }
    }
}
