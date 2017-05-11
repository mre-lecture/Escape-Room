using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Valve.VR.InteractionSystem;

public class Test2 : Photon.PunBehaviour
{

    // Use this for initialization
    void Start()
    {
        if (!photonView.isMine)
        {
            GetComponent<Player>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
