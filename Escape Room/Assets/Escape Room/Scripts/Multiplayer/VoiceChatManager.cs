using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceChatManager : Photon.MonoBehaviour {

    private void Start()
    {
        if (photonView.isMine)
        {
            GetComponent<PhotonVoiceRecorder>().enabled = true;
        }
    }
}
