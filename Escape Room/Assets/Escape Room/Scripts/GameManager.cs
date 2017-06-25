using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [SerializeField]
    private float timeForThisLevel;

	// Use this for initialization
	void Start () {
        GameObject.Find("VRPlayer").GetComponent<Timer>().StartTimer(timeForThisLevel);		
	}
}
