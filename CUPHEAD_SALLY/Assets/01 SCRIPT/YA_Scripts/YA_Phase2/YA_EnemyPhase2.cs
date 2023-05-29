using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YA_EnemyPhase2 : MonoBehaviour
{
    // 2페이즈 공격
    // 상태머신으로 스킬 구성
    // 스킬 사용 후 IDLE 상태로 2초 대기 후 다시 스킬 (랜덤)(4분의 1 확률)
    // 처음 스킬은 종류별 한 번 씩 실행
    public float speed = 10;
    Vector3 dir;
    Transform target;
    //스크린 x 저장값
    float xScreen;
    //스크린 y 저장값
    float yScreen;
    public CharacterController cc;
    GameObject fanFactory;
    GameObject mouseLeftFactory;
    GameObject mouseRightFactory;
    public GameObject M_left;
    public GameObject M_right;

    //다이 이미지 변경
    public GameObject destroy;
    public GameObject sallySpri;

    //페이즈 관리
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

    //샐리바디 바꾸기 방향
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

    //중앙을 기준으로 플레이어가 있는 쪽의 반대쪽에서 
    //4분의 1 정도 이동하며 등장

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

    //페이즈 관리 칭구들 펄스
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

    //아이들
    //시간이 2초가 흐르면
    //시간
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





    //점프 스킬
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
        #region 그전점프
        /* switch (move)
         {
         //점프후 (위로 올라간 후)
             case Move.Jump:
                 UpdateJump();
                 break;
         //내려간다
             case Move.Down:
                 UpdateDown();
                 break;
         }
         void UpdateJump()
         {
             transform.position += Vector3.up * jumppower * Time.deltaTime;
             if (transform.position.y >= yScreen-5)
             {
                 //타겟을 향한 방향을 잡고
                 dir = target.position - transform.position;
                 move = Move.Down;
             }
         }
         void UpdateDown()
         {
             //타겟 위치 업데이트 되면 안됨
             //transform.position += dir * speed * Time.deltaTime;//바닥을 뚫음. CC로 움직이게 하기?
             cc.Move(dir * speed * Time.deltaTime);
             if(cc.isGrounded)
             {
                 state = State.Idle;
             }
         }*/
        #endregion
        switch (move)
        {
            //점프후 (위로 올라간 후)
            case Move.Jump:
                UpdateJump();
                break;
            //내려간다
            case Move.Down:
                UpdateDown();
                break;
        }

        void UpdateJump()
        {
            if (!jump)//애님트리거 한번씩 만 발동하게 하기...
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
                    //타겟을 향한 방향을 잡고
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
            //타겟 위치 업데이트 되면 안됨
            //transform.position += dir * speed * Time.deltaTime;//바닥을 뚫음. CC로 움직이게 하기?
            cc.Move(dir * speed * 0.1f * Time.deltaTime);
            if (cc.isGrounded)
            {
                anim.Play("JumpEnd");
                jump = false;
                state = State.Idle;
            }
        }
    }






    //쥐 스킬
    //쥐 오른쪽으로 갈 친구와 왼쪽으로 갈 친구 따로 만들어야 함

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
        //만약 하트를 발사하지 않았다면 

            // 하트를 다시 쓸 수 있느느 상황으로 만들고
        
        //0.3초가 지나고 나서
            // 아이들상태로 돌린다

        
    }





    //부채 스킬

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
            //타겟 위치 업데이트 되면 안됨
            //transform.position += dir * speed * Time.deltaTime;//바닥을 뚫음. CC로 움직이게 하기?
            cc.Move(Vector3.down * jumppower * Time.deltaTime);
            if (cc.isGrounded)
            {
                currTime = 0;
                print(11111);
                fanCount = false;
                state = State.Idle;
            }
        }
        #region 그전부채
        /*switch (fanMove)
        {
            //점프후 (위로 올라간 후)
            case FanMove.Jump:
                FanJump();
                break;
            case FanMove.Fan:
                FanFan();
                break;
            //내려간다
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
            //만약 부채를 날리지 않았다면
            if (!fanCount)
            {
                //1초 뒤 
                //부채를 날린다
                GameObject fan = Instantiate(fanFactory);
                fan.transform.position = transform.position;
                *//*//타겟이 있다면 쏜다
                fandir = target.transform.position - transform.position;
                //fandir.x = target.transform.position.x - transform.position.x;
                //fandir.y = 0;
                //총알을 타겟방향으로 회전시켜주기
                fan.transform.up = fandir; //걔가 향하는 방향으로 나아가라->회전한 상태로 회전한 방향으로 날아감
                                           //총알이 생성되는 위치
                fan.transform.position = transform.position;*//*
            }
            fanCount = true;
            currTime += Time.deltaTime;
            //0.3초가 지나고 나서
            if (currTime >= 0.3f)
            {
                currTime = 0;
                //print(2222222222222);
                fanMove = FanMove.Down;
            }
        }
        void FanDown()
        {
            //타겟 위치 업데이트 되면 안됨
            //transform.position += dir * speed * Time.deltaTime;//바닥을 뚫음. CC로 움직이게 하기?
            cc.Move(Vector3.down * jumppower * Time.deltaTime);
            if (cc.isGrounded)
            {
                fanCount = false;
                state = State.Idle;
            }
        }


        //부채 스크립트
        //부채의 방향은 타겟이 있는 방향이다
        // 부채에도 Hp가 존재한다*/
        #endregion
    }

}

