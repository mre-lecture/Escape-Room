using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPanels : MonoBehaviour
{
    [SerializeField]
    private GameObject ConnectedView;
    [SerializeField]
    private GameObject ConnectingView;

    protected virtual void Update()
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

    protected void ShowConnectingGUI()
    {
        ConnectedView.SetActive(false);
        ConnectingView.SetActive(true);
    }

    protected void ShowConnectedGUI()
    {
        ConnectedView.SetActive(true);
        ConnectingView.SetActive(false);
    }
}
