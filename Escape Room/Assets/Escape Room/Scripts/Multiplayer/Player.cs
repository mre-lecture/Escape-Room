using Photon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : PunBehaviour
{

    public float speed = 10f;

    private float lastSynchronizationTime = 0f;
    private float syncDelay = 0f;
    private float syncTime = 0f;
    private Vector3 syncStartPosition = Vector3.zero;
    private Vector3 syncEndPosition = Vector3.zero;

    void Update()
    {
        if (photonView.isMine == false && PhotonNetwork.connected == true)
        {
            return;
        }

        InputMovement();

        /*if (photonView.isMine)
        {
            InputMovement();
        }
        else
        {
            SyncedMovement();
        }*/
    }

    void InputMovement()
    {
        if (Input.GetKey(KeyCode.W))
            GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + Vector3.forward * speed * Time.deltaTime);

        if (Input.GetKey(KeyCode.S))
            GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position - Vector3.forward * speed * Time.deltaTime);

        if (Input.GetKey(KeyCode.D))
            GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + Vector3.right * speed * Time.deltaTime);

        if (Input.GetKey(KeyCode.A))
            GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position - Vector3.right * speed * Time.deltaTime);
    }

    /*void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(GetComponent<Rigidbody>().position);
        }
        else
        {
            syncEndPosition = (Vector3)stream.ReceiveNext();
            syncStartPosition = GetComponent<Rigidbody>().position;

            syncTime = 0f;
            syncDelay = Time.time - lastSynchronizationTime;
            lastSynchronizationTime = Time.time;
        }
    }

    private void SyncedMovement()
    {
        syncTime += Time.deltaTime;
        GetComponent<Rigidbody>().position = Vector3.Lerp(syncStartPosition, syncEndPosition, syncTime / syncDelay);
    }*/
}
