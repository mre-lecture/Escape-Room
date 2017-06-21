using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class TeleportManagerScript : Photon.MonoBehaviour {

    [SerializeField]
    private TeleportArea teleportAreaLeftCell;
	[SerializeField]
	private TeleportArea teleportAreaRightCell;
    [SerializeField]
    private TeleportArea teleportAreaHallway;
    [SerializeField]
    private TeleportArea teleportAreaRoom;
	[SerializeField]
	private GameObject syncVariablesObject;

	private bool masterClient;

    //Used to initialize the teleport settings correct for each player
    void OnJoinedRoom()
	{			
		masterClient = PhotonNetwork.player.IsMasterClient;

		//first Player (starts outside cell)
		if (masterClient) { 
			teleportAreaRightCell.locked = true;
		} else {
			teleportAreaLeftCell.locked = true;
        }

		teleportAreaHallway.locked = true;
		teleportAreaRoom.locked = true;
	}

	void Update(){		
		//unlock tp in hallway for master if his cell door is opened and the hallway tp area isn't already unlocked
		if (masterClient && teleportAreaHallway.locked && syncVariablesObject.GetComponent<SyncVariables> ().isLeftCellDoorOpened ()) {
			teleportAreaHallway.locked = false;
			//same as above for player2
		} else if (!masterClient && teleportAreaHallway.locked && syncVariablesObject.GetComponent<SyncVariables> ().isRightCellDoorOpened ()) {
			teleportAreaHallway.locked = false;
			//unlock right cell tp area for master if isn't already unlocked and he already has access to the hallway
		} else if (masterClient && !teleportAreaHallway.locked && teleportAreaRightCell.locked && syncVariablesObject.GetComponent<SyncVariables> ().isRightCellDoorOpened ()) {
			teleportAreaRightCell.locked = false;
			//same as above for player 2
		} else if (!masterClient && !teleportAreaHallway.locked && teleportAreaLeftCell.locked && syncVariablesObject.GetComponent<SyncVariables> ().isLeftCellDoorOpened ()) {
			teleportAreaLeftCell.locked = false;
			//if any player has the hallway tp enabled and the room door is opened, enable room tp area.
		} else if (!teleportAreaHallway.locked && teleportAreaRoom.locked && syncVariablesObject.GetComponent<SyncVariables> ().isHallwayDoorOpened ()) {
			teleportAreaRoom.locked = false;
		}
	}



}
