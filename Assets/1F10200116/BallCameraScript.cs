using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCameraScript : MonoBehaviour
{

    private GameObject BallCamera;
    private GameObject WhiteBall;
    private GameObject BilliardsStick;
    public Vector3 offset;
    public float rotateSpeed = 1.0f;

    void Start()
    {
        BallCamera = GameObject.Find("BallCamera");
        WhiteBall= GameObject.Find("WhiteBall");
        offset = transform.position - WhiteBall.transform.position;
        BilliardsStick = GameObject.Find("BilliardsStickDir");
    }


    void Update()
    {
        if (WhiteBall == null)
        {
            this.WhiteBall = GameObject.Find("WhiteBall");
        }

        //rotateCamera();
    }

    public void rotateCamera()
    {
        if (Input.GetKey(KeyCode.A))
        {
            BallCamera.transform.RotateAround(WhiteBall.transform.position, Vector3.up, -rotateSpeed);
            BilliardsStick.transform.RotateAround(WhiteBall.transform.position, Vector3.up, -rotateSpeed);
        }
        else if (Input.GetKey(KeyCode.D))
        {      
            BallCamera.transform.RotateAround(WhiteBall.transform.position, Vector3.up, rotateSpeed);
            BilliardsStick.transform.RotateAround(WhiteBall.transform.position, Vector3.up, rotateSpeed);
        }

    }
}