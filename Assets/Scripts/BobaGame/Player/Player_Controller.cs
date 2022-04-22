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
    // a reference to the navmesh agent we want to move (player)
    [Tooltip(" a reference to the navmesh agent we want to move (player)")]
    public NavMeshAgent agentPlayer;

    //a reference to our goal object we clicked on
    private GameObject obj_TempGoal;
    //a reference to our position data we clicked on (doesnt require goal object)
    private Transform trans_TempGoal;

    // a bool for picking finished seeds (if we have the basket)
    [HideInInspector]
    public bool isCarryingBasket;
    // a reference to the basket
    [HideInInspector]
    public GameObject obj_Basket;
    // a bool for picking up the boba cup
    [HideInInspector]
    public bool isCarryingCup;
    // a reference to the boba cup
    [HideInInspector]
    public GameObject obj_Cup;

    // references to the animation states
    [HideInInspector]
    public bool isWalking;

    //start
    private void Start()
    {
        //set vars
        canMove = true;
        isWalking = false;
        
    }//end of start


    //update
    private void Update()
    {
        //check if we have inputs
        CheckMouseButtons();

        //check goal
        WaitingToReachGoal();
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
                if(canMove == true)
                {
                    //send the agent to that location
                    agentPlayer.SetDestination(hitInfo.point);
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
        if(trans_TempGoal == null) { return; }


        // Check if we've reached the destination
        if (!agentPlayer.pathPending)
        {
           
            //if we have no distance left to travel
            if (agentPlayer.remainingDistance <= agentPlayer.stoppingDistance)
            {
                //set walking to no
                isWalking = false;

                //if we have no path
                if (!agentPlayer.hasPath || agentPlayer.velocity.sqrMagnitude == 0f)
                {
                    // Done

                    trans_TempGoal = null;

                    //if we have no object goal then return
                    if (obj_TempGoal == null) { return; }

                    //if it's the basket
                    if (obj_TempGoal.tag == "Finish" || obj_TempGoal.tag == "Basket")
                    {
                        //if we dont have the cup
                        if(isCarryingCup == false)
                        {
                            //assign the basket ref
                            obj_Basket = obj_TempGoal;
                            //call the scrip on the object
                            //isCarryingBasket = obj_Basket.transform.GetComponent<Basket_Manager>().ThisObjectTriggered(isCarryingBasket, agentPlayer.gameObject);
                            //clear our goal
                            obj_TempGoal = null;
                        }//end of dont have the cup   
                        else { print("Cant pickup because carrying cup"); }

                    }//end of basket
                    else //if it's the boba cup
                    if (obj_TempGoal.tag == "Cup")
                    {
                        //if we dont have the basket
                        if (isCarryingBasket == false)
                        {
                            //assign the basket ref
                            obj_Cup = obj_TempGoal;
                            //call the scrip on the object
                            //isCarryingCup = obj_Cup.GetComponent<Cup_Manager>().ThisObjectTriggered(isCarryingCup, agentPlayer.gameObject);
                            //clear our goal
                            obj_TempGoal = null;

                        }//end of dont have the basket
                        else { print("Cant pickup because carrying basket"); }

                    }//end of boba cup
                    else //if it's the kitchen basket
                    if (obj_TempGoal.tag == "Respawn" || obj_TempGoal.tag == "KitchenBasket")
                    {
                        //if we have the basket
                        if(isCarryingBasket == true)
                        {
                            //ref the kitchenbasket script
                            //Inventory_Manager scrpt_IM = obj_TempGoal.transform.GetComponent<Inventory_Manager>();
                            //ref the basket script
                            //Basket_Manager scrpt_BM = obj_Basket.GetComponent<Basket_Manager>();
                            //track res #
                            int id = 0;

                            ////for each resource we have in the basket
                            //foreach (string _res in scrpt_BM.res_Plantables)
                            //{
                            //    //if it matches our kitchen basket res
                            //    if (_res == scrpt_IM.myResource)
                            //    {
                            //        //if we have any
                            //        if (scrpt_BM.res_NumberAvailable[id] > 0)
                            //        {
                            //            //log it
                            //            print("Gave: " + scrpt_BM.res_NumberAvailable[id] + " of '" + _res + "' - to kitchen basket");
                            //            //call the function to give it
                            //            //scrpt_BM.res_NumberAvailable[id] += scrpt_IM.GiveTake(true, obj_Basket, _res, scrpt_BM.res_NumberAvailable[id]);
                            //            //loop the resource children in the basket
                            //            //for each child in the basket
                            //            foreach (Transform _child in obj_Basket.transform) //****************************FIX this does not make all of the leafs go inside the basket, just 1 model
                            //            {
                            //                //if the child has a child
                            //                if (_child.transform.childCount != 0)
                            //                {                                                
                            //                    //if the first child tag matches
                            //                    if (_child.GetChild(0).tag == _res)
                            //                    {
                            //                        //say we cannot move
                            //                        canMove = false;

                            //                        //loop each resource we picked
                            //                        foreach (Transform _resChildren in _child)
                            //                        {
                            //                            //unassign parent
                            //                            _resChildren.transform.parent = null;
                            //                            //add the script for the animation
                            //                            //Animation_DroppingOffResources scrpt_AD = _resChildren.gameObject.AddComponent<Animation_DroppingOffResources>();
                            //                            //assign a start
                            //                            scrpt_AD.trans_Start = _resChildren;
                            //                            //assign a goal
                            //                            scrpt_AD.trans_DepositTarget = obj_TempGoal.transform; //*********************This will need to be changed to the slots inside the basket
                            //                            //call function
                            //                            scrpt_AD.AnimateDeposit();
                            //                            //assign parent
                            //                            _resChildren.transform.parent = obj_TempGoal.transform;
                            //                        }//end of loop picked res

                            //                         //say we can move
                            //                        canMove = true;
                            //                    }//end of transform.tag                          

                            //                }//end of does have a child

                            //            }//end of loop basket

                            //        }//end of have any
                            //        else
                            //        //else we dont have any
                            //        {
                            //            //log it
                            //            print("we dont have any : " + _res);
                            //        }//end of dont have any

                            //    }//end of matches resource

                            //    id++;
                            //}//end of loop basket

                        }//end of is carrying the basket
                        else
                        //if we are not carrying the basket *******Boba cup condition, and none condition
                        {
                            print("no basket and no boba cup"); 

                        }//end of not carrying basket
                                              
                        //clear our goal
                        obj_TempGoal = null;

                    }//end of kitchen basket
                    else
                    //its a plant
                    {
                        //call the scrip on the object
                        //obj_TempGoal.transform.GetComponent<Planting_Instance>().ThisObjectTriggered(obj_Basket, transform.gameObject);
                        //dont clear our goal so the player can choose to wait at the planting spot for it to finish
                        //obj_TempGoal = null;
                    }//end of its a plant

                   

                }//end of no path

            }//end of no remaining distance
            else
            //else if we have not reached our goal then we are moving
            {
                //set walking to be true
                isWalking = true;

            }//end of have not reached our goal

        }//end of if we reached our destination      


    }//end of waiting to reach goal


}// end of player controller