using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class soundMaster : MonoBehaviour {

    public AudioClip[] sounds;

    private AudioSource audio;   



    // Use this for initialization
    void Start () {        
        audio = GetComponent<AudioSource>();
        audio.clip = sounds[Random.Range(0, sounds.Length)];
        audio.Play();



    }


    void playRandomSound()
    {
        Invoke("LaunchProjectile", 2);
    }


}
