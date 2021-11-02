using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMouse : MonoBehaviour
{
    public float sensitivity;

    void FixedUpdate()
    {
        float rotateHorizontal = Input.GetAxis("Mouse X");
        float rotateVertical = Input.GetAxis("Mouse Y");

        //transform.Rotate(-transform.up * rotateHorizontal * sensitivity);
        //transform.Rotate(transform.right * rotateVertical * -sensitivity);
        transform.RotateAround(GameManager.scriptPlayer.transform.position, -Vector3.up, rotateHorizontal * sensitivity); //use transform.Rotate(-transform.up * rotateHorizontal * sensitivity) instead if you dont want the camera to rotate around the player
        transform.RotateAround(Vector3.zero, transform.right, rotateVertical * sensitivity); // again, use transform.Rotate(transform.right * rotateVertical * sensitivity) if you don't want the camera to rotate around the player
    }

}
