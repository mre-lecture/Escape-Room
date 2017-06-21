using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlayerStartPosInGameScene : MonoBehaviour
{
    [SerializeField]
    private GameObject playerSpawnerMaster;
    [SerializeField]
    private GameObject playerSpawnerSecondPlayer;

	// Use this for initialization
	void Start () {

	    GameObject tempPlayer = GameObject.Find("VRPlayer");

        if (PhotonNetwork.isMasterClient)
        {
            tempPlayer.transform.position = playerSpawnerMaster.transform.position;

        }
	    else
	    {
	        tempPlayer.transform.position = playerSpawnerSecondPlayer.transform.position;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
