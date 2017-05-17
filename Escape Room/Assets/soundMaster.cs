using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class soundMaster : MonoBehaviour {

    public AudioClip[] sounds;

    private AudioSource audio;   

	public int secondsBetweenSounds = 60;



    // Use this for initialization
    void Start () {        
        audio = GetComponent<AudioSource>();
		playRandomSound ();
    }


    void playRandomSound()
    {
		audio.clip = sounds[Random.Range(0, sounds.Length)];
		audio.Play();

		//Calls itself some time (#secondsBetweenSounds) after the last audio clip finished
		//This way it loops
		Invoke("playRandomSound", audio.clip.length+secondsBetweenSounds);
    }


}
