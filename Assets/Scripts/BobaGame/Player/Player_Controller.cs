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

    public SkinnedMeshRenderer mRen_Player, mRen_SittingPlayer, mRen_LayingPlayer;
    public GameObject obj_PlayerLight;
    public Transform trans_PlayerHandR, trans_FlowerDropoff;
    private bool isHoldingObject;
    private bool hasFlowers;
    private bool vaseHasFlower;    
    private bool hasTeapot;
    private bool teapotHasWater;
    private bool teapotHasHeat;
    private bool hasCup;
    private bool hasPouredTea;

    private bool hasFinishedTheGame;    
    
    private Transform trans_HoldingObject;


    // references to the animation states
    [HideInInspector]
    public bool isWalking;
    // the animator for the player
    private Animator anim_Player;
    // the tag for walking bool
    private string animTag_isMoving = "isMoving";

    // the UI script we need to reference
    public UI_InformationText scrpt_UIInfo;



    //start
    private void Start()
    {
        //set vars
        canMove = true;
        isWalking = false;
        if(agentPlayer!= null && agentPlayer.transform.GetComponent<Animator>() != null) { anim_Player = agentPlayer.transform.GetComponent<Animator>(); }
        UpdateCursor(Vector3.zero, 0, false);
        mRen_SittingPlayer.enabled = false;
        mRen_LayingPlayer.enabled = false;
        scrpt_UIInfo.UpdateUI(false, "");
        hasFlowers = false;
        hasFinishedTheGame = false;
        isHoldingObject = false;
        vaseHasFlower = false;
        hasTeapot = false;
        teapotHasWater = false;
        teapotHasHeat = false;
        hasCup = false;
        hasPouredTea = false;

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

                    //if we have an object goal then  //send the obj goal to our custom function to analyze what to do
                    if (obj_TempGoal != null) { AnalyzeInteractable(obj_TempGoal); }              
                    
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

        //if we are walking then turn off any side animations / UI / idle temporary objects
        if (isWalking) { scrpt_UIInfo.UpdateUI(false, ""); mRen_LayingPlayer.enabled = false; mRen_SittingPlayer.enabled = false; mRen_Player.enabled = true; obj_PlayerLight.SetActive(true); }

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


    //a function to handle all interactable objects
    private void AnalyzeInteractable(GameObject _IntObj)
    {
            
        //check the different name options
        switch (_IntObj.name)
        {
            //when we reach for the bed
            case "Col_Bed":
                mRen_Player.enabled = false;
                mRen_LayingPlayer.enabled = true;
                obj_PlayerLight.SetActive(false);

                //if we are done
                if (hasPouredTea && vaseHasFlower) { print("Finished"); hasFinishedTheGame = true; scrpt_UIInfo.UpdateUI(true, "I have lot's to do, but a little time to rest before my big day."); }
                else { scrpt_UIInfo.UpdateUI(true, "This is nice, hot tea and new flowers will make it perfect."); }

                break;
            //when we reach for the cushion seat
            case "Col_Cush":
                mRen_Player.enabled = false;
                mRen_SittingPlayer.enabled = true;
                obj_PlayerLight.SetActive(false);

                //if we are done
                if (hasPouredTea && vaseHasFlower) { print("Finished"); hasFinishedTheGame = true; scrpt_UIInfo.UpdateUI(true, "Okay, I have everything I need to plan for my big day."); }
                else
                { scrpt_UIInfo.UpdateUI(true, "I have a lot to plan for today, but I could use some tea, and maybe fresh flowers to help me think."); }

                break;
            // when we reach for the flowers
            case "flowers":
                //are holding something already
                if (isHoldingObject)
                {
                    if (hasTeapot == true) { scrpt_UIInfo.UpdateUI(true, "I don't need flowers to make tea, just water and some heat."); }
                    if (hasCup == true) { scrpt_UIInfo.UpdateUI(true, "I wonder if this flower could fit in my cup... Hm, I'll have to test it another time."); }
                    if (hasFlowers == true) { scrpt_UIInfo.UpdateUI(true, "Don't be sad my other flowers, I'll come back for you when it's the right time."); }
                    //scrpt_UIInfo.UpdateUI(true, "I would love a drink, but wasn't I doing something?");
                }
                //are not holding something already
                else
                {
                    scrpt_UIInfo.UpdateUI(true, "Oh, this one is perfect!");
                    trans_HoldingObject = _IntObj.transform;
                    trans_HoldingObject.position = trans_PlayerHandR.position;
                    trans_HoldingObject.GetComponent<BoxCollider>().enabled = false;
                    trans_HoldingObject.parent = trans_PlayerHandR;
                    hasFlowers = true;
                    isHoldingObject = true;
                }
                break;
            // when we reach the vase
            case "Col_Vas":
                //are holding something already
                if (isHoldingObject)
                {
                    if (hasTeapot == true) { scrpt_UIInfo.UpdateUI(true, "I think there's still fresh water in the vase, so I don't need anymore."); }
                    if (hasCup == true) { scrpt_UIInfo.UpdateUI(true, "For some reason I feel like I want to drink the vase water... but I definitely shouldn't."); }
                    if (hasFlowers == true)
                    {
                        if (vaseHasFlower) { scrpt_UIInfo.UpdateUI(true, "I guess more flowers won't hurt."); }
                        else { scrpt_UIInfo.UpdateUI(true, "Wow, I picked the best one. The feng shui is perfect for some tea."); }
                        trans_HoldingObject.parent = null; trans_HoldingObject.position = _IntObj.transform.position; hasFlowers = false; vaseHasFlower = true; isHoldingObject = false; trans_HoldingObject = null;
                    }

                }
                //are not holding something already
                else
                { scrpt_UIInfo.UpdateUI(true, "the vase look a little empty, some flowers will be nice!"); }
                break;
            //when we reach for the CUP
            case "Col_Cup":
                //are holding something already
                if (isHoldingObject)
                {
                    if (hasTeapot == true && teapotHasWater == true && teapotHasHeat == true) { hasPouredTea = true; scrpt_UIInfo.UpdateUI(true, "I'm so excited to enjoy my tea!"); } // pour the tea in the cup
                    else if (hasTeapot == true && teapotHasWater == true) { scrpt_UIInfo.UpdateUI(true, "It's a little cold today, so I'll proably heat this up before I drink it"); }
                    else if (hasTeapot == true) { scrpt_UIInfo.UpdateUI(true, "*Immitates pouring hot water with mouth* ... Okay, I'm ready for the real thing!"); }
                    //put down the cup
                    if (hasCup == true) { if (_IntObj.GetComponent<OnMouseOverChange>().theOriginalModel.transform != null) { trans_HoldingObject.transform.parent = null; trans_HoldingObject.position = _IntObj.transform.position; trans_HoldingObject.rotation = _IntObj.transform.rotation; trans_HoldingObject = null; isHoldingObject = false; hasCup = false; } }
                    if (hasFlowers == true) { scrpt_UIInfo.UpdateUI(true, "I wouldn't drink flowers. Well ... yeah I prefer hot tea."); }

                }
                //are not holding something already
                else
                {
                    if (_IntObj.GetComponent<OnMouseOverChange>().theOriginalModel.transform != null) { trans_HoldingObject = _IntObj.GetComponent<OnMouseOverChange>().theOriginalModel.transform; trans_HoldingObject.position = trans_PlayerHandR.position; trans_HoldingObject.parent = trans_PlayerHandR; isHoldingObject = true; hasCup = true; }
                    trans_HoldingObject.transform.position = trans_PlayerHandR.position;
                    trans_HoldingObject.parent = trans_PlayerHandR;
                    hasCup = true;
                    isHoldingObject = true;
                }
                break;
            //when we reach for the tpot
            case "Col_Ketl":
                //are holding something already
                if (isHoldingObject)
                {
                    //put the teapot down
                    if (hasTeapot == true) { if (_IntObj.GetComponent<OnMouseOverChange>().theOriginalModel.transform != null) { trans_HoldingObject.transform.parent = null; trans_HoldingObject.position = _IntObj.transform.position; trans_HoldingObject.rotation = _IntObj.transform.rotation; trans_HoldingObject = null; isHoldingObject = false; hasTeapot = false; } }

                    if (hasCup == true && teapotHasWater == true && teapotHasHeat == true) { hasPouredTea = true; scrpt_UIInfo.UpdateUI(true, "I'm so excited to enjoy my tea"); } // pour the tea in the cup
                    else if (hasCup == true && teapotHasWater == true) { scrpt_UIInfo.UpdateUI(true, "Oops! I forgot to heat up the pot."); }
                    else if (hasCup == true) { scrpt_UIInfo.UpdateUI(true, "Warm tea sounds great right now"); }

                    if (hasFlowers == true) { scrpt_UIInfo.UpdateUI(true, "I see there's some tea, and I don't thik that'll mix well with flowers."); }
                }
                //are not holding something already
                else
                {
                    scrpt_UIInfo.UpdateUI(true, "Looks like there's still tea leaves, just need a few more things for my morning ritual.");
                    if (_IntObj.GetComponent<OnMouseOverChange>().theOriginalModel.transform != null) { print("grabbing kettle"); trans_HoldingObject = _IntObj.GetComponent<OnMouseOverChange>().theOriginalModel.transform; trans_HoldingObject.position = trans_PlayerHandR.position; trans_HoldingObject.parent = trans_PlayerHandR; isHoldingObject = true; hasTeapot = true; }
                    trans_HoldingObject.transform.position = trans_PlayerHandR.position;
                    trans_HoldingObject.parent = trans_PlayerHandR;
                    hasTeapot = true;
                    isHoldingObject = true;
                }
                break;
            // when we reach the oven
            case "Col_Oven":
                //are holding something already
                if (isHoldingObject)
                {
                    if (hasTeapot == true) { if (teapotHasWater) { scrpt_UIInfo.UpdateUI(true, "Oven is on, now I can put the teapot back on the stove"); teapotHasHeat = true; } else {scrpt_UIInfo.UpdateUI(true, "Let me fill this thing with water before I turn up the heat."); }  }
                    if (hasCup == true ) { if (teapotHasWater) { scrpt_UIInfo.UpdateUI(true, "Heat activated! Now I can pour myself a cup."); teapotHasHeat = true; } else { scrpt_UIInfo.UpdateUI(true, "I should put some water in the kettle before I turn the oven on."); } }
                    if (hasFlowers == true) { scrpt_UIInfo.UpdateUI(true, "I probably should put my flower in the oven... Although I'm curious if, ... nah. nope. You're going in the vase."); }
                }
                //are not holding something already
                else
                { scrpt_UIInfo.UpdateUI(true, "I'll turn the oven on so I can make some hot tea. I'll make it low just in case I forget... again."); if (teapotHasWater == true) { teapotHasHeat = true; scrpt_UIInfo.UpdateUI(true, "Yay! Teatime! Let me grab my cup"); } }
                break;
            // when we reach the kitchen sink
            case "Col_Sink":
                //are holding something already
                if (isHoldingObject)
                {
                    if (hasTeapot == true)
                    {
                        if (teapotHasWater) { scrpt_UIInfo.UpdateUI(true, "Good on water, just need to heat this baby up."); } else { teapotHasWater = true; scrpt_UIInfo.UpdateUI(true, "Just enough to make the perfect brew."); }
                        if (teapotHasHeat) { scrpt_UIInfo.UpdateUI(true, "I could pour this out, but that would be a waste"); }                        
                    }

                    if (hasCup == true) { scrpt_UIInfo.UpdateUI(true, "I'd rather drink faucet water after the germs have been cooked. That's one thing I love about hot tea."); }
                    if (hasFlowers == true) { scrpt_UIInfo.UpdateUI(true, "There's fresh water in the vase, I can just drop these in."); }
                }
                //are not holding something already
                else
                { scrpt_UIInfo.UpdateUI(true, "I should grab my kettle teapot, then I could make some nice warm tea after."); }
                break;
            //when we reach for an interactable object we haven't wrote a condition for
            default:
                print("no condition listed for name: " + _IntObj.name);
                scrpt_UIInfo.UpdateUI(true, "Even the developers aren't sure what to do with this " + _IntObj.name);
                break;

        }//end of interactable conditions

    }//end of analyze interactable

}// end of player controller