using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YA_HartMoving : MonoBehaviour
{
    //�¾�� ���� Ÿ�� ������ ��
    //�߾ӿ��� ���ٰ� �� �Ʒ���
    Transform target;
    //��ũ�� x ���尪
    float xScreen;
    //��ũ�� y ���尪
    float yScreen;
    //����
    Vector3 dir;
    //���������� ���� �������� ���� �Ұ����� Ȯ���ؼ� ������ ���� �ϱ�
    bool right;

    // �и� Ÿ�̹� (����)
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

        // �и�(����)
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
            //���� Ÿ�ٺ��� x��ǥ�� ũ�ٸ�-
            if (right)
            {
                tr = transform.position + new Vector3(-3, yScreen, 0);
            }
            //�ƴ϶�� +
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
        if (transform.position.y >= td.y)//�� ���� ����Ǳ⵵ ���� �ٿ��� ����ɱ�? �ٿ��� �Ǵ� ������ �޼��Ǿ?? �� �׷��� ���� �޼��Ǵ� ���ϱ�>??
        {
            if (right)
            {
                tr = transform.position - new Vector3(3, yScreen * 2, 0);
            }
            //�ƴ϶�� +
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
            //�ƴ϶�� +
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

    // �и� (����)
    void Parring()
    {
        if (b_parring == true)
        {
            currentTime += Time.deltaTime;

            // �и� Ÿ�̹�
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
