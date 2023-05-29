using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YA_Phase3String : MonoBehaviour
{
    public static YA_Phase3String Instance;
    //�۾� ������ ������ �������ٰ� 1.5�� �� �ö󰡱�
    //Ʈ�簡 �Ǹ� �ش� ������Ʈ �ѱ�

    //���׿�
    public bool meteo;
    public GameObject meteostr;
    enum String
    {
        Down,
        Stop,
        Up
    }
    String S_state = String.Down;

    //�ĵ�
    public bool bigwave;
    public GameObject bigwavestr;

    //����
    public bool thunder;
    public GameObject thunderstr;

    //��ũ�� x ���尪
    float xScreen;
    //��ũ�� y ���尪
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

    //���׿� �۾� ��������
    float speed = 8;
    private void Down()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;
        if(transform.position.y<=yScreen-3)
        {
            S_state = String.Stop;
        }
    }

    //1.5�ӹ�����
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

    //�ö󰡱�
    //�༺ ����
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
