using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class KeyAnimationScript : MonoBehaviour
{

    public Animator animator;
    public GameObject unlockableDoor;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.enabled = false;
    }

    
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag.Equals("KeyTrigger"))
        {
            animator.enabled = true;

            //Should be done right before destroy, but don`t know how to
            //we need to do this, before this object is destroyed!
            UnlockDoor();

            //Destroy this this object after the animation is finished
            Destroy(gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
            

            //detach from hand
            this.gameObject.transform.parent = null;
            //detach object from hand (depending on which hand it is in)
            Hand[] hands = GetComponents<Hand>();
            foreach (Hand hand in hands)
            {
                hand.DetachObject(collider.gameObject, true);
            }
        }
    }


    private void UnlockDoor()
    {
        unlockableDoor.GetComponent<CircularDrive>().maxAngle = 204;
    }
}