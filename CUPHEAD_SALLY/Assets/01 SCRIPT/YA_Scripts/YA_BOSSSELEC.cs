using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;
using System;

public class YA_BOSSSELEC : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Animator anim;

    public GameObject NomakeBoss;

    public GameObject text;
    Vector3 sca;
    public float currentTime = 0;
    public float textTime=2;

    bool moving;
    float runningTime = 0;
    float xPos = 0;

    public float speed = 10;
    public float length = 5;

    Vector3 change;

    bool click;

    public GameObject FadeOutObject;
    bool FooStart;
    bool FooStart1;

    public GameObject Size;

    void Start()
    {
        change= new Vector3(1.5f, 1.5f, 1.5f);
        sca = transform.localScale;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(!click)
       Size.transform.localScale = change;
        
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (!click)
            Size.transform.localScale = sca;
    }
    public void BossScene()
    {
        if (GameObject.Find("FadeIn") == null)
        {
            anim.Play("Boss01");
            click = true;
            GetComponent<AudioSource>().Play();
        }
    }
    public void OnClickBACK()
    {
        if (GameObject.Find("FadeIn") == null)
        {
            FooStart = true;
        }
    }

    bool texttt;
    void Update()
    {
        //문구 등장
        if (texttt)
        {
            currentTime += Time.deltaTime;
            if (currentTime >= 1)
            {
                    moving = false;
                if (currentTime >= textTime)
                {
                    texttt = false;
                    text.SetActive(false);
                    currentTime = 0;
                }
            }
        }
        if(moving)
        {
            runningTime += Time.deltaTime*speed;
            xPos = MathF.Sin(runningTime) * length;
            NomakeBoss.transform.position = new Vector2(transform.position.x+xPos, transform.position.y);
        }
        if (click)
        {
            currentTime += Time.deltaTime;
            transform.localScale = sca;
            if (currentTime >= 0.1)
            {
                transform.localScale = change;
                if (currentTime >= 0.5)
                {
                    if(currentTime>=1)
                    FooStart1 = true;
                }
            }
        }
        if (FooStart1)
        {
            FadeOutObject.SetActive(true);
            FadeOutObject.GetComponent<YA_FadeOut>().FadeSceneCh(2);
        }
        if (FooStart)
        {
            FadeOutObject.SetActive(true);
            FadeOutObject.GetComponent<YA_FadeOut>().FadeSceneCh(0);
        }
    }

    //버튼을 누르면
    //버튼이 좌우로 흔들리게 하고 싶다
    //버튼이 좌로 이동햇다가
    //버튼이 우로 이동햇다가 (x값변화

    public void OnClickNoMake()
    {
        if (GameObject.Find("FadeIn") == null)
        {
            texttt = true;
            text.SetActive(true);
            moving = true;
            GetComponent<AudioSource>().Play();
        }
    }
}
