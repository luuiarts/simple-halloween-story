using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <para>Camera_Controller handles the agent or actor camera(s) to behave as needed</para>
/// </summary>
/// 
//a reference to player controller is required with this script
[RequireComponent(typeof(Player_Controller))]
public class Camera_Controller : MonoBehaviour
{

    //script references
    private Player_Controller scrpt_PC;

    //a reference to the player transform
    [Tooltip("Drag and drop the player transform (parent)")]
    public Transform transPlayer;
    // a reference to the camera we want to match
    [Tooltip("Drag the camera/parent to align this camera to the player's pos")]
    public Transform transCamMatchPlayer;
    // a reference to the camera for orbiting/taking close pictures
    [Tooltip("Drag in the orbit character camera")]
    public Transform transCamOrbitPlayer, transCamTopDownPlayer;


    // the zoom/scroll variables
    private float scrollMin = 12.0f, scrollMax = 40.0f, scrollCur = 25.0f;
    // a bool for if we are zoomed in enough
    private bool zoomToOrbit;

    //start
    private void Start()
    {
        //set up vars
        InitializeVariables();

    }//end of start


    //initialize variables/refs
    private void InitializeVariables()
    {
        //assign
        scrpt_PC = transform.GetComponent<Player_Controller>();
        scrpt_PC.camRay = transCamTopDownPlayer.GetComponent<Camera>();

        transCamTopDownPlayer.tag = "MainCamera";
        transCamOrbitPlayer.tag = "Untagged";

        zoomToOrbit = false;

    }//end of setting up variables


    //late update
    private void LateUpdate()
    {
        //match the camera position
        CamMatchPlayer();
        //check the zoom
        CamZoomCheck();

    }//end of late update



    // a function to match the camera's x,y position to the player's 
    private void CamMatchPlayer()
    {
        //align the camera to the player
        transCamMatchPlayer.position = new Vector3(transPlayer.position.x, transCamMatchPlayer.position.y, transPlayer.position.z);

    }//end of cam match player

    // a function to zoom the camera forward or backwards
    private void CamZoomCheck()
    {
        //check if the scroll cur is less than our max but still positive
        if(scrollCur < scrollMax && Input.mouseScrollDelta.y > 0)
        {
            //adjust the counter
            scrollCur += Input.mouseScrollDelta.y;
            //move the camera
            transCamMatchPlayer.localPosition += new Vector3(0, Input.mouseScrollDelta.y, 0);
            //transCamMatchPlayer.Translate(transCamMatchPlayer.forward * Input.mouseScrollDelta.y, Space.Self);
        }//end of increase and can
        //if decrease scroll and camera can
        else if(scrollCur > scrollMin && Input.mouseScrollDelta.y < 0)
        {
            //adjust the counter
            scrollCur += Input.mouseScrollDelta.y;
            //move the camera
            transCamMatchPlayer.localPosition += new Vector3(0, Input.mouseScrollDelta.y, 0);
            //transCamMatchPlayer.Translate(transCamMatchPlayer.forward * Input.mouseScrollDelta.y, Space.Self);
        }//end of decrease and can

        //if we scroll as close as we can go
        if (scrollCur <= scrollMin)
        {
            //if the orbit camera is still off
            if(transCamOrbitPlayer.gameObject.activeSelf == false)
            {
                //turn on orbit cam
                transCamOrbitPlayer.gameObject.SetActive(true);
                //turn off top down cam
                transCamTopDownPlayer.GetComponent<Camera>().enabled = false;             
                //assing it for raycast
                scrpt_PC.camRay = transCamOrbitPlayer.GetComponent<Camera>();
                //swap tags
                transCamTopDownPlayer.tag = "Untagged";
                transCamOrbitPlayer.tag = "MainCamera";
            }//end of orbit camera is off
           

        }//end of scrolled into orbit
        //else we are not close enough to orbit
        else
        {
            //if the orbit camera is still on
            if (transCamOrbitPlayer.gameObject.activeSelf == true)
            {
                //turn on top down cam
                transCamTopDownPlayer.GetComponent<Camera>().enabled = true;
                //turn off orbit cam
                transCamOrbitPlayer.gameObject.SetActive(false);
                //assing it for raycast
                scrpt_PC.camRay = transCamTopDownPlayer.GetComponent<Camera>();
                //fix tags
                transCamTopDownPlayer.tag = "MainCamera";
                transCamOrbitPlayer.tag = "Untagged";
            }//end of orbit camera is on

        }//end of scroll is greater than orbit
       

    }//end of cam zoom check


    // a function to orbit the camera if we are zommed in enough
    private void CamOrbitPlayer()
    {

    }//end of camera orbit player

}//end of camera_controller script