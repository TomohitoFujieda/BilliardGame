using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class GameDirector : MonoBehaviour
{
    int count;
    public int shotCount;
    bool[] destroyedBalls;
    bool doesStopAllBalls;
    bool doesActiveBallCamera;
    bool indicatorGoBack;
    bool shootingBall;
    bool movedScene;
    GameObject AboveCamera;
    GameObject BallCamera;
    GameObject WhiteBall;
    GameObject[] Balls;
    GameObject ShotUI;
    GameObject AboveCameraUI;
    GameObject BallCameraUI;
    GameObject Indicator;
    GameObject BilliardsStickDir;
    float timeDelta;
    float indicatorPositionX;
    Vector3 CamOffset;
    Vector3 CamPos;
    Vector3 WhiteBallPos;
    Vector3 WhiteBallPushVec;


    public void decreaseCount(string ball)
    {
        this.count--;
        int ballNum = Int32.Parse(ball[4].ToString());
        destroyedBalls[ballNum - 1] = true;
    }

    IEnumerator PlayAnim()
    {
        BilliardsStickDir.GetComponent<Animator>().Play("StickAnimation");
        yield return new WaitForSeconds(0.5f);
        WhiteBall.GetComponent<WhiteBallController>().pushBall(WhiteBallPushVec * (indicatorPositionX + 300) / 2);
        shootingBall = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        this.count = 6;
        AboveCamera = GameObject.Find("Main Camera");
        BallCamera = GameObject.Find("BallCamera");
        WhiteBall = GameObject.Find("WhiteBall");
        ShotUI = GameObject.Find("ShotUI");
        AboveCameraUI = GameObject.Find("AboveCameraUI");
        BallCameraUI = GameObject.Find("BallCameraUI");
        Indicator = GameObject.Find("Indicator");
        BilliardsStickDir = GameObject.Find("BilliardsStickDir");
        Balls = new GameObject[6];
        destroyedBalls = new bool[6];
        for (int i = 0; i < 6; i++)
        {
            Balls[i] = GameObject.Find("Ball" + (i + 1).ToString());
            destroyedBalls[i] = false;
        }
        CamOffset = BallCamera.transform.position - WhiteBall.transform.position;
        doesStopAllBalls = true;
        doesActiveBallCamera = false;
        indicatorGoBack = false;
        ShotUI.SetActive(false);
        BallCameraUI.SetActive(false);
        timeDelta = 1.0f;
        indicatorPositionX = -250;
        shootingBall = false;
        movedScene = false;
        shotCount = 0;
        AboveCamera.GetComponent<AudioListener>().enabled = true;
        BallCamera.GetComponent<AudioListener>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (!movedScene)
        {
            if (doesActiveBallCamera)
            {
                BallCamera.GetComponent<BallCameraScript>().rotateCamera();
            }

            if (WhiteBall == null)
            {
                WhiteBall = GameObject.Find("WhiteBall");
            }

            for (int i = 0; i < 7; i++)
            {
                if (i < 6 && !destroyedBalls[i])
                {
                    if (Balls[i].GetComponent<Rigidbody>().velocity.magnitude > 0.2f)
                    {
                        doesStopAllBalls = false;
                        break;
                    }
                }
                else
                {
                    if (WhiteBall.GetComponent<Rigidbody>().velocity.magnitude > 0.2f)
                    {
                        doesStopAllBalls = false;
                        break;
                    }
                    else
                    {
                        doesStopAllBalls = true;
                    }
                }
            }

            if (doesStopAllBalls && !shootingBall)
            {
                if (!AboveCameraUI.activeSelf && !BallCameraUI.activeSelf && !ShotUI.activeSelf)
                {
                    AboveCameraUI.SetActive(true);
                }

                if (count == 0)
                {
                    movedScene = true;
                    SceneManager.LoadScene("RetryScene");
                }

                if (Input.GetKeyDown(KeyCode.Space) && !doesActiveBallCamera)
                {
                    BallCamera.transform.position = WhiteBall.transform.position + CamOffset;
                    BallCamera.transform.rotation = Quaternion.Euler(30.0f, 90.0f, 0);
                    BilliardsStickDir.GetComponent<BilliardsStickController>().initStickPos();
                    BallCamera.SetActive(true);
                    AboveCamera.SetActive(false);
                    BallCameraUI.SetActive(true);
                    AboveCameraUI.SetActive(false);
                    doesActiveBallCamera = true;
                    AboveCamera.GetComponent<AudioListener>().enabled = false;
                    BallCamera.GetComponent<AudioListener>().enabled = true;
                }

                if (Input.GetKeyDown(KeyCode.LeftControl) && doesActiveBallCamera)
                {
                    BallCamera.SetActive(false);
                    AboveCamera.SetActive(true);
                    BallCameraUI.SetActive(false);
                    AboveCameraUI.SetActive(true);
                    doesActiveBallCamera = false;
                    AboveCamera.GetComponent<AudioListener>().enabled = true;
                    BallCamera.GetComponent<AudioListener>().enabled = false;
                }

            }

            if (doesActiveBallCamera && Input.GetMouseButton(0))
            {
                Debug.Log(indicatorPositionX);
                Debug.Log(indicatorGoBack);
                ShotUI.SetActive(true);
                BallCameraUI.SetActive(false);
                shootingBall = true;
                if(timeDelta < 1.0f)
                {
                    timeDelta = 1.0f;
                }

                if (indicatorGoBack)
                {
                    timeDelta -= 0.5f;
                    indicatorPositionX -= 10 * timeDelta;

                }
                else
                {
                    timeDelta += 0.5f;
                    indicatorPositionX += 10 * timeDelta;
                }
                if (indicatorPositionX >= 250)
                {
                    indicatorPositionX = 250;
                    indicatorGoBack = true;
                }
                else if (indicatorPositionX <= -250)
                {
                    indicatorPositionX = -250;
                    indicatorGoBack = false;
                }
                Indicator.GetComponent<RectTransform>().localPosition = new Vector3(indicatorPositionX, -180, 0);
            }
            else if (doesActiveBallCamera && Input.GetMouseButtonUp(0))
            {
                shotCount++;
                ShotUI.SetActive(false);
                CamPos = new Vector3(BallCamera.transform.position.x, 0, BallCamera.transform.position.z);
                WhiteBallPos = new Vector3(WhiteBall.transform.position.x, 0, WhiteBall.transform.position.z);
                WhiteBallPushVec = (WhiteBallPos - CamPos).normalized;
                StartCoroutine("PlayAnim");
                indicatorPositionX = -250;
                timeDelta = 1.0f;
                AboveCamera.GetComponent<AudioListener>().enabled = true;
                BallCamera.GetComponent<AudioListener>().enabled = false;
                BallCamera.SetActive(false);
                AboveCamera.SetActive(true);
                doesActiveBallCamera = false;
            }
        }
    }
}