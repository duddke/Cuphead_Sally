using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YA_EnemyPhase2 : MonoBehaviour
{
    // 2������ ����
    // ���¸ӽ����� ��ų ����
    // ��ų ��� �� IDLE ���·� 2�� ��� �� �ٽ� ��ų (����)(4���� 1 Ȯ��)
    // ó�� ��ų�� ������ �� �� �� ����
    public float speed = 10;
    Vector3 dir;
    Transform target;
    //��ũ�� x ���尪
    float xScreen;
    //��ũ�� y ���尪
    float yScreen;
    public CharacterController cc;
    GameObject fanFactory;
    GameObject mouseLeftFactory;
    GameObject mouseRightFactory;
    public GameObject M_left;
    public GameObject M_right;

    //���� �̹��� ����
    public GameObject destroy;
    public GameObject sallySpri;

    //������ ����
    public GameObject babyposition;
    public GameObject baby1;
    public GameObject baby2;
    public GameObject babymanager;

    bool startRight;
    YA_EnemyHP sallyhp;

    public AudioSource Kick;
    public AudioSource ToySound;

    public enum State
    {
        Start,
        Idle,
        Mouse,
        Jump,
        Um,
        Die
    }
    public State state=State.Start;

    Animator anim;

    //�����ٵ� �ٲٱ� ����
    public GameObject SallyBody;


    // Start is called before the first frame update
    void Start()
    {
        //state = State.Mouse;
        fanFactory = Resources.Load<GameObject>("YA_Prefabs/Fan");
        mouseLeftFactory = Resources.Load<GameObject>("YA_Prefabs/Mouse_Left");
        mouseRightFactory = Resources.Load<GameObject>("YA_Prefabs/Mouse_Right");
        cc = GetComponent<CharacterController>();
        target = GameObject.Find("Foot").transform;
        yScreen = Camera.main.orthographicSize * 2;
        xScreen = yScreen * Camera.main.aspect;
        sallyhp = GetComponent<YA_EnemyHP>();

        babyposition.SetActive(true);
        babymanager.SetActive(true);
        baby2.SetActive(true);
        baby1.SetActive(true);
        babymanager.GetComponent<YA_Phase2_BabyManager>().enabled = true;

        anim = GetComponentInChildren<Animator>();


        if (target.position.x >= 0)
        {
            transform.position = new Vector3(-xScreen * 0.5f - 5, transform.position.y, target.position.z);
            startRight = true;
        }
        else
        {
            transform.position = new Vector3(xScreen * 0.5f + 5, transform.position.y, target.position.z);
        }

        cc.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, -xScreen * 0.5f, xScreen * 0.5f);
        transform.position = pos;
        if (sallyhp.HP <= 0)
        {
            state = State.Die;
        }
        switch (state)
        {
            case State.Start:
                PhStart();
                break;
            case State.Idle:
                SkillIdle();
                break;
            case State.Jump:
                SkillJump();
                break;
            case State.Mouse:
                SkillMouse();
                break;
            case State.Um:
                SkillUm();
                break;
            case State.Die:
                Die();
                break;
        }
    }

    //�߾��� �������� �÷��̾ �ִ� ���� �ݴ��ʿ��� 
    //4���� 1 ���� �̵��ϸ� ����

    private void PhStart()
    {
        if (target.position.x >= transform.position.x)
        {
            SallyBody.transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            SallyBody.transform.eulerAngles = new Vector3(0, 0, 0);
        }
        GetComponent<Collider>().enabled = true;
            GetComponent<YA_EnemyHP>().enabled = true;
            if (startRight)
            {
                dir = Vector3.right;
                if (transform.position.x >= -xScreen * 0.2f)
                    state = State.Idle;
            }
            else
            {
                dir = Vector3.left;
                if (transform.position.x <= xScreen * 0.2f)
                    state = State.Idle;
            }
        cc.Move(dir * speed*2 * Time.deltaTime);
    }

    //������ ���� Ī���� �޽�
    private void Die()
    {
        GetComponent<Collider>().enabled = false;
        sallySpri.SetActive(false);
        destroy.SetActive(true);
        cc.enabled=false;
        transform.position += Vector3.up * speed * Time.deltaTime;
        babyposition.SetActive(false);
        babymanager.SetActive(false);
        baby2.SetActive(false);
        baby1.SetActive(false);
        if (transform.position.y >= yScreen + 5)
        {
            GameManager.Instance.two = true;
            gameObject.SetActive(false);
        }
    }

    //���̵�
    //�ð��� 2�ʰ� �帣��
    //�ð�
    public float currTime;
    float skillTime = 1;

    bool countSet;
    private void SkillIdle()
    {
        if (target.position.x >= transform.position.x)
        {
            SallyBody.transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            SallyBody.transform.eulerAngles = new Vector3(0, 0, 0);
        }
        currTime += Time.deltaTime;
        if(currTime>=skillTime)
        {
            currTime = 0;
            int ran = -1;
            int rann = -1;
            if (!countSet)
            {
                ran = Random.Range(0, 3);
                if (ran == 0 )
                {
                    if (ran != rann)
                    {
                        state = State.Jump;
                        move = Move.Jump;
                    }
                    else
                    {
                        state = State.Mouse;
                    }
                }
                else if (ran == 1 )
                {
                    if (ran != rann)
                    {
                        state = State.Mouse;
                    }
                    else
                    {
                        state = State.Um;
                    }
                }
                else if (ran == 2 )
                {
                    if (ran != rann)
                    {
                        state = State.Um;
                    }
                    else
                    {
                        state = State.Jump;
                        move = Move.Jump;
                    }
                }
                countSet = true;
            }
            else
            {
                rann = Random.Range(0, 3);
                if (rann == 0)
                {
                    state = State.Jump;
                    move = Move.Jump;
                }
                else if (rann == 1)
                {
                    state = State.Mouse;
                }
                else if (rann == 2)
                {
                    state = State.Um;
                }
                countSet = false;
            }
        }
    }





    //���� ��ų
    public float jumppower = 10;
    public enum Move
    {
        Jump,
        Down
    }
    public Move move;
    bool jump = false;
    private void SkillJump()
    {
        #region ��������
        /* switch (move)
         {
         //������ (���� �ö� ��)
             case Move.Jump:
                 UpdateJump();
                 break;
         //��������
             case Move.Down:
                 UpdateDown();
                 break;
         }
         void UpdateJump()
         {
             transform.position += Vector3.up * jumppower * Time.deltaTime;
             if (transform.position.y >= yScreen-5)
             {
                 //Ÿ���� ���� ������ ���
                 dir = target.position - transform.position;
                 move = Move.Down;
             }
         }
         void UpdateDown()
         {
             //Ÿ�� ��ġ ������Ʈ �Ǹ� �ȵ�
             //transform.position += dir * speed * Time.deltaTime;//�ٴ��� ����. CC�� �����̰� �ϱ�?
             cc.Move(dir * speed * Time.deltaTime);
             if(cc.isGrounded)
             {
                 state = State.Idle;
             }
         }*/
        #endregion
        switch (move)
        {
            //������ (���� �ö� ��)
            case Move.Jump:
                UpdateJump();
                break;
            //��������
            case Move.Down:
                UpdateDown();
                break;
        }

        void UpdateJump()
        {
            if (!jump)//�ִ�Ʈ���� �ѹ��� �� �ߵ��ϰ� �ϱ�...
            {
                anim.SetTrigger("Jump");
                jump = true;
                Kick.Play();
            }
            currTime += Time.deltaTime;
            if (currTime >= 0.3f)
            {
                transform.position += Vector3.up * jumppower * Time.deltaTime;
                if (transform.position.y >= yScreen)
                {
                    currTime = 0;
                    anim.Play("JumpDown");
                    //Ÿ���� ���� ������ ���
                    dir = target.position - transform.position;
                    if (target.position.x >= transform.position.x)
                    {
                        SallyBody.transform.eulerAngles = new Vector3(0, 180, 0);
                    }
                    else
                    {
                        SallyBody.transform.eulerAngles = new Vector3(0, 0, 0);
                    }
                    move = Move.Down;
                }
            }
        }
        void UpdateDown()
        {
            //Ÿ�� ��ġ ������Ʈ �Ǹ� �ȵ�
            //transform.position += dir * speed * Time.deltaTime;//�ٴ��� ����. CC�� �����̰� �ϱ�?
            cc.Move(dir * speed * 0.1f * Time.deltaTime);
            if (cc.isGrounded)
            {
                anim.Play("JumpEnd");
                jump = false;
                state = State.Idle;
            }
        }
    }






    //�� ��ų
    //�� ���������� �� ģ���� �������� �� ģ�� ���� ������ ��

    int mouseCount;
    public enum Mouse
    {
        One,
        Two,
        Idle
    }
    public Mouse mouse;
    bool mouseset;
    private void SkillMouse()
    {
        switch(mouse)
        {
            case Mouse.One:
                UpOne();
                break;
            case Mouse.Two:
                UpTwo();
                break;
            case Mouse.Idle:
                UpIdle();
                break;
        }
        void UpOne()
        {
            if (!mouseset)
            {
                anim.SetTrigger("Mouse");
                ToySound.Play();
                mouseset = true;
            }
            currTime += Time.deltaTime;
            if (currTime >= 0.49f)
            {
                GameObject mouseLeft = Instantiate(mouseLeftFactory);
                mouseLeft.transform.position = M_left.transform.position;
                GameObject mouseRight = Instantiate(mouseRightFactory);
                mouseRight.transform.position = M_right.transform.position;
                mouseCount++;
                currTime = 0;
            }
            if (mouseCount < 2)
            {
                mouse = Mouse.Two;
            }
            else
            {
                mouse = Mouse.Idle;
            }
        }
        void UpTwo()
        {
            mouse = Mouse.One;
        }
        void UpIdle()
        {
            currTime += Time.deltaTime;
            if (currTime >= 0.3f)
            {
                currTime = 0;
                mouseset = false;
                state = State.Idle;
            }
        }
        //���� ��Ʈ�� �߻����� �ʾҴٸ� 

            // ��Ʈ�� �ٽ� �� �� �ִ��� ��Ȳ���� �����
        
        //0.3�ʰ� ������ ����
            // ���̵���·� ������

        
    }





    //��ä ��ų

    bool fanCount;
    /*public enum FanMove
    {
        Jump,
        Fan,
        Down
    }
    public FanMove fanMove;*/
    private void SkillUm()
    {
        if (!fanCount)
        {
            anim.SetTrigger("Fan");
            fanCount = true;
        }
        currTime += Time.deltaTime;
        if (currTime >= 1.37f)
        {
            //Ÿ�� ��ġ ������Ʈ �Ǹ� �ȵ�
            //transform.position += dir * speed * Time.deltaTime;//�ٴ��� ����. CC�� �����̰� �ϱ�?
            cc.Move(Vector3.down * jumppower * Time.deltaTime);
            if (cc.isGrounded)
            {
                currTime = 0;
                print(11111);
                fanCount = false;
                state = State.Idle;
            }
        }
        #region ������ä
        /*switch (fanMove)
        {
            //������ (���� �ö� ��)
            case FanMove.Jump:
                FanJump();
                break;
            case FanMove.Fan:
                FanFan();
                break;
            //��������
            case FanMove.Down:
                FanDown();
                break;
        }
        void FanJump()
        {
            transform.position += Vector3.up * jumppower * Time.deltaTime;
            if (transform.position.y >= yScreen-5)
            {
                fanMove = FanMove.Fan;
            }
        }
        void FanFan()
        {
            //���� ��ä�� ������ �ʾҴٸ�
            if (!fanCount)
            {
                //1�� �� 
                //��ä�� ������
                GameObject fan = Instantiate(fanFactory);
                fan.transform.position = transform.position;
                *//*//Ÿ���� �ִٸ� ���
                fandir = target.transform.position - transform.position;
                //fandir.x = target.transform.position.x - transform.position.x;
                //fandir.y = 0;
                //�Ѿ��� Ÿ�ٹ������� ȸ�������ֱ�
                fan.transform.up = fandir; //�°� ���ϴ� �������� ���ư���->ȸ���� ���·� ȸ���� �������� ���ư�
                                           //�Ѿ��� �����Ǵ� ��ġ
                fan.transform.position = transform.position;*//*
            }
            fanCount = true;
            currTime += Time.deltaTime;
            //0.3�ʰ� ������ ����
            if (currTime >= 0.3f)
            {
                currTime = 0;
                //print(2222222222222);
                fanMove = FanMove.Down;
            }
        }
        void FanDown()
        {
            //Ÿ�� ��ġ ������Ʈ �Ǹ� �ȵ�
            //transform.position += dir * speed * Time.deltaTime;//�ٴ��� ����. CC�� �����̰� �ϱ�?
            cc.Move(Vector3.down * jumppower * Time.deltaTime);
            if (cc.isGrounded)
            {
                fanCount = false;
                state = State.Idle;
            }
        }


        //��ä ��ũ��Ʈ
        //��ä�� ������ Ÿ���� �ִ� �����̴�
        // ��ä���� Hp�� �����Ѵ�*/
        #endregion
    }

}

