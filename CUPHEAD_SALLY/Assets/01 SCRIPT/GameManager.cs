using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //페이즈 관리
    //페이즈 별 에너미 등장 관리
    //1페이즈 시작 전 준비상태
    //
    //마지막 페이즈 에너미 처리 후 게임 끝
    //플레이어 사망 시 게임오버
    public GameObject FadeOut;

    #region 상태머신과 Static
    public static GameManager Instance;
    public enum GameState
    {
        Ready,
        Start,
        PhaseChange,
        PhaseOne,
        PhaseTwo,
        PhaseThree,
        PhaseFour,
        GameOver,
        Ending
    }
    public GameState gameState = GameState.Ready;
    #endregion

    private void Awake()
    {
        Instance = this;
    }

    #region 커텐
    public GameObject curtain;
    Vector3 curtainPos;
    float speed = 0;
    float SPEED = 20;
    #endregion

    //스크린 x 저장값
    float xScreen;
    //스크린 y 저장값
    float yScreen;

    #region 페이즈 관리
    public GameObject Sally_P1;
    public GameObject Sally_P2;
    public GameObject Sally_P3;
    public GameObject Sally_P4;

    public GameObject Phase1;
    public GameObject Phase2;
    public GameObject Phase3;
    public GameObject Phase4;

    public GameObject Phase1BG;
    public GameObject Phase2BG;
    public GameObject Phase3BG;
    public GameObject Phase4BG;
    #endregion

    float currentTime = 0;
    float readyTime = 2;

    #region 스타트 페이즈

    float startTime = 3;

    public bool first;
    public bool two;
    public bool three;
    public bool four;
    #endregion

    // 영수
    // 엔딩 씬 타이머
    double sec, min;
    public Text time;
    // 엔딩 씬 HP보너스
    float hpBonus = 0;
    public Text hp_bonus;
    // 엔딩 씬 패리
    float parring = 0;
    public Text parry;
    // 엔딩 씬 슈퍼미터
    float superMeter = 0;
    public Text super;
    // 엔딩 씬 스킬 레벨
    string skillLevel = "★★★";
    public Text slevel;
    // 엔딩 씬 그레이드
    string grade = "B";
    public Text abc;
    // 레디꼬
    public GameObject readyWallop;
    // 녹앗
    public GameObject knockOut;
    // 플레이어 애니메이션
    Animator p_anim;
    //

    // Start is called before the first frame update
    void Start()
    {
        yScreen = Camera.main.orthographicSize * 2;
        xScreen = yScreen * Camera.main.aspect;
        curtainPos = curtain.transform.position;

        // 플레이어 애니메이션 (영수)
        p_anim = GameObject.Find("Player").GetComponentInChildren<Animator>();
        //
    }

    // Update is called once per frame
    void Update()
    {
        if(ending)
        {
            FadeOut.SetActive(true);
            FadeOut.GetComponent<YA_FadeOutOrig>().FadeSceneCh(4);
        }
        switch (gameState)
        {
            case GameState.Ready:
                ReadyState();
                break;
            case GameState.Start:
                StartState();
                break;
            case GameState.PhaseOne:
                OneState();
                break;
            case GameState.PhaseTwo:
                TwoState();
                break;
            case GameState.PhaseThree:
                ThreeState();
                break;
            case GameState.PhaseFour:
                FourState();
                break;
            case GameState.GameOver:
                GGState();
                break;
            case GameState.Ending:
                EndingState();
                break;
            case GameState.PhaseChange:
                PhaseChange();
                break;
        }

        // 시간 누적(영수)
        if (gameState == GameState.PhaseOne || first == true)
        {
            sec += Time.deltaTime;
        }
        // 개발자 모드(HP증가)
        if(Input.GetKeyDown(KeyCode.P))
        {
            YS_PlayerHealth.Instance.HP++;
        }
        //
    }

    //게임 스타트 버튼을 누르면 씬전환 후 시작
    // 배경(커튼)
    // 에너미(페이즈 원 에너미) 오른쪽에
    // 플레이어 왼쪽에
    //2초 텀 두고 스타트로 전환

    private void ReadyState()
    {
        if (GameObject.Find("FadeIn") == null)
        {
            currentTime += Time.deltaTime;
            if (currentTime >= readyTime)
            {
                currentTime = 0;
                gameState = GameState.Start;

                // 인트로 애니메이션
                p_anim.SetTrigger("Intro");
            }
        }
    }


    //에너미 착장 변경 에니메이션 진행
    // 커튼 올라감
    //배경(교회)
    //페이즈1 오브젝트 켜기
    //3초 후 게임 시작

    bool curtainSound;
    private void StartState()
    {
        //커텐 올라가기
        speed = SPEED;
        currentTime += Time.deltaTime;
        curtain.transform.position += Vector3.up * speed * Time.deltaTime;
        if (!curtainSound)
        {
            curtain.GetComponent<AudioSource>().Play();
            curtainSound = true;
        }
        if(curtain.transform.position.y>=yScreen+10)
        {
            speed = 0;
            // 레디꼬 켜기(영수)
            readyWallop.SetActive(true);
            //
        }
        //시작할 시간이 되면
        if (currentTime >= startTime)
        {
            curtainSound = false;
            currentTime = 0;
            //1페이즈
            if (!first)
            {
                gameState = GameState.PhaseOne;
            }
            //2페이즈
            if (first)
            {
                Phase1.SetActive(false);
                Phase2.SetActive(true);
                gameState = GameState.PhaseTwo;
            }
            //3페이즈
            if (two)
            {
                Phase2.SetActive(false);
                Phase3.SetActive(true);
                gameState = GameState.PhaseThree;
            }
            //4페이즈
            if (three)
            {
                Phase3.SetActive(false);
                Phase4.SetActive(true);
                gameState = GameState.PhaseFour;
            }
        }
    }

    //페이즈1 스크립트 켜기
    //에너미 피가 140이 되면
    //에너미 퇴장
    //커튼 내려옴
    //3초 뒤 2페이즈 전환

    private void OneState()
    {
        //1페이즈 켜기
        Sally_P1.GetComponent<YA_EnemyPhase1>().enabled = true;
        if(GameObject.Find("Sally_P1") == null)
        {
            //페이지 바꾸는 상태로 변환
            gameState = GameState.PhaseChange;
        }
    }

    //커튼 올라감
    //2초 뒤
    //페이즈2 오브젝트 켜기(페이즈 스크립트 켜있는 상태)
    //에너미 피가 80이 되면
    //에너미 퇴장
    //커튼 내려옴
    //3초 뒤 3페이즈 전환

    private void TwoState()
    {
        //2페이즈 켜기
        Sally_P2.GetComponent<YA_EnemyPhase2>().enabled = true;
        if (GameObject.Find("Sally_P2")==null)
        {
            //Sally_P2.GetComponent<YA_EnemyPhase2>().enabled = false;
            //페이지 바꾸는 상태로 변환
            gameState = GameState.PhaseChange;
        }
    }

    //커튼 올라감
    //2초 뒤
    //페이즈3 오브젝트 켜기(페이즈 스크립트 켜있는 상태)
    //에너미 피가 20이 되면
    //에너미 퇴장
    //커튼 내려옴
    //3초 뒤 3페이즈 전환

    private void ThreeState()
    {
        //3페이즈 켜기
        Sally_P3.GetComponent<YA_EnemyPhase3>().enabled = true;
        if (Sally_P3.GetComponent<YA_EnemyHP>().HP <= 0)
        {
            //페이지 바꾸는 상태로 변환
            gameState = GameState.PhaseChange;
        }
    }

    //커튼 올라감
    //2초 뒤
    //페이즈4 오브젝트 켜기(페이즈 스크립트 켜있는 상태)
    //에너미 피가 0이 되면
    //화면 페이드 아웃
    //알파값 풀 되면 엔딩으로 전환
    bool ending;
    private void FourState()
    {
        //Phase3.SetActive(false);
        //4페이즈 켜기
        Phase4.GetComponent<YA_EnemyPhase4>().enabled = true;
        if (Sally_P4.GetComponent<YA_EnemyHP>().HP <= 0)
        {
            // 누적 시간 넣어주기(영수)
            YS_DataBox.data.gameTime = sec;
            //

            // 녹아웃 켜기(영수)
            knockOut.SetActive(true);
            //

            ending = true;
            //페이지 바꾸는 상태로 변환
            gameState = GameState.Ending;
        }
    }

    //플레이어 HP가 0이 되면 바꾸게 할 것
    //게임오버 유아이 등장
    //리트라이 등 유아이 등장
    private void GGState()
    {
        print("GG");
    }

    //일정 시간이 지난 뒤 페이드 아웃으로
    //엔딩씬으로 넘겨주는 씬전환
    float nextTime;
    private void EndingState() // 영수
    {
        nextTime += Time.deltaTime;

        // 엔딩씬 할당
        sec = Math.Truncate(YS_DataBox.data.gameTime); // 게임 누적시간 할당!
        min = (int)sec / 60;
        sec = sec % 60;
        hpBonus = YS_DataBox.data.p_hp; // 플레이어 남은 체력 할당!
        parring = YS_DataBox.data.p_parry; // 플레이어 패링 횟수 할당!
        if(parring > 3)
        {
            parring = 3;
        }
        superMeter = YS_DataBox.data.p_super; // 플레이어 필살기 게이지 갯수 할당!
        if(superMeter > 6)
        {
            superMeter = 6;
        }

        // 모든 점수를 종합하여 등급 계산
        if(min < 3 && hpBonus >= 3 && parring >= 3 && superMeter >= 6)
        {
            grade = "S";
        }
        else if(min < 4 && hpBonus >= 2 && parring >= 3 && superMeter >= 5)
        {
            grade = "A";
        }
        else if (min < 5 && hpBonus >= 1 && parring >= 2 && superMeter >= 4)
        {
            grade = "B";
        }
        else if (min < 8 || hpBonus >= 1 || parring >= 1 || superMeter >= 3)
        {
            grade = "C";
        }

        if (nextTime > 1)
        {
            // 타이머
            time.text = string.Format("{0:00}:{1:00}", min, sec);
        }
        if(nextTime > 2)
        {
            // HP보너스
            hp_bonus.text = "" + hpBonus + "/3";
        }
        if(nextTime > 3)
        {
            // 패링
            parry.text = "" + parring + "/3";
        }
        if(nextTime > 4)
        {
            // 슈퍼미터
            super.text = "" + superMeter + "/6";
        }
        if(nextTime > 5)
        {
            // 스킬 레벨
            slevel.text = "" + skillLevel;
        }
        if(nextTime > 6)
        {
            // 그레이드
            abc.text = "" + grade;
        }
    }

    GameObject Sally;
    float delayTime = 5;
    private void PhaseChange()
    {
        currentTime += Time.deltaTime;
        curtain.transform.position += Vector3.down * SPEED * Time.deltaTime;
        if (!curtainSound)
        {
            curtain.GetComponent<AudioSource>().Play();
            curtainSound = true;
        }
        if (curtain.transform.position.y <= curtainPos.y)
        {
            speed = 0;
            curtainSound = false;
            curtain.transform.position = curtainPos;
            //2페이즈
            if (first)
            {
                Phase1BG.SetActive(false);
                Phase2BG.SetActive(true);
            }
            //3페이즈
            if (two)
            {
                Phase2BG.SetActive(false);
                Phase3BG.SetActive(true);
            }
            //4페이즈
            if (three)
            {
                Phase3BG.SetActive(false);
                Phase4BG.SetActive(true);
            }
            if (currentTime >= delayTime)
            {
                currentTime = 0;
                gameState = GameState.Start;
            }
        }

    }

}
