using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public delegate void PhaseEnd();

public class YA_EnemyPhase3 : MonoBehaviour
{
    public PhaseEnd phaseEnd;
    public static YA_EnemyPhase3 Instance;
    Animator anim;

    public AudioSource ThunderSound;

    // �༺


    // �ĵ�
    //�� �����ڸ��� ���ʿ��� ���������� �ĵ��� �ɾ��
    //�ĵ��� ���� �ִ� ��ġ�� �������� �� �� �ˤ���

    //����
    //�ĵ��� ���� 2�� �� ������ ������ ��
    //���� ���ڸ��� ���ʿ��� ���� ����, �÷��̾� ��ġ�� ���ٰ� �ݴ�������
    //1�� �� �� ������ ���� (������ ��ġ�� �����ε�)(������ ����. 2~4��

    //���� �ݺ�
    public enum State
    {
        Start,
        Idle,
        Meteo,
        Bigwave,
        ThunderStay,
        Thunder,
        Die
    }
    public State state = State.Start;
    //��ũ�� x ���尪
    float xScreen;
    //��ũ�� y ���尪
    float yScreen;
    MeshRenderer mesh;
    Vector3 dir;
    public float SPEED = 8;
    float speed=0;
    CharacterController cc;
    public GameObject meteooj;
    public GameObject bigoj;
    public GameObject thunderoj;

    //���� �̹��� ����
    public GameObject destroy;
    public GameObject sallySpri;

    YA_EnemyHP sallyhp;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        yScreen = Camera.main.orthographicSize * 2;
        xScreen = yScreen * Camera.main.aspect;
        mesh = GetComponent<MeshRenderer>();
        transform.position = new Vector3(transform.position.x, yScreen + 7, transform.position.z);
        cc = GetComponent<CharacterController>();
        cc.enabled = true;
        sallyhp = GetComponent<YA_EnemyHP>();

        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (sallyhp.HP <= 0)
        {
            state = State.Die;
        }
        switch (state)
        {
            case State.Start:
                StartState();
                break;
            case State.Idle:
                IdleState();
                break;
            case State.Meteo:
                MeteoState();
                break;
            case State.Bigwave:
                BigwaveState();
                break;
            case State.ThunderStay:
                ThunderSStaytate();
                break;
            case State.Thunder:
                ThunderState();
                break;
            case State.Die:
                Die();
                break;
        }
        
    }

    public void Die()
    {
        GetComponent<Collider>().enabled = false;
        sallySpri.SetActive(false);
        destroy.SetActive(true);
        cc.enabled = false;
        str.SetActive(false);
        transform.position += Vector3.up * SPEED * Time.deltaTime;
        if (transform.position.y >= yScreen + 5)
        {
            GameManager.Instance.three = true;
            gameObject.SetActive(false);
        }
        if (phaseEnd != null)
        {
            phaseEnd();
        }
    }

    public float stopTime = 1f;
    private void StartState()
    {
        //�ݶ��̴� �ѱ�
        GetComponent<Collider>().enabled = true;
        //hp �ѱ�
        GetComponent<YA_EnemyHP>().enabled = true;
        currentTime += Time.deltaTime;
        if (currentTime >= stopTime)
        {
            
            speed = SPEED;
            if (currentTime >= stopTime*2)
            { currentTime = 0; }
        }
        else 
        {
            speed = 0;
        }
        dir = Vector3.down;
        cc.Move(dir * speed * Time.deltaTime);
        if(transform.position.y<=yScreen*0.5+1)
        {
            state = State.Idle;
        }
        
    }
    private void IdleState()
    {

        state = State.Meteo;
        if (transform.position.y <= yScreen * 0.5f)
        {
            speed = 0;
            state = State.Meteo;
        }
    }

    public GameObject str;
    public bool meteoEnd = false;
    bool meteoCome;
    bool animTime;
    private void MeteoState()
    {
        thunderEnd = false;
        //���׿� 
        //���� ���ӿ�����Ʈ Ʈ�� ->���׿� �̹��� Ʈ��
        str.SetActive(true);
        YA_Phase3String.Instance.meteo = true;
        if (YA_Phase3String.Instance.upend)
        {
            str.SetActive(false);
            anim.SetBool("Attack", true);
            if (!meteoCome)
            {
                Instantiate(meteooj);
                meteoCome = true;
            }
            //Ʈ���� ��� �ĵ� ��ȯ �Լ� 
            if (meteoEnd)
            {
                YA_Phase3String.Instance.meteo = false;
                YA_Phase3String.Instance.upend = false;
                meteoCome = false;
                state = State.Bigwave;
            }
        }
    }
    public bool waveEnd = false;
    bool waveCome;
    private void BigwaveState()
    {
        meteoEnd = false;
        //�ĵ�
        //���� ���ӿ�����Ʈ Ʈ�� ->�ĵ� �̹��� Ʈ��
        str.SetActive(true);
        YA_Phase3String.Instance.bigwave = true;
        if (YA_Phase3String.Instance.upend)
        {
            str.SetActive(false);
            if (!waveCome)
            {
                Instantiate(bigoj);
                waveCome = true;
            }
            //Ʈ���� ��� ���� ��ȯ �Լ� 
            if (waveEnd)
            {
                YA_Phase3String.Instance.bigwave = false;
                YA_Phase3String.Instance.upend = false;
                anim.SetBool("Attack", false);
                animTime = false;
                waveCome = false;
                state = State.Thunder;
            }
        }
    }

    float currentTime=0;
    public float creatThunderTime = 2f;
    private void ThunderSStaytate()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= creatThunderTime)
        {
            currentTime = 0;
            state = State.Thunder;
        }
    }

    public bool thunderEnd = false;
    int thunderCount = 0;
    private void ThunderState()
    {
        ThunderSound.Play();
            waveEnd = false;
            //����
            //���� ���ӿ�����Ʈ Ʈ�� ->���� �̹��� Ʈ��
            str.SetActive(true);
            YA_Phase3String.Instance.thunder = true;

        if (YA_Phase3String.Instance.upend)
        {
            str.SetActive(false);
            thunderoj.SetActive(true);
            if (!animTime)
            {
                anim.SetBool("Attack", true);
                animTime = true;
            }

            //Ʈ���� ��� �༺ ��ȯ �Լ� 
            if (thunderEnd)
            {
                YA_Phase3String.Instance.thunder = false;
                YA_Phase3String.Instance.upend = false;
                thunderCount++;
                if (thunderCount >= 2)
                {
                    anim.SetBool("Attack", false);
                    animTime = false;
                    thunderCount = 0;
                    state = State.Meteo;
                }
                else
                {
                    thunderEnd = true;
                    state = State.ThunderStay;
                }

            }
        }
        
    }
}
