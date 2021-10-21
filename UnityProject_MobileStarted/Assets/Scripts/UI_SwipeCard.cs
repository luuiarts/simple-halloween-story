using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <para>
/// UI Swipecard is intended to allow UI to react to swiping on mobile
/// </para>
/// </summary>
public class UI_SwipeCard : MonoBehaviour
{
    public MobileInputReads scrpt_MRI;

    private RectTransform rectStart;
    private Vector3 posRectStart;
    //private Vector3 posStart;


    // checks for if we want to lock the position/direction we want to drag this object.
    // this only applies if we cannot swipe anywhere to move this object
    public bool lockX, lockY;
    [Range(-1f, 1f)]
    public float acceptOffset;
    private float exceedSwipe;

    // if we have buttons or objects that can be swiped anywhere, then we want to read this
    // in the updates function. That, combined with Must Swipe Right will ensure this object 
    // is interacted with
    public bool canSwipeAnywhere;
    public bool mustSwipeRight;

    // Start is called before the first frame update
    private void Start()
    {
        rectStart = transform.GetComponent<RectTransform>();
        posRectStart = rectStart.position;
        
        if(canSwipeAnywhere == true) { lockX = true; lockY = true; }
    }//end start


    //update
    private void Update()
    {
        
        
    }//end update

    private void ReadSwipeAnywhere()
    {

    }


    // while we are dragging
    public void DraggingMe()
    {
        print("currently dragging");
        rectStart.position = scrpt_MRI.updatePosition;

        //if lock x
        if (lockX == true)
        {
            //restrict only the X to our start pos
            rectStart.position = new Vector3(posRectStart.x, rectStart.position.y, rectStart.position.z);
        }//end of lock x
      

        //if lock y
        if (lockY == true)
        {
            //restrict only the Y to our start pos
            rectStart.position = new Vector3(rectStart.position.x, posRectStart.y, rectStart.position.z);
        }//end of lock y
      


    }// end of while dragging

    public void DragStop()
    {
        print("let go of UI");

        //option 1 is to go right
        if(exceedSwipe > acceptOffset) { print("exceed right"); }
        //option 2 is to go left
        else
        if (exceedSwipe < acceptOffset) { print("exceed left"); }
        //option 3 is to reset/lerp back to center
        else { rectStart.position = posRectStart; }
        

    }//end of exit drag


}// end of UI swipecard
