using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YA_SallyMove : MonoBehaviour
{

    public enum State
    {
        Down,
        MoveRight,
        MoveLeft,
        Die
    }
    public State m_state = State.Down;

    //스크린 x 저장값
    float xScreen;
    //스크린 y 저장값
    float yScreen;

    // Start is called before the first frame update
    void Start()
    {
        yScreen = Camera.main.orthographicSize * 2;
        xScreen = yScreen * Camera.main.aspect;
        transform.position = new Vector3(transform.position.x,yScreen+2,0);
    }

    // Update is called once per frame
    void Update()
    {
        if(YA_EnemyHP.Instance.hp<=0)
        {
            m_state = State.Die;
        }
        switch (m_state)
        {
            case State.Down:
                UpDown();
                break;
            case State.MoveRight:
                UpMoveRight();
                break;
            case State.MoveLeft:
                UpMoveLeft();
                break;
            case State.Die:
                UpDie();
                break;
        }
    }

    //에너미가 왼쪽 위에서 천천히 내려온 뒤
    public float downSpeed = 2;
    private void UpDown()
    {
        transform.position += Vector3.down * downSpeed * Time.deltaTime;
    //특정 위치에 멈춰서
        if(transform.position.y<=yScreen-5)
        {
            m_state = State.MoveRight;
        }
    }

    // 좌우로 러프 이동한다
    // 이때 좌우 범위 값은 스크린 x값을 기반으로 한다
    public float moveLerp = 0.5f;
    Vector3 dd;
    private void UpMoveRight()
    {
        dd = transform.position;
        dd.x = xScreen * 0.5f+5;
        transform.position = Vector3.Lerp(transform.position, dd, Time.deltaTime * moveLerp);
        if (transform.position.x>=xScreen*0.5f-3)
        {
            m_state = State.MoveLeft;
        }
    }

    private void UpMoveLeft()
    {
        dd = transform.position;
        dd.x = -xScreen * 0.5f-5;
        transform.position = Vector3.Lerp(transform.position, dd, Time.deltaTime * moveLerp);
        if (transform.position.x <= -xScreen * 0.5f+3)
        {
            m_state = State.MoveRight;
        }
    }

        private void UpDie()
    {
        GetComponent<Collider>().enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        YA_EnemyHP.Instance.EnemyTrigger(other);//플레이어 피 깍기
    }
}
