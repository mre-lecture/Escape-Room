using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMystery : MonoBehaviour {

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
        float button1Distance = button1.transform.position.z;
        float button2Distance = button2.transform.position.z;
        float button3Distance = button3.transform.position.z;
        float button4Distance = button4.transform.position.z;
        float button5Distance = button5.transform.position.z;
        float button6Distance = button6.transform.position.z;
        float button7Distance = button7.transform.position.z;
        float button8Distance = button8.transform.position.z;
        float button9Distance = button9.transform.position.z;

        Debug.Log("" + button1Distance);
         if ((button1Distance  > 2.95 && button2Distance > 2.95 && button3Distance > 2.95) && (button4Distance < 2.95 && button5Distance < 2.95
            && button6Distance < 2.95 && button7Distance < 2.95 && button8Distance < 2.95 && button9Distance < 2.95))
            
         {
             Destroy(hiddenDoor);
         }
    }
    
}
