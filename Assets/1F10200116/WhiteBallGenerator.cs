using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteBallGenerator : MonoBehaviour
{

    public GameObject WhiteBallPrefab;

    public void GenerateWhiteBall()
    {
        GameObject go = Instantiate(WhiteBallPrefab);
        go.transform.position = new Vector3(-4.0f, -0.2f, 0f);
        go.name = "WhiteBall";
    }
 
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
