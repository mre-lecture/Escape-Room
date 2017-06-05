using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class ButtonsBehavior : MonoBehaviour {
    public const float DISTANCE = 0.05f;
    private bool pressed = false;

    [SerializeField]
    private GameObject activeButton;
   

    private void OnTriggerEnter(Collider collider)
    {
        if (pressed) { return; }

     
            activeButton.transform.position += new Vector3(0, 0, DISTANCE);
            activeButton.SetActive(true);
        
        
    }
}
