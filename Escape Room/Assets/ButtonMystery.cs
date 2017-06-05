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

         if ((button1.GetActive()  && button4.GetActive()  && button8.GetActive())  && !(button2.GetActive() || button3.GetActive() ||  button5.GetActive()
            || button6.GetActive() || button7.GetActive() ||  button9.GetActive()))
         {
             Destroy(hiddenDoor);
         }
    }
    
}
