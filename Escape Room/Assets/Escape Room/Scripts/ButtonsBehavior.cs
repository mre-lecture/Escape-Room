using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsBehavior : MonoBehaviour
{

    [SerializeField]
    private GameObject button1;

    [SerializeField]
    private GameObject button2;

    [SerializeField]
    private GameObject button3;

    [SerializeField]
    private GameObject button4;

    [SerializeField]
    private GameObject button5;

    [SerializeField]
    private GameObject button6;

    [SerializeField]
    private GameObject button7;

    [SerializeField]
    private GameObject button8;

    [SerializeField]
    private GameObject button9;

    [SerializeField]
    private GameObject hiddenDoor;


    // Update is called once per frame

    void Update()
    {
        ButtonBehavior button1Script = button1.GetComponent<ButtonBehavior>();
        ButtonBehavior button2Script = button2.GetComponent<ButtonBehavior>();
        ButtonBehavior button3Script = button3.GetComponent<ButtonBehavior>();
        ButtonBehavior button4Script = button4.GetComponent<ButtonBehavior>();
        ButtonBehavior button5Script = button5.GetComponent<ButtonBehavior>();
        ButtonBehavior button6Script = button6.GetComponent<ButtonBehavior>();
        ButtonBehavior button7Script = button7.GetComponent<ButtonBehavior>();
        ButtonBehavior button8Script = button8.GetComponent<ButtonBehavior>();
        ButtonBehavior button9Script = button9.GetComponent<ButtonBehavior>();
/*
        float button1Distance = button1.transform.position.z;
        float button2Distance = button2.transform.position.z;
        float button3Distance = button3.transform.position.z;
        float button4Distance = button4.transform.position.z;
        float button5Distance = button5.transform.position.z;
        float button6Distance = button6.transform.position.z;
        float button7Distance = button7.transform.position.z;
        float button8Distance = button8.transform.position.z;
        float button9Distance = button9.transform.position.z;*/
        
        if (button1Script.pressed && !button2Script.pressed && !button3Script.pressed && button4Script.pressed && !button5Script.pressed && 
            !button6Script.pressed && !button7Script.pressed && button8Script.pressed && !button9Script.pressed)

        {
            Destroy(button1.GetComponent<Rigidbody>());
            Destroy(button1.GetComponent<BoxCollider>());

            Destroy(button2.GetComponent<Rigidbody>());
            Destroy(button2.GetComponent<BoxCollider>());

            Destroy(button3.GetComponent<Rigidbody>());
            Destroy(button3.GetComponent<BoxCollider>());

            Destroy(button4.GetComponent<Rigidbody>());
            Destroy(button4.GetComponent<BoxCollider>());

            Destroy(button5.GetComponent<Rigidbody>());
            Destroy(button5.GetComponent<BoxCollider>());

            Destroy(button6.GetComponent<Rigidbody>());
            Destroy(button6.GetComponent<BoxCollider>());

            Destroy(button7.GetComponent<Rigidbody>());
            Destroy(button7.GetComponent<BoxCollider>());

            Destroy(button8.GetComponent<Rigidbody>());
            Destroy(button8.GetComponent<BoxCollider>());

            Destroy(button9.GetComponent<Rigidbody>());
            Destroy(button9.GetComponent<BoxCollider>());

            Destroy(hiddenDoor);
        }
    }

}
