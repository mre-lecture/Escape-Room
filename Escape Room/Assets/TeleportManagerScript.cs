﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class TeleportManagerScript : Photon.MonoBehaviour {

	//Used to initialize the teleport settings correct for each player
	void OnJoinedRoom()
	{			
		//first Player (starts outside cell)
		if (PhotonNetwork.player.ID == 1) {
			GameObject.Find("TeleportAreaCell").GetComponent<TeleportArea>().locked = true;
		} else {
			GameObject.Find("TeleportAreaRoom").GetComponent<TeleportArea>().locked = true;
		}
	}

	//just for test purposes 
	//(without a vive connected, the teleport areas are disabled when a player joins, so onJoinedRoom has no effect)
	void Update(){		
		//GameObject.Find("TeleportAreaCell").GetComponent<TeleportArea>().locked = true;
	}



}
