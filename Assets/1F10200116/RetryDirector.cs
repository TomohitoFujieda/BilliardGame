using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RetryDirector : MonoBehaviour
{
    GameObject GameDirector;
    GameObject ShotCount;

    // Start is called before the first frame update
    void Start()
    {
        GameDirector = GameObject.Find("GameDirector");
        ShotCount = GameObject.Find("ShotCount");
        SceneManager.MoveGameObjectToScene(GameDirector, SceneManager.GetActiveScene());
        ShotCount.GetComponent<Text>().text = "�Ő��F" + GameDirector.GetComponent<GameDirector>().shotCount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("GameScene");
        }
    }
}
