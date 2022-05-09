using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


/// <summary>
/// <para>Player_Controller Handles the mouse-to-navmesh movement for our main character and the player's mouse </para>
/// </summary>

public class Player_Controller : MonoBehaviour
{
    // a bool for if we can move or cannot move
    [HideInInspector]
    public bool canMove;

    //reference to camera that will shoot ray for navmesh target **Assigned by Camera_Controller
    [Tooltip("reference to camera that will shoot ray for navmesh target")]
    public Camera camRay;
    // a reference to the cursor3D object
    [Tooltip("Drag and drop a cursor 3d object here. It will be placed where the player clicks to move the character")]
    public Transform trans_cursor3D;
    // a reference to the navmesh agent we want to move (player)
    [Tooltip(" a reference to the navmesh agent we want to move (player)")]
    public NavMeshAgent agentPlayer;

    //a reference to our goal object we clicked on
    private GameObject obj_TempGoal;
    //a reference to our position data we clicked on (doesnt require goal object)
    private Transform trans_TempGoal;



    // references to the animation states
    [HideInInspector]
    public bool isWalking;
    // the animator for the player
    private Animator anim_Player;
    // the tag for walking bool
    private string animTag_isMoving = "isMoving";


    //start
    private void Start()
    {
        //set vars
        canMove = true;
        isWalking = false;
        if(agentPlayer!= null && agentPlayer.transform.GetComponent<Animator>() != null) { anim_Player = agentPlayer.transform.GetComponent<Animator>(); }
        UpdateCursor(Vector3.zero, 0, false);

    }//end of start


    //update
    private void Update()
    {
        //check if we have inputs
        CheckMouseButtons();

        //check goal
        WaitingToReachGoal();

        //update animation
        UpdateAnimation();
    }//end of update


    // a function that allows us to check for input of mouse
    private void CheckMouseButtons()
    {
        //if we release right mouse click
        if (Input.GetMouseButtonUp(1))
        {
            //reference to the raycast we will shoot
            Ray rayCast = camRay.ScreenPointToRay(Input.mousePosition);
            // a ref to where the ray hit
            RaycastHit hitInfo;
            // if we shoot the ray and it hits something store the information
            if (Physics.Raycast(rayCast, out hitInfo))
            {
                //if we can move
                if (canMove == true)
                {
                    //send the agent to that location
                    agentPlayer.SetDestination(hitInfo.point);
                    //update the cursor
                    UpdateCursor(hitInfo.point, 0, true);
                }//end of can move
                else
                //else if we cant
                {
                    //log it
                    print("xMOVEx : cannot");
                }//end of cant move

            }//end of raycast hit something


            print("hit: " + hitInfo.transform.gameObject.tag);

            //store ref
            trans_TempGoal = hitInfo.transform;

            //if it's not untagged
            if (hitInfo.transform.tag != "Untagged")
            {
                //assign it to our goal
                obj_TempGoal = hitInfo.transform.gameObject;
            }//end of not untagged
            else
            //remove temporary goal
            {
                obj_TempGoal = null;
            }//end of clicked anywhere

        }//end of release right mouse button

    }//end of check mouse buttons


    // a check for when we have a goal
    private void WaitingToReachGoal()
    {
        //if we have no transform goal then return
        if (trans_TempGoal == null) { return; }


        // Check if we've reached the destination
        if (!agentPlayer.pathPending)
        {

            //if we have no distance left to travel
            if (agentPlayer.remainingDistance <= agentPlayer.stoppingDistance)
            {
                //set walking to no
                isWalking = false;
                UpdateCursor(Vector3.zero, 0, false);

                //if we have no path
                if (!agentPlayer.hasPath || agentPlayer.velocity.sqrMagnitude == 0f)
                {
                    // Done
                    trans_TempGoal = null;                    

                    //if we have no object goal then return
                    if (obj_TempGoal == null) { return; }

                    //if it's the basket
                    //if (obj_TempGoal.tag == "Finish" || obj_TempGoal.tag == "Basket")


                    //clear our goal
                    obj_TempGoal = null;

                }//end of no path

            }//end of no remaining distance
            else
            //else if we have not reached our goal then we are moving
            {
                //set walking to be true
                isWalking = true;
                if (agentPlayer.remainingDistance != Mathf.Infinity) { UpdateCursor(Vector3.zero, agentPlayer.remainingDistance, true); } else { UpdateCursor(Vector3.zero, 1f, true); }

            }//end of have not reached our goal

        }//end of if we reached our destination      


    }//end of waiting to reach goal



    //a function for reading and assigning animations
    private void UpdateAnimation()
    {
        //if we dont have an animator thenreturn
        if(anim_Player == null) { return; }

        anim_Player.SetBool(animTag_isMoving, isWalking);

    }//end of update animations

    //updating the 3d cursor when we click
    private void UpdateCursor(Vector3 _pos, float _distSize, bool _showMesh)
    {
        //if no cursor then stop
        if(trans_cursor3D == null) { return; }
        trans_cursor3D.gameObject.SetActive(_showMesh);
        if (_pos != Vector3.zero) { trans_cursor3D.position = _pos; }
        if(_distSize != 0) { trans_cursor3D.localScale = new Vector3(_distSize, 0.1f, _distSize); }
          

    }//end of update cursor



}// end of player controller