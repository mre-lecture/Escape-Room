using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class KeyOpening : Photon.MonoBehaviour
{

	public Animator animator;
	public GameObject unlockable;
	public int unlockMinAngle;
	public int unlockMaxAngle;

    [SerializeField]
    private bool isLeftDoor, isRightDoor;

    [SerializeField]
    private GameObject syncObject;

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

			//Should be done on the end of the animation and right before destroy,
			//but don`t know how to do it
			UnlockDoor();


			Invoke("SyncDestroy",this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);

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

	[PunRPC]
	private void SyncDestroy(){
		photonView.RPC("ButtonPressed", PhotonTargets.All);
	}

	[PunRPC]
	private void DestroyKey(){
		//Destroy this object after the animation is finished
		//Destroy(gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
		gameObject.transform.position = new Vector3(-6,7,-1);
	}


	private void UnlockDoor()
	{
		if (unlockMinAngle != null)
		{
			unlockable.GetComponent<CircularDrive>().minAngle = unlockMinAngle;
		}

		if (unlockMinAngle != null)
		{
			unlockable.GetComponent<CircularDrive>().maxAngle = unlockMaxAngle;
		}
	    if (isLeftDoor)
	    {
	        syncObject.GetComponent<SyncVariables>().syncLeftCellOpenedStatus();
	    }else if (isRightDoor)
	    {
	        syncObject.GetComponent<SyncVariables>().syncRightCellOpenedStatus();
        }
	}
}