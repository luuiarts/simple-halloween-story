using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform trans_Cam;
    public float offsetPosition;
    public Transform trans_Player;

    // Start is called before the first frame update
    void Start()
    {
        offsetPosition = Mathf.Abs(trans_Cam.position.z) - Mathf.Abs(trans_Player.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        trans_Cam.position = new Vector3(trans_Cam.position.x, trans_Cam.position.y, trans_Player.position.z - offsetPosition);
        trans_Cam.LookAt(trans_Player);

        offsetPosition += Input.mouseScrollDelta.y;

        if (offsetPosition > 13) { offsetPosition = 13; }
        if(offsetPosition < 2.5) { offsetPosition = 2.5f; }
    }
}
