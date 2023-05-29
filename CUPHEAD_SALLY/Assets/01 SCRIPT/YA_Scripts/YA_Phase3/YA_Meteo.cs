using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YA_Meteo : MonoBehaviour
{

    

    //õ�忡�� ���߱�
    //�ĵ��� x�� �������� ���� ��
    //���׿� ������Ʈ ����

    public enum State
    {
        Down,
        Ground,
        Stay,
        Up
    }
    public State state;

    //��ũ�� x ���尪
    float xScreen;
    //��ũ�� y ���尪
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


    //�༺ ��������(������ �Ʒ���)
    public float speed = 8;
    private void UpDown()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;
        if(transform.position.y<=5)
        {
            state = State.Ground;
        }
    }

    //�׶������
    //2�� �� //�ĵ�ȣ�� �Լ� ������(������ 3�� �ĵ��� ��ȯ�ϴ�)
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

    //�ö󰡱�
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
