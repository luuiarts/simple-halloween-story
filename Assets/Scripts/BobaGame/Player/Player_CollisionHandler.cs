using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// player collision handler handles all the object management when the player collides with things. This all comes from 
/// </summary>
public class Player_CollisionHandler : MonoBehaviour
{

    // the lists
    // onstay
    private List<GameObject> list_TrigStay = new List<GameObject>();


    // the house manager
    public HouseManager scrpt_HouseMngr;


    // a function to add triggers to the list
    public void AddTrigger(GameObject _TrigObj)
    {
        //if we dont have the reference already then add it
        if (!list_TrigStay.Contains(_TrigObj)) { list_TrigStay.Add(_TrigObj); }

    }//end of add triggers


    // a function for onetime triggers
    public void OneTimeTriggers(GameObject _TrigObj, bool _wasEnter)
    {

        CheckTagNames(_TrigObj, _wasEnter);
        //if it's exit then check if we need to remove it
        if (list_TrigStay.Contains(_TrigObj)) { list_TrigStay.Remove(_TrigObj); }

    }//end of one time triggers


    // a function to compare the tags based on the actions
    private void CheckTagNames(GameObject _TrigObj, bool _wasEnter)
    {
        //if outside
        if (_TrigObj.name == "Outside")
        {
            print("Outside is registered: " + _wasEnter);

            //if we enetered outside then turn on house walls, otherwise turn them off
            if (_wasEnter) { scrpt_HouseMngr.ToggleFromOutside(true); } else { scrpt_HouseMngr.ToggleFromOutside(false); }
        }//end outside




    }//end of tag names



}//end of collision handler

