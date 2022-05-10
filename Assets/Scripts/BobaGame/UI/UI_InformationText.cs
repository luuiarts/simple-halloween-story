using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UI Info Text should handle all the information we want to display on SCREEN
/// </summary>

public class UI_InformationText : MonoBehaviour
{
    public Text uiTxt_Info;
    public Image uiImg_Background;


    public void UpdateUI(bool _ImgVisible, string _TxtInfo)
    {
        uiImg_Background.enabled = _ImgVisible;
        uiTxt_Info.text = _TxtInfo;

    }//end of update UI


}//end of ui info text script