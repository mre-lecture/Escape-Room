using UnityEngine;

public class torchRotation : MonoBehaviour
{

    public Animator animator;

    GameObject door = GameObject.Find("Gate_door");
    GameObject torch = GameObject.Find("Torch3");

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.enabled = true;
         

    }

    void Update()
    {
       /* if (torchPar >= 70)
        {
            animator.enabled = true;
            animator.Play("Key_OpenDoorAnimation");

            door.GetComponent<BoxCollider>().enabled = true;
        }
*/        

    }
}