using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //������ ����
    //������ �� ���ʹ� ���� ����
    //1������ ���� �� �غ����
    //
    //������ ������ ���ʹ� ó�� �� ���� ��
    //�÷��̾� ��� �� ���ӿ���
    public GameObject FadeOut;

    #region ���¸ӽŰ� Static
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

    #region Ŀ��
    public GameObject curtain;
    Vector3 curtainPos;
    float speed = 0;
    float SPEED = 20;
    #endregion

    //��ũ�� x ���尪
    float xScreen;
    //��ũ�� y ���尪
    float yScreen;

    #region ������ ����
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

    #region ��ŸƮ ������

    float startTime = 3;

    public bool first;
    public bool two;
    public bool three;
    public bool four;
    #endregion

    // ����
    // ���� �� Ÿ�̸�
    double sec, min;
    public Text time;
    // ���� �� HP���ʽ�
    float hpBonus = 0;
    public Text hp_bonus;
    // ���� �� �и�
    float parring = 0;
    public Text parry;
    // ���� �� ���۹���
    float superMeter = 0;
    public Text super;
    // ���� �� ��ų ����
    string skillLevel = "�ڡڡ�";
    public Text slevel;
    // ���� �� �׷��̵�
    string grade = "B";
    public Text abc;
    // ����
    public GameObject readyWallop;
    // ���
    public GameObject knockOut;
    // �÷��̾� �ִϸ��̼�
    Animator p_anim;
    //

    // Start is called before the first frame update
    void Start()
    {
        yScreen = Camera.main.orthographicSize * 2;
        xScreen = yScreen * Camera.main.aspect;
        curtainPos = curtain.transform.position;

        // �÷��̾� �ִϸ��̼� (����)
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

        // �ð� ����(����)
        if (gameState == GameState.PhaseOne || first == true)
        {
            sec += Time.deltaTime;
        }
        // ������ ���(HP����)
        if(Input.GetKeyDown(KeyCode.P))
        {
            YS_PlayerHealth.Instance.HP++;
        }
        //
    }

    //���� ��ŸƮ ��ư�� ������ ����ȯ �� ����
    // ���(Ŀư)
    // ���ʹ�(������ �� ���ʹ�) �����ʿ�
    // �÷��̾� ���ʿ�
    //2�� �� �ΰ� ��ŸƮ�� ��ȯ

    private void ReadyState()
    {
        if (GameObject.Find("FadeIn") == null)
        {
            currentTime += Time.deltaTime;
            if (currentTime >= readyTime)
            {
                currentTime = 0;
                gameState = GameState.Start;

                // ��Ʈ�� �ִϸ��̼�
                p_anim.SetTrigger("Intro");
            }
        }
    }


    //���ʹ� ���� ���� ���ϸ��̼� ����
    // Ŀư �ö�
    //���(��ȸ)
    //������1 ������Ʈ �ѱ�
    //3�� �� ���� ����

    bool curtainSound;
    private void StartState()
    {
        //Ŀ�� �ö󰡱�
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
            // ���� �ѱ�(����)
            readyWallop.SetActive(true);
            //
        }
        //������ �ð��� �Ǹ�
        if (currentTime >= startTime)
        {
            curtainSound = false;
            currentTime = 0;
            //1������
            if (!first)
            {
                gameState = GameState.PhaseOne;
            }
            //2������
            if (first)
            {
                Phase1.SetActive(false);
                Phase2.SetActive(true);
                gameState = GameState.PhaseTwo;
            }
            //3������
            if (two)
            {
                Phase2.SetActive(false);
                Phase3.SetActive(true);
                gameState = GameState.PhaseThree;
            }
            //4������
            if (three)
            {
                Phase3.SetActive(false);
                Phase4.SetActive(true);
                gameState = GameState.PhaseFour;
            }
        }
    }

    //������1 ��ũ��Ʈ �ѱ�
    //���ʹ� �ǰ� 140�� �Ǹ�
    //���ʹ� ����
    //Ŀư ������
    //3�� �� 2������ ��ȯ

    private void OneState()
    {
        //1������ �ѱ�
        Sally_P1.GetComponent<YA_EnemyPhase1>().enabled = true;
        if(GameObject.Find("Sally_P1") == null)
        {
            //������ �ٲٴ� ���·� ��ȯ
            gameState = GameState.PhaseChange;
        }
    }

    //Ŀư �ö�
    //2�� ��
    //������2 ������Ʈ �ѱ�(������ ��ũ��Ʈ ���ִ� ����)
    //���ʹ� �ǰ� 80�� �Ǹ�
    //���ʹ� ����
    //Ŀư ������
    //3�� �� 3������ ��ȯ

    private void TwoState()
    {
        //2������ �ѱ�
        Sally_P2.GetComponent<YA_EnemyPhase2>().enabled = true;
        if (GameObject.Find("Sally_P2")==null)
        {
            //Sally_P2.GetComponent<YA_EnemyPhase2>().enabled = false;
            //������ �ٲٴ� ���·� ��ȯ
            gameState = GameState.PhaseChange;
        }
    }

    //Ŀư �ö�
    //2�� ��
    //������3 ������Ʈ �ѱ�(������ ��ũ��Ʈ ���ִ� ����)
    //���ʹ� �ǰ� 20�� �Ǹ�
    //���ʹ� ����
    //Ŀư ������
    //3�� �� 3������ ��ȯ

    private void ThreeState()
    {
        //3������ �ѱ�
        Sally_P3.GetComponent<YA_EnemyPhase3>().enabled = true;
        if (Sally_P3.GetComponent<YA_EnemyHP>().HP <= 0)
        {
            //������ �ٲٴ� ���·� ��ȯ
            gameState = GameState.PhaseChange;
        }
    }

    //Ŀư �ö�
    //2�� ��
    //������4 ������Ʈ �ѱ�(������ ��ũ��Ʈ ���ִ� ����)
    //���ʹ� �ǰ� 0�� �Ǹ�
    //ȭ�� ���̵� �ƿ�
    //���İ� Ǯ �Ǹ� �������� ��ȯ
    bool ending;
    private void FourState()
    {
        //Phase3.SetActive(false);
        //4������ �ѱ�
        Phase4.GetComponent<YA_EnemyPhase4>().enabled = true;
        if (Sally_P4.GetComponent<YA_EnemyHP>().HP <= 0)
        {
            // ���� �ð� �־��ֱ�(����)
            YS_DataBox.data.gameTime = sec;
            //

            // ��ƿ� �ѱ�(����)
            knockOut.SetActive(true);
            //

            ending = true;
            //������ �ٲٴ� ���·� ��ȯ
            gameState = GameState.Ending;
        }
    }

    //�÷��̾� HP�� 0�� �Ǹ� �ٲٰ� �� ��
    //���ӿ��� ������ ����
    //��Ʈ���� �� ������ ����
    private void GGState()
    {
        print("GG");
    }

    //���� �ð��� ���� �� ���̵� �ƿ�����
    //���������� �Ѱ��ִ� ����ȯ
    float nextTime;
    private void EndingState() // ����
    {
        nextTime += Time.deltaTime;

        // ������ �Ҵ�
        sec = Math.Truncate(YS_DataBox.data.gameTime); // ���� �����ð� �Ҵ�!
        min = (int)sec / 60;
        sec = sec % 60;
        hpBonus = YS_DataBox.data.p_hp; // �÷��̾� ���� ü�� �Ҵ�!
        parring = YS_DataBox.data.p_parry; // �÷��̾� �и� Ƚ�� �Ҵ�!
        if(parring > 3)
        {
            parring = 3;
        }
        superMeter = YS_DataBox.data.p_super; // �÷��̾� �ʻ�� ������ ���� �Ҵ�!
        if(superMeter > 6)
        {
            superMeter = 6;
        }

        // ��� ������ �����Ͽ� ��� ���
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
            // Ÿ�̸�
            time.text = string.Format("{0:00}:{1:00}", min, sec);
        }
        if(nextTime > 2)
        {
            // HP���ʽ�
            hp_bonus.text = "" + hpBonus + "/3";
        }
        if(nextTime > 3)
        {
            // �и�
            parry.text = "" + parring + "/3";
        }
        if(nextTime > 4)
        {
            // ���۹���
            super.text = "" + superMeter + "/6";
        }
        if(nextTime > 5)
        {
            // ��ų ����
            slevel.text = "" + skillLevel;
        }
        if(nextTime > 6)
        {
            // �׷��̵�
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
            //2������
            if (first)
            {
                Phase1BG.SetActive(false);
                Phase2BG.SetActive(true);
            }
            //3������
            if (two)
            {
                Phase2BG.SetActive(false);
                Phase3BG.SetActive(true);
            }
            //4������
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
