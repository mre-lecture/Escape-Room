using Photon;
using UnityEngine;

public class Connection : PunBehaviour
{
    [SerializeField]
    private NetworkController _networkController;

    public void Init()
    {
        _networkController.ChangeNetworkState(NetworkState.INITIALIZING);
        PhotonNetwork.autoJoinLobby = false;
    }

    public void Connect()
    {
        _networkController.ChangeNetworkState(NetworkState.CONNECTING_TO_SERVER);
        if (PhotonNetwork.connected)
        {
            OnConnectedToMaster();
        }
        else
        {
            PhotonNetwork.ConnectUsingSettings(NetworkController.NETCODE_VERSION);
        }
    }

    public void Disconnect()
    {
        PhotonNetwork.Disconnect();
    }




    #region PUN Callbacks
    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster() was called by PUN. Now this client is connected and could join a room. Calling: PhotonNetwork.JoinRandomRoom();");
        _networkController.ChangeNetworkState(NetworkState.JOINING_ROOM);
        if (PhotonNetwork.inRoom)
        {
            OnJoinedRoom();
        }
        else
        {
            _networkController.ChangeNetworkState(NetworkState.JOINING_ROOM);
            PhotonNetwork.JoinRandomRoom();
        }
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("OnJoinedLobby(). This client is connected and does get a room-list, which gets stored as PhotonNetwork.GetRoomList(). This script now calls: PhotonNetwork.JoinRandomRoom();");
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnPhotonRandomJoinFailed(object[] codeAndMsg)
    {
        Debug.Log("OnPhotonRandomJoinFailed() was called by PUN. No random room available, so we create one. Calling: PhotonNetwork.CreateRoom(null, new RoomOptions() {maxPlayers = 4}, null);");
        _networkController.ChangeNetworkState(NetworkState.CREATING_ROOM);
        PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = NetworkController.MAX_PLAYERS }, null);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom() called by PUN. Now this client is in a room. From here on, your game would be running. For reference, all callbacks are listed in enum: PhotonNetworkingMessage");
        _networkController.ChangeNetworkState(NetworkState.ROOM_JOINED);
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("OnCreatedRoom() called by PUN. Now this client is in a room. From here on, your game would be running. For reference, all callbacks are listed in enum: PhotonNetworkingMessage");
        _networkController.ChangeNetworkState(NetworkState.ROOM_CREATED);
    }

    public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
    {
        _networkController.ChangeNetworkState(NetworkState.SOME_PLAYER_CONNECTED, newPlayer);
    }

    public override void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
    {
        _networkController.ChangeNetworkState(NetworkState.SOME_PLAYER_DISCONNECTED, otherPlayer);
    }

    public override void OnDisconnectedFromPhoton()
    {
        _networkController.ChangeNetworkState(NetworkState.DISCONNECTED);
    }

    public override void OnFailedToConnectToPhoton(DisconnectCause cause)
    {
        Debug.LogError("Cause: " + cause);
    }
    #endregion
}
