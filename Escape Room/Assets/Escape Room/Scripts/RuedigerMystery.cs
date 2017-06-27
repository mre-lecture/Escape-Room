using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RuedigerMystery : Photon.MonoBehaviour {


	public GameObject shieldTrigger, swordTrigger, helmTrigger;

	
	// Update is called once per frame
	void Update () {
		if (shieldTrigger.transform.childCount != 0 && swordTrigger.transform.childCount != 0 && helmTrigger.transform.childCount != 0) {
			photonView.RPC("SwitchToCredits", PhotonTargets.All);
		}
	
	}


	[PunRPC]
	public void SwitchToCredits()
	{
		SceneManager.LoadScene(3);
	}
}
