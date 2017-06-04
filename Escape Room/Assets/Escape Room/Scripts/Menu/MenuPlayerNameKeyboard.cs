using System;
using UnityEngine;
using UnityEngine.UI;

public class MenuPlayerNameKeyboard : MenuPanels
{
    [SerializeField]
    private Text playerNameTextField;
    [SerializeField]
    private GameObject bigGrid;
    [SerializeField]
    private GameObject smallGrid;

    public static string playerNamePrefKey = "PlayerName";


    void Start()
    {
        string defaultName = "Spieler Name";

        if (playerNameTextField != null)
        {
            if (PlayerPrefs.HasKey(playerNamePrefKey))
            {
                defaultName = PlayerPrefs.GetString(playerNamePrefKey);
                playerNameTextField.text = defaultName;
            }
            else
            {
                PlayerPrefs.SetString(playerNamePrefKey, defaultName);
            }
        }

        PhotonNetwork.playerName = defaultName;
    }

    public void SetPlayerName()
    {
        if (!String.IsNullOrEmpty(playerNameTextField.text))
        {
            PhotonNetwork.playerName = playerNameTextField.text;

            PlayerPrefs.SetString(playerNamePrefKey, playerNameTextField.text);
        }
        else
        {
            playerNameTextField.text = PlayerPrefs.GetString(playerNamePrefKey);
        }
    }
    
    public void AddCharToNameText(Text textToAdd)
    {
        playerNameTextField.text += textToAdd.text;
    }

    public void ShiftButtonPressed()
    {
        bigGrid.SetActive(!bigGrid.activeSelf);
        smallGrid.SetActive(!smallGrid.activeSelf);
    }

    public void OkButtonPressed()
    {
        SetPlayerName();
    }

    public void SpaceButtonPressed()
    {
        playerNameTextField.text += " ";
    }

    public void BackButtonPressed()
    {
        if (!String.IsNullOrEmpty(playerNameTextField.text))
        {
            playerNameTextField.text = playerNameTextField.text.Remove(playerNameTextField.text.Length - 1);
        }
    }

    public void DeleteButtonPressed()
    {
        playerNameTextField.text = String.Empty;
    }
}