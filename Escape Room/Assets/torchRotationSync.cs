using UnityEngine;
using Valve.VR.InteractionSystem;

public class torchRotationSync : MonoBehaviour
{  
    void HandHoverUpdate(Hand hand)
    {        
        if (hand.GetStandardInteractionButtonUp())
        {
            GetComponent<CircularDrive>().outAngle = 0;
            //TODO: Adjust rotation to the scene
            gameObject.transform.eulerAngles = new Vector3(0,180,0);
        } 
    }    

}