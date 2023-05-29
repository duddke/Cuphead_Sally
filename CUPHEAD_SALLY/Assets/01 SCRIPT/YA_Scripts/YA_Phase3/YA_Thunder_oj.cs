using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YA_Thunder_oj : MonoBehaviour
{
    Transform target;
    CharacterController cc;
    public float speed = 12;
    Vector3 dir;
    //��ũ�� x ���尪
    float xScreen;
    //��ũ�� y ���尪
    float yScreen;
    //Ÿ�� ��ġ���� ���ٰ�
    //x���� Ÿ�ٰ� �������� 
    //-dir��������(�Ƹ� ���� �ܰ� �̷��� ���ؾ� ������ ���Ë��� ���� ������ ��������)

    //��ŸƮ���� ��ǥ ����
    Vector3 startPos;

    //���������� �������

    public enum State
    {
        Down,
        Up
    }
    public State state;
    // Start is called before the first frame update
    void Start()
    {
        cc = gameObject.GetComponent<CharacterController>();
        cc.enabled = true;
        target = GameObject.Find("Foot").transform;
        yScreen = Camera.main.orthographicSize * 2;
        xScreen = yScreen * Camera.main.aspect;
        dir = target.position - transform.position;
        dir.Normalize();
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        switch(state)
        {
            case State.Down:
                upDown();
                break;
            case State.Up:
                upUp();
                break;
        }
        /*cc.Move(dir * speed * Time.deltaTime);
        if(cc.isGrounded)
        {
            print(1212);
            int ran1 = Random.Range(1, 11);
            dir = GameObject.Find("TS" + ran1).transform.position-transform.position;
            dir.Normalize();
        }
        else
        {
           
        }*/
    }


    private void upDown()
    {

        cc.Move(dir * speed * Time.deltaTime);
        if (cc.isGrounded)
        {
            state = State.Up;
            dd = transform.position;
        }
    }
    Vector3 dd;
    private void upUp()
    {
        int ran1 = Random.Range(1, 11);
        dir = GameObject.Find("TS" + ran1).transform.position - transform.position;
        if (startPos.x >= dd.x)
        {
            dir.x = Mathf.Clamp(dir.x, dd.x + 2, -xScreen * 0.5f);
        }
        else
        {
            dir.x = Mathf.Clamp(dir.x, dd.x - 2, xScreen * 0.5f);
        }
        dir.Normalize();
        cc.Move(dir * speed * Time.deltaTime);
        if (GameObject.Find("TS" + ran1).transform.position.y <= transform.position.y+2)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        YA_EnemyHP.Instance.EnemyTrigger(other);
        if (other.gameObject.name == "Player")
        {
            Destroy(gameObject);
        }

    }

}
