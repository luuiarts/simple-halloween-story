using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <para>
/// Player animations controls the different animations we can play. It does not control the parameters that creates or reads those conditions.
/// It simply reads the results of the conditions from "Player_Controller"
/// </para>
/// </summary>

[RequireComponent(typeof(Player_Controller))]
public class Player_Animations : MonoBehaviour
{
    //reference to scripts
    private Player_Controller scrpt_PC;


    // a reference to our player's animator
    public Animator animPlayer;

    //current conditions
    private string strIsWalking;

    //start
    private void Start()
    {
        //assign references
        scrpt_PC = transform.GetComponent<Player_Controller>();


        strIsWalking = "isWalking";

    }//end of start


    //update
    private void LateUpdate()
    {

        //check animations
        CheckMoving();


    }//end of late update

    //check for animation changes
    private void CheckMoving()
    {
        //if we are moving then set the animation
        if(scrpt_PC.isWalking == true)
        {
            //set our animation to walk
            animPlayer.SetBool(strIsWalking, true);
        }//end of are movign
        else
        //else if we are not moving
        {
            //set our animation to NOT walk
            animPlayer.SetBool(strIsWalking, false);
        }//end of not moving

    }//end of check moving


}//end of player animation