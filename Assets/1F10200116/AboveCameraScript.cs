using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AboveCameraScript : MonoBehaviour
{
    private GameObject Field;
    private GameObject AboveCamera;
    GameObject BallCamera;
    private GameObject WhiteBall;

    // Start is called before the first frame update
    void Start()
    {
        this.Field = GameObject.Find("Field");
        this.AboveCamera = GameObject.Find("Main Camera");
        this.BallCamera = GameObject.Find("BallCamera");
        this.WhiteBall = GameObject.Find("WhiteBall");
    }

    // Update is called once per frame
    void Update()
    {
        if (WhiteBall == null)
        {
            this.WhiteBall = GameObject.Find("WhiteBall");
        }

        if (!Input.GetMouseButtonDown(0) || !Input.GetMouseButtonUp(0) || !Input.GetMouseButton(0))
        {
            if (Input.GetKey(KeyCode.A))
            {
                transform.RotateAround(Field.transform.position, Vector3.up, 1f);
            } 
            else if (Input.GetKey(KeyCode.D))
            {
                transform.RotateAround(Field.transform.position, Vector3.up, -1f);
            }
        }
    }
}
