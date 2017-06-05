using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class TeleportManagerScript : Photon.MonoBehaviour {

    [SerializeField]
    private TeleportArea teleportAreaCell;
    [SerializeField]
    private TeleportArea teleportAreaRoom1;
    [SerializeField]
    private TeleportArea teleportAreaRoom2;

    //Used to initialize the teleport settings correct for each player
    void OnJoinedRoom()
	{			
		//first Player (starts outside cell)
		if (PhotonNetwork.player.IsMasterClient ) {                        
			teleportAreaCell.locked = true;
		} else {
			teleportAreaRoom1.locked = true;
            teleportAreaRoom2.locked = true;
        }
	}

	//just for test purposes 
	//(without a vive connected, the teleport areas are disabled when a player joins, so onJoinedRoom has no effect)
	void Update(){		
		//GameObject.Find("TeleportAreaCell").GetComponent<TeleportArea>().locked = true;
	}



}
