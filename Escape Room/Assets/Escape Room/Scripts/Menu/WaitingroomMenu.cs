using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaitingroomMenu : MonoBehaviour {

    [SerializeField]
    private GameObject waitingOtherPlayerLabel;
    [SerializeField]
    private GameObject startGameButton;
    [SerializeField]
    private PhotonView photonView;
	
	// Update is called once per frame
	void Update () {
        if (PhotonNetwork.room.PlayerCount != PhotonNetwork.room.MaxPlayers)
        {
            waitingOtherPlayerLabel.SetActive(true);
            startGameButton.SetActive(false);
        }
        else
        {
            waitingOtherPlayerLabel.SetActive(false);
            startGameButton.SetActive(true);
        }
	}

    public void StartGameButtonPressed()
    {
        photonView.RPC("SwitchToGameScene", PhotonTargets.All);
    }

    public void LeavRoomButtonPressed()
    {
        
        GameObject[] tempPlayerAvatarObjectsArray = GameObject.FindGameObjectsWithTag("Player");

        foreach (var playerAvatar in tempPlayerAvatarObjectsArray)
        {
            if (playerAvatar.GetComponent<PhotonView>().isMine)
            {
                Destroy(playerAvatar);
            }
        }

        GameObject tempVRPlayer = GameObject.Find("VRPlayer");


        PhotonNetwork.Disconnect();
        Destroy(tempVRPlayer);
        SceneManager.LoadScene(0);
        
    }

    [PunRPC]
    public void SwitchToGameScene()
    {
        SceneManager.LoadScene(2);
    }
}
