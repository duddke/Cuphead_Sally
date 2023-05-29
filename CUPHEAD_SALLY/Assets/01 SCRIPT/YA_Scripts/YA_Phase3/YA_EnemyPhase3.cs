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

    // 행성


    // 파도
    //별 나오자마자 왼쪽에서 오른쪽으로 파도가 걸어옴
    //파도가 별이 있는 위치를 지나가면 별 슝 옹ㄹ라감

    //번개
    //파도가 들어가고 2초 뒤 라이팅 문구가 뜸
    //문구 들어가자마자 왼쪽에서 번개 등장, 플레이어 위치로 갔다가 반대편으로
    //1초 뒤 또 번개가 나옴 (나오는 위치는 랜덤인듯)(갯수도 랜덤. 2~4개

    //순서 반복
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
    //스크린 x 저장값
    float xScreen;
    //스크린 y 저장값
    float yScreen;
    MeshRenderer mesh;
    Vector3 dir;
    public float SPEED = 8;
    float speed=0;
    CharacterController cc;
    public GameObject meteooj;
    public GameObject bigoj;
    public GameObject thunderoj;

    //다이 이미지 변경
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
        //콜라이더 켜기
        GetComponent<Collider>().enabled = true;
        //hp 켜기
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
        //메테오 
        //문구 게임오브젝트 트루 ->메테오 이미지 트루
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
            //트루일 경우 파도 전환 함수 
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
        //파도
        //문구 게임오브젝트 트루 ->파도 이미지 트루
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
            //트루일 경우 번개 전환 함수 
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
            //번개
            //문구 게임오브젝트 트루 ->번개 이미지 트루
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

            //트루일 경우 행성 전환 함수 
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
