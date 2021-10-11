using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroCamera : MonoBehaviour
{
    public Gyroscope gyro;
    public float x, y, z, w;

    public Vector3 offset;
    private Quaternion gyroQuaternion;
    private Vector3 gyroEuler;

    private void Start()
    {
        gyro = Input.gyro;
        gyro.enabled = true;
    }

    private void Update()
    {
        //gyroQuaternion = new Quaternion(gyro.attitude.z, gyro.attitude.y, -gyro.attitude.x, -gyro.attitude.w);
        //gyroQuaternion = new Quaternion(gyro.attitude.x + offsetX, gyro.attitude.y + offsetY, -gyro.attitude.z + offsetZ, -gyro.attitude.w + offsetW);
        gyroQuaternion = new Quaternion(gyro.attitude.x, gyro.attitude.y, gyro.attitude.z, gyro.attitude.w) * new Quaternion (x,y,z,w);

        gyroEuler = gyroQuaternion.eulerAngles;

        gyroEuler += offset;

        transform.rotation = Quaternion.Euler(gyroEuler + offset);


    }


}
