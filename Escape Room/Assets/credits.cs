using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class credits : Photon.MonoBehaviour {

	[SerializeField]
	private GameObject skeleton;

	[SerializeField]
	private AudioSource sound;

	[SerializeField]
	private float secondsUntilNextCredit;

	private TextMesh display;

	private bool started;

	private string[] creditsText = {
		"Dungeon Escape",

		"Entwickler:",
		"Patrick Geyer",
		"Katharina Shits", 
		"Steffen Brehm",
		"Rico Zieger",

		"Grafiker/Modellierer:",
		"Patrick Geyer",
        "Katharina Shits", 
		"Steffen Brehm",
		"Rico Zieger",

		"Rekordhalter im Programm-Zum-Absturz-Bringen:",
		"Rico Zieger",

        "Master of the Multiplayer:",
        "Patrick Geyer",

		"Special Guest:\n"+
		"Rüdiger, das tanzende Skelett",

		"Songs:",
		"\"Shamanistic\" Kevin MacLeod (incompetech.com)\n"+
		"Licensed under Creative Commons: By Attribution 3.0 License\n"+
		"http://creativecommons.org/licenses/by/3.0/",

		"\"Interloper\" Kevin MacLeod (incompetech.com)\n"+
		"Licensed under Creative Commons: By Attribution 3.0 License\n"+
		"http://creativecommons.org/licenses/by/3.0/",

		"\"Scream 43\" ERH (freesound.org)\n"+
		"Licensed under Creative Commons: By Attribution 3.0 License\n"+
		"http://creativecommons.org/licenses/by/3.0/",

		"\"Footsteps, Concrete\" InspectorJ (freesound.org, edited)\n"+
		"Licensed under Creative Commons: By Attribution 3.0 License\n"+
		"http://creativecommons.org/licenses/by/3.0/",

		"Darude - Sandstorm",

		"Besonderen Dank an\n"+
		"Marc Durstewitz (Empea AR VR GmbH)",

		"Erstellt mit Unity",

		"MRE - SS2017 - HS Mannheim"

	};

	void startCredits(){
		display = gameObject.GetComponent<TextMesh> ();
		skeleton.GetComponent<Animation> ().Play ();
		sound.Play ();
		started = true;
			
		StartCoroutine (playSequence ());
	}

	IEnumerator playSequence(){
		foreach (string credit in creditsText) {		
			display.text = credit;
			yield return new WaitForSeconds (secondsUntilNextCredit);
		}

		photonView.RPC("SwitchToWaitingRoom", PhotonTargets.All);
	}

	//for test purposes
	//startCredits has to be called as soon as both players enter the scene
	void Awake(){
		startCredits ();
	}
	

	void Update(){
		if (started && !skeleton.GetComponent<Animation> ().isPlaying) {
			skeleton.GetComponent<Animation> ().Play ();
		}
	}


	[PunRPC]
	public void SwitchToWaitingRoom()
	{
		SceneManager.LoadScene(1);
	}
}
