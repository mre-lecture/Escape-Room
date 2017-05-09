using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NetworkState
{
    INITIALIZING, CONNECTING_TO_SERVER, CREATING_ROOM, ROOM_CREATED, JOINING_ROOM,
    ROOM_JOINED, PLAYING, SOME_PLAYER_CONNECTED, SOME_PLAYER_DISCONNECTED, DISCONNECTED
}

public enum NetworkEvent
{
    UPDATE_SCORE, TRAP_HIT, TO_SCORING_STATE, ROUND_ENDED
}

public class NetworkController : MonoBehaviour {

    

    public GameObject playerAvatarPrefab;

    public const string NETCODE_VERSION = "1.0";
    public const int MAX_PLAYERS = 5;


    void Start () {
        PhotonNetwork.ConnectUsingSettings(NETCODE_VERSION);
	}


    public NetworkState ActiveState { get; private set; }

    [SerializeField]
    private Connection _connection;

    public static event Action<NetworkState> OnNetworkStateChange;
    public static event Action<PhotonPlayer> OnRoundEnded;

    void Awake()
    {
        StartMultiplayerGame();
    }

    public void StartMultiplayerGame()
    {
        _connection.Init();
        _connection.Connect();
    }

    public void EndMultiplayerGame()
    {
        _connection.Disconnect();
    }

    public void ChangeNetworkState(NetworkState newState, object stateData = null)
    {
        ActiveState = newState;

        if (OnNetworkStateChange != null)
        {
            OnNetworkStateChange(ActiveState);
        }

        switch (ActiveState)
        {
            case NetworkState.ROOM_CREATED:
                ChangeNetworkState(NetworkState.PLAYING);
                break;
            case NetworkState.ROOM_JOINED:
                ChangeNetworkState(NetworkState.PLAYING);
                GameObject player = PhotonNetwork.Instantiate(playerAvatarPrefab.name, Vector3.zero, Quaternion.identity, 0);
                break;
        }

    }
}
