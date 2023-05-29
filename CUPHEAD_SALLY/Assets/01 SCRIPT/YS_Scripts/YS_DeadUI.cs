using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YS_DeadUI : MonoBehaviour
{
    Image deadUI, black, deadTextUI, retryButton, returnButton, quitButton, mini;
    public float currentTime = 0;
    // UI 회전 정도
    public float rot;
    // UI 트랜스폼
    RectTransform rectT;

    // Start is called before the first frame update
    void Start()
    {
        deadUI = GameObject.Find("DeadUI").GetComponent<Image>();
        black = GameObject.Find("BlackImage2").GetComponent<Image>();
        deadTextUI = GameObject.Find("DeadTextUI").GetComponent<Image>();
        rectT = GameObject.Find("DeadUI").GetComponent<RectTransform>();
        retryButton = GameObject.Find("DeadRetry").GetComponent<Image>();
        returnButton = GameObject.Find("DeadReturn").GetComponent<Image>();
        quitButton = GameObject.Find("DeadQuit").GetComponent<Image>();
        mini = GameObject.Find("Mini").GetComponent<Image>();

        // UI 회전 정도
        rot = rectT.rotation.eulerAngles.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (YS_PlayerHealth.Instance.HP <= 0)
        {
            currentTime += 0.008f;

            // 죽음글씨 켜기
            deadTextUI.enabled = true;
            // 배경 색 켜기
            black.enabled = true;

            // 일정시간 후에 글씨 페이드 아웃
            if(currentTime >= 2f)
            {
                Color color = deadTextUI.color;
                color.a -= 0.005f;
                deadTextUI.color = color;
            }

            // 페이드 아웃 되면 죽음UI 켜기
            if(deadTextUI.color.a <= 0)
            {
                deadUI.enabled = true;
                returnButton.enabled = true;
                retryButton.enabled = true;
                quitButton.enabled = true;
                mini.enabled = true;
                // 등장 애니메이션(회전)
                if(rot > 1)
                {
                    rot -= 5f;
                    rectT.rotation = Quaternion.Euler(0, 0, rot);
                }
                // 등장 애니메이션(페이드아웃)
                Color color = deadUI.color;
                color.a += 0.03f;
                deadUI.color = color;
                retryButton.color = color;
                returnButton.color = color;
                quitButton.color = color;
                mini.color = color;
            }
        }
    }
}
