using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class RüdigerMystery : MonoBehaviour {

	public GameObject[] solutionTrigger;

	private bool[] triggerActivated;

	void Start ()
	{
		/*triggerActivated = new bool[solutionTrigger.Length];

		for (int i = 0; i < triggerActivated.Length; i++)
		{
			triggerActivated[i] = false;
		}*/
	}

	public void onObjectActivated(GameObject triggerObject) 
	{
		bool puzzleSolved = true; 
		/*
		for (int i = 0; i < solutionTrigger.Length; i++) 
		{
			if (solutionTrigger[i].Equals(triggerObject)) 
			{
				triggerActivated [i] = true;
			}
			else if (triggerActivated[i] == false)
			{
				puzzleSolved = false;
			}
		}

		if (puzzleSolved)
		{
			// do something
			//throw(new UnityException());
		}*/
	}
}
