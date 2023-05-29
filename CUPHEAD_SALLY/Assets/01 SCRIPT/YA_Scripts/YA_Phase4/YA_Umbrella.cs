using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YA_Umbrella : MonoBehaviour
{
    //���ʹ� ���� �� �ٷ� Ÿ�� ��ġ�� �޾�, 
    Transform target;
    Vector3 targetPos;
    // Ÿ���� X ��ǥ���� �Ʒ��� õõ�� �����´� (�������� �ӵ��� �����̴� �ӵ� �ٸ�
    // �׶��� ���¿���
    // ����ź ó�� �÷��̾�� �¿�� �Դٰ��� �Ѵ�
    Animator anim;

    Rigidbody rid;

    public AudioSource UmSound;
    public AudioSource UmDownSound;

    public enum State
    {
        Down,
        UpMove,
        DownMove,
        Target
    }
    public State state = State.Down;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        target = GameObject.Find("Player").transform;
        Vector3 pos = transform.position;
        pos.x = target.position.x;
        transform.position = pos;
        rid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (YA_EnemyHP.Instance.hp <= 0)
        {
            GetComponent<Collider>().enabled = false;
        }
        switch (state)
        {
            case State.Down:
                UpDown();
                break;
            case State.UpMove:
                UpMove();
                break;
            case State.DownMove:
                DownMove();
                break;
            case State.Target:
                UpTarget();
                break;
        }
    }


    public float downSpeed = 2;
    private void UpDown()
    {
        UmDownSound.Play();
        transform.position += Vector3.down * downSpeed * Time.deltaTime;
        if(isGround)
        {
            UmDownSound.Stop();
            state = State.Target;
        }
    }

    public float num = 5;
    public float speed = 0;
    public float force = 1;
    Vector3 lastPos;
    Vector3 dir;
    public float disNum = 5;
    public float maxspeed = 5;
    // Ÿ���� y���� ������ ������
    // Ÿ���� ��ġ�� �Լ� ���ŵǰ�
    // Ÿ���� y���� ���ϸ�
    // Ư�� ������ �޼��ؾ� ���ŵȴ�

    //����� Ÿ���� �ִ� �������� ���Ѵ�.
    //Ÿ�ٿ� ��������� �ӵ��� ���� �ش�
    //�ӵ��� 0�� �Ǹ� �ٽ� Ÿ�� ������ ã�´�
    private void UpMove()
    {
        /* if (right)
         {
             lastPos.x = targetPos.x + 10;
             if (transform.position.x - targetPos.x >= num)
             {
                 right = false;
                 state = State.Target;
             }    
         }
         else
         {
             lastPos.x = targetPos.x - 10;
             if (targetPos.x - transform.position.x >= num)
             {
                 state = State.Target;
             }
         }
         dir = lastPos - transform.position;
         dir.Normalize();
         transform.position += dir* Time.deltaTime * speed;*/

        if (right)
        {
            dir = Vector3.right;
            if (target.position.x <= transform.position.x)
            {
                state = State.DownMove;
            }
        }
        else
        {
            dir = Vector3.left;
            if (target.position.x > transform.position.x)
            {
                state = State.DownMove;
            }
        }
        speed += force * Time.deltaTime;
        speed = Mathf.Clamp(speed, 0, maxspeed);
        transform.position += dir * speed * Time.deltaTime;

        float dis = Vector3.Distance(target.position, transform.position);
        if (speed == maxspeed && dis <= disNum)
        {
            state = State.DownMove;
        }

    }

    private void DownMove()
    {
        UmSound.Play();
        speed -= force * Time.deltaTime;
        speed = Mathf.Clamp(speed, 0, maxspeed);
        transform.position += dir * speed * Time.deltaTime;
        float dis = Vector3.Distance(target.position, transform.position);
        if (speed <= 0)
            {
                state = State.Target;
            }
    }


    bool right;
    private void UpTarget()
    {
        /*targetPos = target.position;
        lastPos = transform.position;
        if(transform.position.x <= targetPos.x)
        {
            right = true;
        }
        state = State.Move;*/
        targetPos = target.position;
        lastPos = transform.position;
        if (transform.position.x<target.position.x)
        {
            anim.SetTrigger("Right");
            right = true;
            state = State.UpMove;
        }
        else if (transform.position.x >= target.position.x)
        {
            anim.SetTrigger("Left");
            right = false;
            state = State.UpMove;
        }
    }

    bool isGround;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name=="Ground")
        {
            Vector3 currentPos = transform.position;
            transform.position = currentPos;
            isGround = true;
        }
        YA_EnemyHP.Instance.EnemyTrigger(other);//�÷��̾� �� ���
    }
}
