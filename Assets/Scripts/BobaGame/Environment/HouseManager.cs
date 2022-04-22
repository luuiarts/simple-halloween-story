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

    private bool toggle_HideWalls = false;

    //some testing GUI
    void OnGUI()
    {
        if(houseParts_ToHide == null) { print("No Objects Selected for House Parts To Hide"); return; }
        //button to hide walls
        toggle_HideWalls = GUI.Toggle(new Rect(10, 10, 100, 30), toggle_HideWalls, "Toggle House Entrance");

    }//end of testing gui

    //update
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
                Debug.Log("left-click over a GUI element!");
            else Debug.Log("just a left-click!");
        }

    }//end update


}//end of house manager
