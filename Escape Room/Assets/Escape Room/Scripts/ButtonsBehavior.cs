using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class ButtonsBehavior : MonoBehaviour {
    public const float DISTANCE = 0.05f;
    private bool pressed = false;
    public Hand.AttachmentFlags pressingGripPoint = Hand.AttachmentFlags.ParentToHand | Hand.AttachmentFlags.DetachFromOtherHand;

    // Use this for initialization
    void Start () {
		
	}
	
	void ViveGripInteractionStart(Hand.AttachmentFlags gripPoint)
    {
        if(pressed) { return; }

        transform.position += new Vector3(0, 0, DISTANCE);
        pressed = true;
        pressingGripPoint = gripPoint;
    }

    void ViveGripInteractionStop(Hand.AttachmentFlags gripPoint)
    {
        if (!pressed ) { return; }

        transform.position += new Vector3(0, 0, DISTANCE);
        pressed = false;
    }
}
