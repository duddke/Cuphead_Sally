using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YA_EnemyPhase1 : MonoBehaviour
{

    // 1페이즈 공격
    // 상태머신으로 스킬 구성
    // 스킬 사용 후 IDLE 상태로 2초 대기 후 다시 스킬 (랜덤)(4분의 1 확률)
    // 처음 스킬은 종류별 한 번 씩 실행
    public float speed = 5;
    Vector3 dir;
    Transform target;
    //스크린 x 저장값
    float xScreen;
    //스크린 y 저장값
    float yScreen;
    public CharacterController cc;


    //샐리바디 바꾸기 방향
    public GameObject SallyBody;

    YA_EnemyHP sallyhp;

    public AudioSource Kick;
    public AudioSource FadeSound;
    public AudioSource HartSound;

    public enum State
    {
        Start,
        Idle,
        Jump,
        Hart,
        Um,
        Fly,
        Die
    }
    public State state=State.Start;

    Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        yScreen = Camera.main.orthographicSize*2;
        xScreen = yScreen * Camera.main.aspect;
        target = GameObject.Find("Foot").transform;

        anim = GetComponentInChildren<Animator>();
        sallyhp = GetComponent<YA_EnemyHP>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, -xScreen * 0.5f, xScreen * 0.5f);
        transform.position = pos;
        if(sallyhp.HP<=0)
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
            case State.Hart:
                SkillHart();
                break;
            case State.Um:
                SkillUm();
                break;
            case State.Fly:
                SkillFly();
                break;
            case State.Die:
                Die();
                break;
        }
    }

    private void PhStart()
    {
        //콜라이더 켜기
        GetComponent<Collider>().enabled = true;
        //hp 켜기
        GetComponent<YA_EnemyHP>().enabled = true;
        state = State.Idle;
    }

    bool die;
    private void Die()
    {
        GetComponent<Collider>().enabled = false;
        if (!die)
        {
            currTime = 0;
            anim.Play("Die");
            die = true;
        }
        currTime += Time.deltaTime;
        if (currTime >= 1f)
        {
            currTime = 0;
            die = false;
            GameManager.Instance.first = true;
            gameObject.SetActive(false);
        }
    }

    //시간이 2초가 흐르면
    //시간
    public float currTime;
    float skillTime = 1;
    //동일한 수가 2회이상 나왔는지 검열하기
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
                ran = Random.Range(0, 4);
                if (ran == 0 )
                {
                    if (ran != rann)
                    {
                        state = State.Jump;
                        move = Move.Jump;
                    }
                    else
                    {
                        state = State.Hart;
                    }
                }
                else if (ran == 1 )
                {
                    if (ran != rann)
                    {
                        state = State.Hart;
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
                        state = State.Fly;
                        flyState = FlyState.Jump;
                    }
                }
                else if (ran == 3 )
                {
                    if (ran != rann)
                    {
                        state = State.Fly;
                        flyState = FlyState.Jump;
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
                rann = Random.Range(0, 4);
                if (rann == 0)
                {
                    state = State.Jump;
                    move = Move.Jump;
                }
                else if (rann == 1)
                {
                    state = State.Hart;
                }
                else if (rann == 2)
                {
                    state = State.Um;
                }
                else if (rann == 3)
                {
                    state = State.Fly;
                    flyState = FlyState.Jump;
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
        /*anim.SetTrigger("Jump");
        currTime += Time.deltaTime;
        if(currTime>=0.42f)
        {
            transform.position += Vector3.up * jumppower * Time.deltaTime;
            if (transform.position.y >= yScreen - 5)
            {
                //타겟을 향한 방향을 잡고
                dir = target.position - transform.position;
                if (currTime >= 1)
                {
                    cc.Move(dir * speed * 0.25f * Time.deltaTime);
                    if (cc.isGrounded)
                    {
                        currTime = 0;
                        state = State.Idle;
                    }
                }
            }
        }*/
        
            
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
            cc.Move(dir * speed*0.1f * Time.deltaTime);
            if(cc.isGrounded)
            {
                anim.Play("JumpEnd");
                jump = false;
                state = State.Idle;
            }
        }
    }
    //하트 스킬

    public bool hartCount;
    private void SkillHart()
    {
        //만약 하트를 발사하지 않았다면 
        if (!hartCount)
        {
            HartSound.Play();
            anim.SetTrigger("Hart");
            hartCount = true;
        }
        currTime += Time.deltaTime;
        //0.3초가 지나고 나서
        if (currTime >= 0.3f)
        {
            currTime = 0;
            // 하트를 다시 쓸 수 있느느 상황으로 만들고
            hartCount = false;
            // 아이들상태로 돌린다
            state = State.Idle;
        }
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
                cc.Move(Vector3.down * jumppower*1.5f * Time.deltaTime);
                if (cc.isGrounded)
                {
                    currTime = 0;
                    fanCount = false;
                    state = State.Idle;
                }
            }
        #region 그전 부채스크립트
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
                
                *//*//타겟이 있다면 쏜다
                fandir = target.transform.position - transform.position;
                //fandir.x = target.transform.position.x - transform.position.x;
                //fandir.y = 0;
                //총알을 타겟방향으로 회전시켜주기
                fan.transform.up = fandir; //걔가 향하는 방향으로 나아가라->회전한 상태로 회전한 방향으로 날아감
                                           //총알이 생성되는 위치
                fan.transform.position = transform.position;*//*
                fanCount = true;
            }
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
        }*/


        //부채 스크립트
        //부채의 방향은 타겟이 있는 방향이다
        // 부채에도 Hp가 존재한다
        #endregion
    }

    //위에서 떨어지는 스킬
    public enum FlyState
    {
        Jump,
        Fade,
        FadeOut,
        Down
    }
    public FlyState flyState;
    CapsuleCollider SallyCol;
    bool Fade;
    Vector3 ffdir;
    private void SkillFly()
    {
        SallyCol = GetComponent<CapsuleCollider>();
        //SallyMes = GetComponent<MeshRenderer>();
        switch (flyState)
        {
            case FlyState.Jump:
                FlyJump();
                break;
            case FlyState.Fade:
                FlyFade();
                break;
            case FlyState.FadeOut:
                FlyFadeOut();
                break;
            case FlyState.Down:
                FlyDown();
                break;
        }
        void FlyJump()
        {
            if (!Fade)//애님트리거 한번씩 만 발동하게 하기...
            {
                anim.SetTrigger("Fade");
                FadeSound.Play();
                Fade = true;
            }
            currTime += Time.deltaTime;
            if (currTime >= 0.08f)
            {
                //원래 점프파워의 반절만큼만 점프하고
                transform.position += Vector3.up * jumppower * Time.deltaTime;
                if (transform.position.y >= yScreen * 0.5f - 3)
                {
                    currTime = 0;
                    flyState = FlyState.Fade;
                }
            }
        }
        void FlyFade()
        {
        //1초 뒤사라진다 (매쉬, 콜라이더 끄기)
            currTime += Time.deltaTime;
        // 타겟의 x값을 받는다
            ffdir.x = target.position.x;
            //y값을 카메라 밖으로 잡고
            ffdir.y = yScreen;
            if (currTime >= 0.5f)
            {
                currTime = 0;
                SallyCol.enabled = false;
                //SallyMes.enabled = false;
                flyState = FlyState.FadeOut;
            }
        }
        void FlyFadeOut()
        {
        //x값을 타겟의  x값을 잡느다
            transform.position = ffdir;
            currTime += Time.deltaTime;
        //1초 뒤
            if (currTime >= 0.8f)
            {
                // 매쉬와 콜라이더를 키고
                currTime = 0;
                SallyCol.enabled = true;
                //SallyMes.enabled = true;
                flyState = FlyState.Down;
            }
        }
        void FlyDown()
        {
            anim.Play("FadeLoop");
            //위에서 아래로 떨어진다(벡터 다운)
            cc.Move(Vector3.down * jumppower * Time.deltaTime);
            if (cc.isGrounded)
            {
                transform.position += Vector3.up * jumppower * Time.deltaTime;
                anim.Play("FadeIn");
                Fade = false;
                state = State.Idle;
            }
        }
    }
}

