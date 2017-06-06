﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class ButtonsBehavior : MonoBehaviour {
    public const float DISTANCE = 0.05f;
    private bool pressed = false;

    [SerializeField]
    private GameObject activeButton;

    // For Mouse testing
    private void OnMouseDown()
    {
        if (pressed)
        {
            activeButton.transform.position += new Vector3(0, 0, -DISTANCE);
            pressed = false;
            return;
        }else { 
            activeButton.transform.position += new Vector3(0, 0, DISTANCE);
            pressed = true;
        }

    }

    private void HandHoverUpdate(Hand hand)
    {
        if (hand.GetStandardInteractionButtonDown())
        {
            pressButton();
        }
    }

    // For Trigger Enter - Check Collider
    private void pressButton()
    {
        if (pressed) {
            activeButton.transform.position += new Vector3(0, 0, -DISTANCE);
            pressed = false;
            return;
        }else {
            activeButton.transform.position += new Vector3(0, 0, DISTANCE);
            pressed = true;
        }
        
    }
    
}
