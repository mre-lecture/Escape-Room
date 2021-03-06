﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkManager : MonoBehaviour {
    
    [SerializeField]
    private bool debuggConnect = false;

    [SerializeField]
    private GameObject headPrefab;
    [SerializeField]
    private GameObject leftHandPrefab;
    [SerializeField]
    private GameObject rightHandPrefab;

    public virtual void Start()
    {
        PhotonNetwork.ConnectUsingSettings("0.1");
        var temp = PhotonVoiceNetwork.Client;
    }

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(PlayerPrefs.GetString(MenuPlayerNameKeyboard.playerNamePrefKey) + "'s Raum", new RoomOptions() { MaxPlayers = 2 }, null);
    }

    public void JoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }


    // below, we implement some callbacks of PUN
    // you can find PUN's callbacks in the class PunBehaviour or in enum PhotonNetworkingMessage
    public virtual void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster() was called by PUN. Now this client is connected and could join a room. Calling: PhotonNetwork.JoinRandomRoom();");
        PhotonNetwork.JoinLobby();
    }

    public virtual void OnJoinedLobby()
    {
        Debug.Log("OnJoinedLobby(). This client is connected and does get a room-list, which gets stored as PhotonNetwork.GetRoomList(). This script now calls: PhotonNetwork.JoinRandomRoom();");
        if (debuggConnect)
        {
            JoinRandomRoom();
        }
    }

    public virtual void OnPhotonRandomJoinFailed()
    {
        Debug.Log("OnPhotonRandomJoinFailed() was called by PUN. No random room available, so we create one. Calling: PhotonNetwork.CreateRoom(null, new RoomOptions() {maxPlayers = 4}, null);");
        if (debuggConnect)
        {
            PhotonNetwork.CreateRoom("DebuggRoom", new RoomOptions() { MaxPlayers = 2 }, null);
        }
    }

    // the following methods are implemented to give you some context. re-implement them as needed.

    public virtual void OnFailedToConnectToPhoton(DisconnectCause cause)
    {
        Debug.LogError("Cause: " + cause);
    }

    public void OnJoinedRoom()
    {

        Debug.Log("OnJoinedRoom() called by PUN. Now this client is in a room. From here on, your game would be running. For reference, all callbacks are listed in enum: PhotonNetworkingMessage");

        if (headPrefab == null || leftHandPrefab == null || rightHandPrefab == null)
        {
            Debug.LogError(
                "<Color=Red><a>Missing</a></Color> one or more playerPrefab Reference. Please set it up in GameObject 'NetworkManager'",
                this);
        }
        else
        {
            PhotonNetwork.Instantiate(headPrefab.name, ViveManager.Instance.head.transform.position,
                ViveManager.Instance.head.transform.rotation, 0);
            PhotonNetwork.Instantiate(leftHandPrefab.name, ViveManager.Instance.leftHand.transform.position,
                ViveManager.Instance.leftHand.transform.rotation, 0);
            PhotonNetwork.Instantiate(rightHandPrefab.name, ViveManager.Instance.rightHand.transform.position,
                ViveManager.Instance.rightHand.transform.rotation, 0);
        }

        if (!debuggConnect)
        {
            SceneManager.LoadScene(1);
        }
    }
}
