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
    private Vector3 posStart;


    // Start is called before the first frame update
    void Start()
    {
        rectStart = transform.GetComponent<RectTransform>();
        posRectStart = rectStart.position;
        posStart = transform.position;
    }

    // while we are dragging
    public void DraggingMe()
    {
        print("currently dragging");

        rectStart.position = scrpt_MRI.initialPosition;

    }// end of while dragging

    public void DragStop()
    {
        print("let go of UI");

        //option 1 is to go right

        //option 2 is to go left

        //option 3 is to reset/lerp back to center
        rectStart.position = posRectStart;

    }//end of exit drag


}// end of UI swipecard
