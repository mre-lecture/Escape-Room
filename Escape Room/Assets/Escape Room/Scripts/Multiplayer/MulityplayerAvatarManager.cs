using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MulityplayerAvatarManager : MonoBehaviour
{
    [SerializeField]
    private GameObject headPrefab;
    [SerializeField]
    private GameObject leftHandPrefab;
    [SerializeField]
    private GameObject rightHandPrefab;

	// Use this for initialization
	void Start () {
	    if (headPrefab == null || leftHandPrefab == null || rightHandPrefab == null)
	    {
	        Debug.LogError(
	            "<Color=Red><a>Missing</a></Color> one or more playerPrefab Reference. Please set it up in GameObject 'NetworkManager'",
	            this);
	    }
	    else
	    {
	        DontDestroyOnLoad(PhotonNetwork.Instantiate(headPrefab.name, ViveManager.Instance.head.transform.position,
	            ViveManager.Instance.head.transform.rotation, 0));
	        DontDestroyOnLoad(PhotonNetwork.Instantiate(leftHandPrefab.name, ViveManager.Instance.leftHand.transform.position,
	            ViveManager.Instance.leftHand.transform.rotation, 0));
	        DontDestroyOnLoad(PhotonNetwork.Instantiate(rightHandPrefab.name, ViveManager.Instance.rightHand.transform.position,
	            ViveManager.Instance.rightHand.transform.rotation, 0));
	    }
    }
}
