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
            if (touch.phase == TouchPhase.Began)
            {
                // Construct a ray from the current touch coordinates
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                if (Physics.Raycast(ray))
                {
                    // Create a particle if hit
                    Instantiate(particle, transform.position, transform.rotation);
                }
            }
        }//end of loop touches

    }//end of update


}//end of mobile input reads script
