using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YA_EnemyPhase1 : MonoBehaviour
{

    // 1������ ����
    // ���¸ӽ����� ��ų ����
    // ��ų ��� �� IDLE ���·� 2�� ��� �� �ٽ� ��ų (����)(4���� 1 Ȯ��)
    // ó�� ��ų�� ������ �� �� �� ����
    public float speed = 5;
    Vector3 dir;
    Transform target;
    //��ũ�� x ���尪
    float xScreen;
    //��ũ�� y ���尪
    float yScreen;
    public CharacterController cc;


    //�����ٵ� �ٲٱ� ����
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
        //�ݶ��̴� �ѱ�
        GetComponent<Collider>().enabled = true;
        //hp �ѱ�
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

    //�ð��� 2�ʰ� �帣��
    //�ð�
    public float currTime;
    float skillTime = 1;
    //������ ���� 2ȸ�̻� ���Դ��� �˿��ϱ�
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
        /*anim.SetTrigger("Jump");
        currTime += Time.deltaTime;
        if(currTime>=0.42f)
        {
            transform.position += Vector3.up * jumppower * Time.deltaTime;
            if (transform.position.y >= yScreen - 5)
            {
                //Ÿ���� ���� ������ ���
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
            cc.Move(dir * speed*0.1f * Time.deltaTime);
            if(cc.isGrounded)
            {
                anim.Play("JumpEnd");
                jump = false;
                state = State.Idle;
            }
        }
    }
    //��Ʈ ��ų

    public bool hartCount;
    private void SkillHart()
    {
        //���� ��Ʈ�� �߻����� �ʾҴٸ� 
        if (!hartCount)
        {
            HartSound.Play();
            anim.SetTrigger("Hart");
            hartCount = true;
        }
        currTime += Time.deltaTime;
        //0.3�ʰ� ������ ����
        if (currTime >= 0.3f)
        {
            currTime = 0;
            // ��Ʈ�� �ٽ� �� �� �ִ��� ��Ȳ���� �����
            hartCount = false;
            // ���̵���·� ������
            state = State.Idle;
        }
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
                cc.Move(Vector3.down * jumppower*1.5f * Time.deltaTime);
                if (cc.isGrounded)
                {
                    currTime = 0;
                    fanCount = false;
                    state = State.Idle;
                }
            }
        #region ���� ��ä��ũ��Ʈ
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
                
                *//*//Ÿ���� �ִٸ� ���
                fandir = target.transform.position - transform.position;
                //fandir.x = target.transform.position.x - transform.position.x;
                //fandir.y = 0;
                //�Ѿ��� Ÿ�ٹ������� ȸ�������ֱ�
                fan.transform.up = fandir; //�°� ���ϴ� �������� ���ư���->ȸ���� ���·� ȸ���� �������� ���ư�
                                           //�Ѿ��� �����Ǵ� ��ġ
                fan.transform.position = transform.position;*//*
                fanCount = true;
            }
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
        }*/


        //��ä ��ũ��Ʈ
        //��ä�� ������ Ÿ���� �ִ� �����̴�
        // ��ä���� Hp�� �����Ѵ�
        #endregion
    }

    //������ �������� ��ų
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
            if (!Fade)//�ִ�Ʈ���� �ѹ��� �� �ߵ��ϰ� �ϱ�...
            {
                anim.SetTrigger("Fade");
                FadeSound.Play();
                Fade = true;
            }
            currTime += Time.deltaTime;
            if (currTime >= 0.08f)
            {
                //���� �����Ŀ��� ������ŭ�� �����ϰ�
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
        //1�� �ڻ������ (�Ž�, �ݶ��̴� ����)
            currTime += Time.deltaTime;
        // Ÿ���� x���� �޴´�
            ffdir.x = target.position.x;
            //y���� ī�޶� ������ ���
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
        //x���� Ÿ����  x���� �����
            transform.position = ffdir;
            currTime += Time.deltaTime;
        //1�� ��
            if (currTime >= 0.8f)
            {
                // �Ž��� �ݶ��̴��� Ű��
                currTime = 0;
                SallyCol.enabled = true;
                //SallyMes.enabled = true;
                flyState = FlyState.Down;
            }
        }
        void FlyDown()
        {
            anim.Play("FadeLoop");
            //������ �Ʒ��� ��������(���� �ٿ�)
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

