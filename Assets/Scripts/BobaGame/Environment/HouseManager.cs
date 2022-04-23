using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// House Manager is expected to control the environment of the house models/meshes
/// </summary>
public class HouseManager : MonoBehaviour
{
    public List<GameObject> houseParts_ToHide = new List<GameObject>();

    private bool toggle_HideWalls = true;
    private bool lastToggle = true;

    //start
    private void Start()
    {
        
    }//end start

    //some testing GUI
    void OnGUI()
    {
        if(houseParts_ToHide.Count == 0) { print("No Objects Selected for House Parts To Hide"); return; }

        //button to hide walls
        toggle_HideWalls = GUI.Toggle(new Rect(10, 10, 100, 30), toggle_HideWalls, "Toggle House Entrance");

    }//end of testing gui

    //update
    private void Update()
    {
        // need refs or stop
        if (houseParts_ToHide.Count == 0) { print("No Objects Selected for House Parts To Hide"); return; }
        // if we changed the checkbox
        if(lastToggle != toggle_HideWalls)
        {
            //loop houseparts to enable/disable
            foreach(GameObject _HObj in houseParts_ToHide)
            {
                //enable or disable
                _HObj.SetActive(!_HObj.activeSelf);
            }//end loop houseparts

            //set toggles match
            lastToggle = toggle_HideWalls;
        }//end of changed checkbox

    }//end update


}//end of house manager
