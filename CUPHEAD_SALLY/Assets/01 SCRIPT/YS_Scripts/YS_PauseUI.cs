using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YS_PauseUI : MonoBehaviour
{
    Image pauseUI, black, continueButton, retryButton, optionButton, returnButton;
    // 페이드
    float value, value2;
    float fade = 0.3f; // 페이드 되는 정도
    bool b_fade = true; // 페이드 켜기

    // Start is called before the first frame update
    void Start()
    {
        pauseUI = GetComponent<Image>();
        black = GameObject.Find("BlackImage").GetComponent<Image>();
        // 버튼 지정
        continueButton = GameObject.Find("Continue").GetComponent<Image>();
        retryButton = GameObject.Find("Retry").GetComponent<Image>();
        optionButton = GameObject.Find("Option").GetComponent<Image>();
        returnButton = GameObject.Find("Return").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && black.color.a == 0)
        {
            On();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && black.color.a == fade)
        {
            Off();
        }

        if (pauseUI.enabled == true && b_fade == true)
        {
            FadeIn();
        }
        else if(b_fade == false)
        {
            FadeOut();
        }
    }

    public void On()
    {
        pauseUI.enabled = true;
        continueButton.enabled = true;
        retryButton.enabled = true;
        optionButton.enabled = true;
        returnButton.enabled = true;
        b_fade = true;
        value = 0;

        // 시간이 멈추게
        Time.timeScale = 0f;
    }

    public void Off()
    {
        pauseUI.enabled = false;
        continueButton.enabled = false;
        retryButton.enabled = false;
        optionButton.enabled = false;
        returnButton.enabled = false;
        b_fade = false;
        value = 0;

        // 시간이 흐르게
        Time.timeScale = 1f;
    }

    void FadeIn()
    {
        // 페이드 인 (배경 및 UI 동시에)
        value += 0.1f;
        value2 += 0.1f;
        Color color = black.color;
        Color color2 = pauseUI.color;
        color.a = Mathf.Lerp(0, fade, value);
        color2.a = Mathf.Lerp(0, 1, value2);
        black.color = color;
        pauseUI.color = color2;
        continueButton.color = color2;
        retryButton.color = color2;
        optionButton.color = color2;
        returnButton.color = color2;
    }

    void FadeOut()
    {
        // 페이드 아웃
        value += 0.1f;
        value2 += 0.1f;
        Color color = black.color;
        Color color2 = pauseUI.color;
        color.a = Mathf.Lerp(fade, 0, value);
        color2.a = Mathf.Lerp(1, 0, value2);
        black.color = color;
        pauseUI.color = color2;
        continueButton.color = color2;
        retryButton.color = color2;
        optionButton.color = color2;
        returnButton.color = color2;
    }
}
