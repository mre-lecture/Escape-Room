using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class SyncInteractableObjects : Photon.MonoBehaviour {
    

    private Vector3 correctPlayerPos;
    private Quaternion correctPlayerRot;

    void Awake()
    {
        correctPlayerPos = transform.position;
        correctPlayerRot = transform.rotation;
    }

    void Update()
    {
        if (!photonView.isMine && GetComponent<PhotonView>().ownerId != 0)
        {
            transform.position = Vector3.Lerp(transform.position, this.correctPlayerPos, Time.deltaTime * 15);
            transform.rotation = Quaternion.Lerp(transform.rotation, this.correctPlayerRot, Time.deltaTime * 15);
            
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            // We own this player: send the others our data
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else
        {
            // Network player, receive data
            correctPlayerPos = (Vector3)stream.ReceiveNext();
            correctPlayerRot = (Quaternion)stream.ReceiveNext();
        }
    }



    [PunRPC]
    public void SetRigidbodyToGrabbed()
    {
        if (!photonView.isMine)
        {
            Debug.Log(gameObject.name + " gegriffen");
            GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.None;
        }
    }

    [PunRPC]
    private void SetRigidbodyToDetached()
    {
        if (!photonView.isMine)
        {
            Debug.Log(gameObject.name + " losgelassen");
            GetComponent<Rigidbody>().isKinematic = false;
            GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.Interpolate;
        }
    }
}
