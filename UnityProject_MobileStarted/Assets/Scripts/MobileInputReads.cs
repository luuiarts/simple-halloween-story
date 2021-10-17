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

    private Vector2 initialPosition;

    public GameObject particle;


    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        //loop the touch points
        foreach (Touch touch in Input.touches)
        {
            print("touch");

            // if the touch phase has started
            if (touch.phase == TouchPhase.Began)
            {
                print("touch tap / hold");


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
                print("touch swiped");

                // get the moved direction compared to the initial touch position
                var direction = touch.position - initialPosition;

                // get the signed x direction
                // if(direction.x >= 0) 1 else -1
                var signedDirection = Mathf.Sign(direction.x);

                print("direction swipe: " + direction + " ... signDir: " + signedDirection);
               
            }//end of swiped

        }//end of loop touches
      

    }//end of update


}//end of mobile input reads script
