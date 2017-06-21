using UnityEngine;
using Valve.VR.InteractionSystem;

public class torchRotationSync : MonoBehaviour
{  
    void HandHoverUpdate(Hand hand)
    {        
        if (hand.GetStandardInteractionButtonUp())
        {
            resetTorchRotation();
        } 
    }

    private void OnHandHoverEnd(Hand hand)
    {
        resetTorchRotation();
    }

    private void resetTorchRotation()
    {
        GetComponent<CircularDrive>().outAngle = 0;
        //TODO: Adjust rotation to the scene
        gameObject.transform.eulerAngles = new Vector3(0, 90, 0);
    }

}