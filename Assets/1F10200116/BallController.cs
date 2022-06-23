using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    GameObject gameDirector;
    float velocityX;
    float velocityZ;

    // Start is called before the first frame update
    void Start()
    {
        gameDirector = GameObject.Find("GameDirector");
    }

    // Update is called once per frame
    void Update()
    {
        velocityX = GetComponent<Rigidbody>().velocity.x;
        velocityZ = GetComponent<Rigidbody>().velocity.z;

        if (transform.position.y < -10)
        {
            gameDirector.GetComponent<GameDirector>().decreaseCount(this.gameObject.name);
            Destroy(this.gameObject);
        }

        if (new Vector3(velocityX, 0, velocityZ).magnitude < 0.5f)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        }
    }
}
