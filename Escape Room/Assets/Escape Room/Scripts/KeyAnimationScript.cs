using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class KeyAnimationScript : MonoBehaviour
{

    public Animator animator;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.enabled = false;
        //GameObject door = GameObject.Find("Gate_door");
        //door.GetComponent<Interactable>().enabled = false;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag.Equals("KeyTrigger"))
        {
            animator.enabled = true;
            animator.Play("Key_OpenDoorAnimation");
            
            
            GameObject door = GameObject.Find("Gate_door");
            door.GetComponent<BoxCollider>().enabled = true;
        }
    }

    void DestroyKey()
    {
        Destroy(this.gameObject);
    }
}