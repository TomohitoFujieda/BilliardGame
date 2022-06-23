using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteBallController : MonoBehaviour
{
    Vector3 pushForce;
    Vector2 startPos;
    GameObject whiteBallGenerator;
    float velocityX;
    float velocityZ;


    public void pushBall(Vector3 dir)
    {
        GetComponent<Rigidbody>().velocity = dir;
    }

    void Start()
    {
        whiteBallGenerator = GameObject.Find("WhiteBallGenerator");
    }
    
    void Update()
    {
        velocityX = GetComponent<Rigidbody>().velocity.x;
        velocityZ = GetComponent<Rigidbody>().velocity.z;

        if (transform.position.y < -10)
        {
            whiteBallGenerator.GetComponent<WhiteBallGenerator>().GenerateWhiteBall();
            Destroy(this.gameObject);
        }

        if (new Vector3(velocityX, 0, velocityZ).magnitude < 0.2f)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        }
    }
}
