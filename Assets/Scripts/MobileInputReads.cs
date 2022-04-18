using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <para>
/// The Mobile Input Reads script is designed to track all inputs from mobile devices using the old input system. 
/// On implementation it should read and give print/log feedback of inputs. Ideally all scripts should get information
/// from this script. 
/// </para>
/// </summary>
public class MobileInputReads : MonoBehaviour
{
    //position start and updated of the finger tracking
    public Vector2 initialPosition;
    public Vector2 updatePosition;

    //stored finger tracking (in case)
    public Vector2 initialPositionStored;
    public Vector2 updatePositionStored;

    //active check if we are still touching the screen
    private bool isTouching;
    //direction swiped
    // 0 = none
    // 1 = right
    // 2 = left
    // 3 = up
    // 4 = down
    private int dirSwiped;

    //can delete this, it's just a test
    public GameObject particle;



    // Update is called once per frame
    private void Update()
    {

        //check if touching
        if (Input.touchCount >= 1)
        {
            //print("touching");
            isTouching = true;           
        }
        //else if not touching
        else
        {
            //print("not touching");
            isTouching = false;         
            //store positions
            updatePositionStored = updatePosition;
            initialPositionStored = initialPosition;
            //reset currents
            updatePosition = Vector2.zero;
            initialPosition = Vector2.zero;
        }//end not touching

        //loop the touch points
        foreach (Touch touch in Input.touches)
        {
            //print("touch loop");

            // if the touch phase has started
            if (touch.phase == TouchPhase.Began)
            {
                //print("touch tap / hold");


                initialPosition = touch.position;

                // Construct a ray from the current touch coordinates
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                if (Physics.Raycast(ray))
                {
                    // Create a particle if hit
                    Instantiate(particle, transform.position, transform.rotation);
                }//end of ray

            }//end of touch phase beginning (tap)
            //else we swiped
            else if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                //print("touch moving or stationary");

                updatePosition = touch.position;                
               
            }//end of swiped touch

            //check if touches end
            bool touchEnd = touch.phase == TouchPhase.Ended;
            if (touchEnd)
            {
                print("Bool - Touch End: " + touchEnd);
                SwipeReads();
                break;
            }//end touch end check
               

        }//end of loop touches   



        

    }//end of update


    //function to track the initial position and update position
    //direction swiped
    // 0 = none
    // 1 = right
    // 2 = left
    // 3 = up
    // 4 = down
    private void SwipeReads()
    {
                
        //swip reads
        Vector2 swipeMath= Vector2.zero;

        if (isTouching == true) { swipeMath = initialPosition - updatePosition; }
        else { swipeMath = initialPositionStored - updatePositionStored; }

        //print("Reading Swipes Touch: " + isTouching + "... | SwipeMath: " + swipeMath);

        //horizontal
        if (Mathf.Abs(swipeMath.x) > Mathf.Abs(swipeMath.y))
        {
            //if x is positive
            if (swipeMath.x < 0)
            {
                print("swiped right");
                dirSwiped = 1;
            }//end x pos
             //else x negative
            else
            {
                print("swiped left");
                dirSwiped = 2;
            }//end x neg

        }//end hori
         //vertical
        else
        {
            //if y is positive
            if (swipeMath.y < 0)
            {
                print("swiped up");
                dirSwiped = 3;
            }//end y pos
             //else y negative
            else
            {
                print("swiped down");
                dirSwiped = 4;
            }//end y neg
        }//end verti

    }//end of swipe reads

}//end of mobile input reads script
