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

    //��ũ�� x ���尪
    float xScreen;
    //��ũ�� y ���尪
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

    //���ʹ̰� ���� ������ õõ�� ������ ��
    public float downSpeed = 2;
    private void UpDown()
    {
        transform.position += Vector3.down * downSpeed * Time.deltaTime;
    //Ư�� ��ġ�� ���缭
        if(transform.position.y<=yScreen-5)
        {
            m_state = State.MoveRight;
        }
    }

    // �¿�� ���� �̵��Ѵ�
    // �̶� �¿� ���� ���� ��ũ�� x���� ������� �Ѵ�
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
        YA_EnemyHP.Instance.EnemyTrigger(other);//�÷��̾� �� ���
    }
}
