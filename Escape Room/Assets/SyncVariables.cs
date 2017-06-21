using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncVariables : Photon.MonoBehaviour {

	private bool leftCellOpened, rightCellOpened, hallwayOpened;

	[PunRPC]
	public void syncLeftCellOpenedStatus()
	{		
		photonView.RPC("openLeftCell", PhotonTargets.All);
	}

	[PunRPC]
	public void syncRightCellOpenedStatus()
	{		
		photonView.RPC("openRightCell", PhotonTargets.All);
	}

	[PunRPC]
	public void syncHallwayOpenedStatus()
	{		
		photonView.RPC("openHallway", PhotonTargets.All);
	}

	//------------------------------------------------------------------------

	[PunRPC]
	public void openLeftCell()
	{
		leftCellOpened = true;
	}

	[PunRPC]
	public void openRightCell()
	{
		rightCellOpened = true;
	}

	[PunRPC]
	public void openHallway()
	{
		hallwayOpened = true;
	}

	//------------------------------------------------------------------------

	public bool isLeftCellDoorOpened(){
		return this.leftCellOpened;
	}

	public bool isRightCellDoorOpened(){
		return this.rightCellOpened;
	}

	public bool isHallwayDoorOpened(){
		return this.hallwayOpened;
	}
}
