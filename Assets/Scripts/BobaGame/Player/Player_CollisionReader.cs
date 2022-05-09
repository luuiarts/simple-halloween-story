using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// collision reader just sends messages to collision handler!
/// </summary>

public class Player_CollisionReader : MonoBehaviour
{
    //reference to collision reader
    public Player_CollisionHandler scrpt_CollHndlr;

    // on trigger enter
    private void OnTriggerEnter(Collider trig)
    {
        //null check
        if(scrpt_CollHndlr == null) { print("missing ref to scrpt: " + transform.name); return; }

        scrpt_CollHndlr.OneTimeTriggers(trig.gameObject, true);

    }//end of trigger enter



    //on trigger stay
    private void OnTriggerStay(Collider trig)
    {
        //null check
        if (scrpt_CollHndlr == null) { print("missing ref to scrpt: " + transform.name); return; }

        scrpt_CollHndlr.AddTrigger(trig.gameObject);

    }//end of trigger stay



    //on trigger exit
    private void OnTriggerExit(Collider trig)
    {
        //null check
        if (scrpt_CollHndlr == null) { print("missing ref to scrpt: " + transform.name); return; }

        scrpt_CollHndlr.OneTimeTriggers(trig.gameObject, false);

    }//end of trigger exit

}//end of player collision reader
