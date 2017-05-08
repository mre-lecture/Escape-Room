using UnityEngine;
using System.Collections;

public class Torchelight : MonoBehaviour {
	
	public GameObject TorchLight;
	public GameObject MainFlame;
	public GameObject BaseFlame;
	public GameObject Etincelles;
	public GameObject Fumee;
	public float MaxLightIntensity;
	public float IntensityLight;
	

	void Start () {
		TorchLight.GetComponent<Light>().intensity=IntensityLight;
		MainFlame.GetComponent<ParticleSystem>().emissionRate=IntensityLight*200f;
		BaseFlame.GetComponent<ParticleSystem>().emissionRate=IntensityLight*150f;	
		Etincelles.GetComponent<ParticleSystem>().emissionRate=IntensityLight*70f;
		Fumee.GetComponent<ParticleSystem>().emissionRate=IntensityLight*120f;
	}
	

	void Update () {
		if (IntensityLight<0) IntensityLight=0;
		if (IntensityLight>MaxLightIntensity) IntensityLight=MaxLightIntensity;		

		TorchLight.GetComponent<Light>().intensity=IntensityLight/20f+Mathf.Lerp(IntensityLight-1f,IntensityLight+1f,Mathf.Cos(Time.time*30));

		TorchLight.GetComponent<Light>().color=new Color(Mathf.Min(IntensityLight/15f,10f),Mathf.Min(IntensityLight/20f,10f),0f);
		MainFlame.GetComponent<ParticleSystem>().emissionRate=IntensityLight*200f;
		BaseFlame.GetComponent<ParticleSystem>().emissionRate=IntensityLight*150f;
		Etincelles.GetComponent<ParticleSystem>().emissionRate=IntensityLight*70f;
		Fumee.GetComponent<ParticleSystem>().emissionRate=IntensityLight*120f;		

	}
}
