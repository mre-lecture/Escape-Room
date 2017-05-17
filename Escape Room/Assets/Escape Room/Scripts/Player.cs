using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private Animator anim;
	// Use this for initialization
	void Start () {
        anim = gameObject.GetComponentInChildren<Animator>();
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey("up")) {
            anim.SetInteger("AnimPar", 1);
        }
        else if(Input.GetKey("right"))
        {
            anim.SetInteger("AnimPar", 2);
        }
        else if (Input.GetKey("left"))
        {
            anim.SetInteger("AnimPar", 3);
        }
        else
        {
            anim.SetInteger("AnimPar", 0);
        }
	}
}
