using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleController : MonoBehaviour
{
    GameObject gameDirector;
    GameObject whiteBall;
    GameObject whiteBallGenerator;

    void OnTriggerEnter(Collider other)
    {
        other.GetComponent<ParticleSystem>().Play();
        if (other.gameObject == whiteBall)
        {
            whiteBallGenerator.GetComponent<WhiteBallGenerator>().GenerateWhiteBall();
            Destroy(other.gameObject);
        }
        else
        {
            gameDirector.GetComponent<GameDirector>().decreaseCount(other.gameObject.name);
            Destroy(other.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        gameDirector = GameObject.Find("GameDirector");
        whiteBall = GameObject.Find("WhiteBall");
        whiteBallGenerator = GameObject.Find("WhiteBallGenerator");
    }

    // Update is called once per frame
    void Update()
    {
        if (whiteBall == null)
        {
            whiteBall = GameObject.Find("WhiteBall");
        }
    }
}
