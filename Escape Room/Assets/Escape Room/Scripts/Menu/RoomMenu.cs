using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Valve.VR.InteractionSystem;

public class RoomMenu : MenuPanels
{
    [SerializeField]
    private GameObject joinRandomRoomButton;
    [SerializeField]
    private GameObject noAvailableRoomForRandomRoomText;
    [SerializeField]
    private NetworkManager networkManager;
    [SerializeField]
    private GameObject roomlistContentObject;
    [SerializeField]
    private GameObject roomlistLabePrefab;
    [SerializeField]
    private GameObject roomlistJoinButtonPrefab;


    protected override void Update()
    {
        if (PhotonNetwork.connected)
        {
            ShowConnectedGUI();
        }
        else
        {
            ShowConnectingGUI();
        }
    }

    public void CreateRoomButtonPressed()
    {
        networkManager.CreateRoom();
    }

    public void JoinRandomRoomButtonPressed()
    {
        networkManager.JoinRandomRoom();
    }

    public virtual void OnReceivedRoomListUpdate()
    {
        if (PhotonNetwork.GetRoomList().Length == 0)
        {
            noAvailableRoomForRandomRoomText.SetActive(true);
            joinRandomRoomButton.SetActive(false);
        }
        else
        {
            noAvailableRoomForRandomRoomText.SetActive(false);
            joinRandomRoomButton.SetActive(true);
        }

        foreach (Transform child in roomlistContentObject.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (RoomInfo game in PhotonNetwork.GetRoomList())
        {
            if (game.PlayerCount < game.MaxPlayers)
            {
                GameObject tempRoomlistLabe = Instantiate(roomlistLabePrefab, roomlistContentObject.transform);
                tempRoomlistLabe.GetComponentInChildren<Text>().text = game.Name;


                GameObject tempRoomlistJoinButton = Instantiate(roomlistJoinButtonPrefab, roomlistContentObject.transform);
                tempRoomlistJoinButton.GetComponent<UIElement>().onHandClick.AddListener(arg0 => PhotonNetwork.JoinRoom(game.Name));
            }
        }
    }
}
