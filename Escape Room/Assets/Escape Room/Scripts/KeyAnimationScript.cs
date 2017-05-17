using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyAnimationScript : MonoBehaviour {

    public Animator animator;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        animator.enabled = false;
        //animator.Play("Key_OpenDoorAnimation");
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("1"))
        {
            animator.Play("Key_OpenDoorAnimation");
        }
	}

    void OnTriggerEnter()
    {
        animator.enabled = true;
        animator.Play("Key_OpenDoorAnimation");
    }
}
