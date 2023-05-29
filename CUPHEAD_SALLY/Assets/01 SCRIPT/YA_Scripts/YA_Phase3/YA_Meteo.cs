using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YA_Meteo : MonoBehaviour
{

    

    //천장에서 멈추기
    //파도와 x가 같아지면 위로 슝
    //메테오 오브젝트 끄기

    public enum State
    {
        Down,
        Ground,
        Stay,
        Up
    }
    public State state;

    //스크린 x 저장값
    float xScreen;
    //스크린 y 저장값
    float yScreen;

    // Start is called before the first frame update
    void Start()
    {
        yScreen = Camera.main.orthographicSize * 2;
        xScreen = yScreen * Camera.main.aspect;
        YA_EnemyPhase3.Instance.phaseEnd += () => { Destroy(gameObject); };
    }


    // Update is called once per frame
    void Update()
    {
        switch(state)
        {
            case State.Down:
                UpDown();
                break;
            case State.Ground:
                UpGround();
                break;
            case State.Stay:
                UpStay();
                break;
            case State.Up:
                UpUp();
                break;
        }
        /*currentTime += Time.deltaTime;
        if(currentTime>=creTime)
        {
            YA_EnemyPhase3.Instance.meteoEnd = true;
            currentTime = 0;
        }*/
    }


    //행성 내려오기(위에서 아래로)
    public float speed = 8;
    private void UpDown()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;
        if(transform.position.y<=5)
        {
            state = State.Ground;
        }
    }

    //그라운드까지
    //2초 후 //파도호출 함수 보내기(페이즈 3을 파도로 전환하는)
    float currentTime=0;
    public float upTime=2;
    public float bigTime = 2;

    private void UpGround()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= bigTime)
        {
            YA_EnemyPhase3.Instance.meteoEnd = true;
            if (currentTime >= upTime)
            {
                currentTime = 0;
                state = State.Up;
            }
        }
    }

    public bool up;
    public static bool bigwave;
    private void UpStay()
    {
        if(bigwave)
        {
            up = true;
            bigwave = false;
            state = State.Up;
        }
    }

    //올라가기
    private void UpUp()
    {
        transform.position += Vector3.up * speed * Time.deltaTime;
        if(!up && transform.position.y >= yScreen-10)
        {
            state = State.Stay;
        }
        if (up && transform.position.y >= yScreen)
        {
            up = false;
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        YA_EnemyPhase3.Instance.phaseEnd -= () => { Destroy(gameObject); };
    }
}
