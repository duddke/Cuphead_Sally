using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YA_Mouse_Left : MonoBehaviour
{

    Transform sally;
    float speed = 12;
    Vector3 dir;
    Vector3 angles;
    public CharacterController cc;
    //��ũ�� x ���尪
    float xScreen;
    //��ũ�� y ���尪
    float yScreen;
    //Ÿ�� ��ġ �����ϴ� ��
    public GameObject target;
    //��Ʈ����
    bool destroy;
    public SpriteRenderer MouseForwad;
    public SpriteRenderer MouseLeft;
    public GameObject MouseDestroy;

    public enum State
    {
        Down,
        Left,
        Up,
        Fly,
        Fall
    }
    public State state;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player");
        yScreen = Camera.main.orthographicSize * 2;
        xScreen = yScreen * Screen.width/Screen.height;
        sally = GameObject.Find("Sally_P2").transform;
        //������ ���� �����´�
        cc = GetComponent<CharacterController>();
        cc.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        /*//�ٴڿ� ������
        if (cc.isGrounded)
        {
            //��ġ�� ���ʹ̺��� x�� ������
            if (transform.position.x < sally.position.x)
            {
                //��������(y90��)
                angles = new Vector3(0, 90, 0);
                //������ ���� �����
                dir = Vector3.left ;
                //x��ũ�� ũ�⸸ŭ ����(ī�޶� �� �����;��ҵ�)
                if (transform.position.x <= -xScreen*0.5)
                {
                print(11111111111111);
                    //z-90�� ����
                    angles = new Vector3(90, 90, 0);
                    dir = Vector3.up;
                    //x�������� �������̱⿡ x������ ���ؾ��� y��ũ�� ����ŭ ����
                    if (transform.position.x >= yScreen)
                    {
                        //�ٽ� z-90�� ����
                        angles = new Vector3(180, 90, 0);
                        dir = Vector3.right;
                        //Ÿ�� ��ġ�ޱ�
                        target = GameObject.Find("Player").transform.position;
                        if (target.x == transform.position.x)
                        {
                            //x��ġ�� Ÿ�ٰ� ���ٸ� ��������
                            dir = Vector3.down;
                            destroy = true;
                        }
                    }
                }
            }
        }*/
        transform.eulerAngles = angles;
        if (cc != null)
        { cc.Move(dir * speed * Time.deltaTime); }
        switch (state)
        {
            case State.Down:
                UdDown();
                break;
            case State.Left:
                UpLeft();
                break;
            case State.Up:
                UpUp();
                break;
            case State.Fly:
                UpFly();
                break;
            case State.Fall:
                UpFall();
                break;

        }
        if (cc.isGrounded)
        {
            if (destroy)
            {
                currentTime += Time.deltaTime;
                    MouseForwad.enabled = false;
                    MouseLeft.enabled = false;
                MouseDestroy.SetActive(true);
                if (currentTime >= DestroyTime)
                {
                    cc.enabled = false;
                    Destroy(gameObject);
                }
            }
        }
    }
    void UdDown()
    {
        dir = Vector3.down;
        angles = new Vector3(0, 0, 0);
        if (cc.isGrounded)
        {
            MouseForwad.enabled = false;
            MouseLeft.enabled = true;
            if (transform.position.x < sally.position.x)
            { state = State.Left; }
        }
    }
    void UpLeft()
    {
        angles = new Vector3(0, 90, 0);
        dir = Vector3.left;
        if (transform.position.x <= -19)
        {
            state = State.Up;
        }
    }
    void UpUp()
    {
        angles = new Vector3(90, 90, 0);
        dir = Vector3.up;
        if (transform.position.y >= yScreen-2)
        {
            MouseForwad.enabled = true;
            MouseLeft.enabled = false;
            state = State.Fly;
        }
    }
    void UpFly()
    {
        angles = new Vector3(0, 0, 0);
        dir = Vector3.right;
        if (target.transform.position.x <= transform.position.x)
        {
            state = State.Fall;
        }

    }
    float currentTime = 0;
    public float DestroyTime = 1;
    void UpFall()
    {
        dir = Vector3.down;
        transform.position += dir * speed * Time.deltaTime;
        destroy = true;
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
