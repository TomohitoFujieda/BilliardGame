using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BilliardsStickController : MonoBehaviour
{
    GameObject WhiteBall;
    GameObject BilliardsStick;
    Vector3 offset;

    public void initStickPos()
    {
        transform.position = WhiteBall.transform.position + offset;
        transform.rotation = Quaternion.AngleAxis(-95f, new Vector3(0, 0, 1));
    } 

    // Start is called before the first frame update
    void Start()
    {
        WhiteBall = GameObject.Find("WhiteBall");
        BilliardsStick = GameObject.Find("BilliardsStick");
        transform.rotation = Quaternion.Euler(0f, 0f, -95f);
        transform.position = WhiteBall.transform.position + new Vector3(-12, 1, 0);
        offset = transform.position - WhiteBall.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (WhiteBall == null)
        {
            WhiteBall = GameObject.Find("WhiteBall");
        }
    }
}
