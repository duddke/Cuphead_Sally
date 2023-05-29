using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YA_HartMoving : MonoBehaviour
{
    //태어나는 순간 타겟 방향이 앞
    //중앙에서 가다가 위 아래로
    Transform target;
    //스크린 x 저장값
    float xScreen;
    //스크린 y 저장값
    float yScreen;
    //방향
    Vector3 dir;
    //오른쪽으로 갈지 왼쪽으로 갈지 불값으로 확인해서 일정성 갖게 하기
    bool right;

    // 패링 타이밍 (영수)
    float currentTime;
    bool b_parring = false;
    //

    public enum State
    {
        Move,
        Up,
        Down
    }
    public State state;

    // Start is called before the first frame update
    void Start()
    {
        yScreen = Camera.main.orthographicSize;
        xScreen = yScreen * Camera.main.aspect;
        target = GameObject.Find("Player").transform;
        if (target.position.x < transform.position.x)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            dir = Vector3.left;
            right = true;
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            dir = Vector3.right;
            right = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Move:
                UpdateMove();
                break;
            case State.Up:
                UpdateUp();
                break;
            case State.Down:
                UpdateDown();
                break;
        }

        // 패링(영수)
        Parring();
        //
    }

    public float currTime;
    float moveTime = 0.5f;
    public float speed = 20;

    Vector3 tr;
    Vector3 tdir;
    Vector3 td;
    private void UpdateMove()
    {
        currTime += Time.deltaTime;
        transform.position += dir * speed * Time.deltaTime;
        if (currTime >= moveTime)
        {
            //currTime = 0;
            //만약 타겟보다 x좌표가 크다면-
            if (right)
            {
                tr = transform.position + new Vector3(-3, yScreen, 0);
            }
            //아니라면 +
            else
            {
                tr = transform.position + new Vector3(3, yScreen, 0);
            }
            tr.y = Mathf.Clamp(tr.y, 3, yScreen * 2 - 5);
            state = State.Up;
        }
    }
    private void UpdateUp()
    {
        MoveH();
        if (transform.position.y >= td.y)//왜 업이 진행되기도 전에 다운이 진행될까? 다운이 되는 조건이 달성되어서?? 왜 그렇게 빨리 달성되는 것일까>??
        {
            if (right)
            {
                tr = transform.position - new Vector3(3, yScreen * 2, 0);
            }
            //아니라면 +
            else
            {
                tr = transform.position + new Vector3(3, -yScreen * 2, 0);
            }
            tr.y = Mathf.Clamp(tr.y, 3, yScreen * 2 - 5);
            state = State.Down;
        }
    }

    private void UpdateDown()
    {
        MoveH();
        if (transform.position.y <= td.y)
        {
            if (right)
            {
                tr = transform.position + new Vector3(-3, yScreen * 2, 0);
            }
            //아니라면 +
            else
            {
                tr = transform.position + new Vector3(3, yScreen * 2, 0);
            }
            tr.y = Mathf.Clamp(tr.y, 3, yScreen * 2 - 5);
            state = State.Up;
        }
    }

    public void MoveH()
    {
        td = tr;
        tdir = tr - transform.position;
        tdir.Normalize();
        transform.position += tdir * speed * Time.deltaTime;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            b_parring = true;
        }
    }

    // 패링 (영수)
    void Parring()
    {
        if (b_parring == true)
        {
            currentTime += Time.deltaTime;

            // 패링 타이밍
            if (currentTime <= 0.1f)
            {
                if(Input.GetKeyDown(KeyCode.Space))
                {
                    Destroy(gameObject);
                    currentTime = 0;
                    b_parring = false;
                }
            }
            else
            {
                YS_PlayerHealth.Instance.HP--;
                Destroy(gameObject);
                currentTime = 0;
                b_parring = false;
            }
        }
    }
    //
}
