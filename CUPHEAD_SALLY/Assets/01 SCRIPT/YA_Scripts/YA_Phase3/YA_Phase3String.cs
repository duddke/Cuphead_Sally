using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YA_Phase3String : MonoBehaviour
{
    public static YA_Phase3String Instance;
    //글씨 생성시 밑으로 내려갔다가 1.5초 뒤 올라가기
    //트루가 되면 해당 오브젝트 켜기

    //메테오
    public bool meteo;
    public GameObject meteostr;
    enum String
    {
        Down,
        Stop,
        Up
    }
    String S_state = String.Down;

    //파도
    public bool bigwave;
    public GameObject bigwavestr;

    //번개
    public bool thunder;
    public GameObject thunderstr;

    //스크린 x 저장값
    float xScreen;
    //스크린 y 저장값
    float yScreen;


    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        yScreen = Camera.main.orthographicSize * 2;
        xScreen = yScreen * Camera.main.aspect;
    }

    // Update is called once per frame
    void Update()
    {
        if(meteo)
        {
            meteostr.SetActive(true);
            bigwavestr.SetActive(false);
            thunderstr.SetActive(false);
        }
        else if(bigwave)
        {
            meteostr.SetActive(false);
            bigwavestr.SetActive(true);
            thunderstr.SetActive(false);
        }
        else if(thunder)
        {
            meteostr.SetActive(false);
            bigwavestr.SetActive(false);
            thunderstr.SetActive(true);
        }
        switch (S_state)
        {
            case String.Down:
                Down();
                break;
            case String.Stop:
                Stop();
                break;
            case String.Up:
                Up();
                break;
        }
    }

    //메테오 글씨 내려가기
    float speed = 8;
    private void Down()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;
        if(transform.position.y<=yScreen-3)
        {
            S_state = String.Stop;
        }
    }

    //1.5머무르기
    float currentTime;
    public float dilTime = 1.5f;
    private void Stop()
    {
        currentTime += Time.deltaTime;
        if(currentTime>=dilTime)
        {
            currentTime = 0;
            S_state = String.Up;
        }
    }

    //올라가기
    //행성 실행
    public bool upend = false;
    private void Up()
    {
        transform.position += Vector3.up * speed * Time.deltaTime;
        if (transform.position.y >= yScreen + 3)
        {
            S_state = String.Down;
            upend = true;
        }
    }
}
