using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyPlayerMovement : Photon.MonoBehaviour {

    public int index = 1;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update () {
        if (photonView.isMine)
        {
            switch (index)
            {
                case 1://head
                    transform.position = ViveManager.Instance.head.transform.position;
                    transform.rotation = ViveManager.Instance.head.transform.rotation;
                    break;
                case 2://left
                    transform.position = ViveManager.Instance.leftHand.transform.position;
                    transform.rotation = ViveManager.Instance.leftHand.transform.rotation;
                    break;
                case 3://right
                    transform.position = ViveManager.Instance.rightHand.transform.position;
                    transform.rotation = ViveManager.Instance.rightHand.transform.rotation;
                    break;
            }
        }
	}
}
