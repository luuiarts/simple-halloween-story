using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnMouseOverChange : MonoBehaviour
{
    public MeshRenderer mRen_HighlightedMesh;
    public GameObject theOriginalModel;


    private void Start()
    {
        if (mRen_HighlightedMesh == null) { return; }
        mRen_HighlightedMesh.enabled = false;
    }

    private void OnMouseOver()
    {
        if(mRen_HighlightedMesh == null) { return; }
        //If your mouse hovers over the GameObject with the script attached, output this message
        //Debug.Log("Mouse is over GameObject.");
        mRen_HighlightedMesh.enabled = true;
    }

    private void OnMouseExit()
    {
        if (mRen_HighlightedMesh == null) { return; }
        //The mouse is no longer hovering over the GameObject so output this message each frame
        //Debug.Log("Mouse is no longer on GameObject.");
        mRen_HighlightedMesh.enabled = false;
    }


}//end of onmouseoverchange
