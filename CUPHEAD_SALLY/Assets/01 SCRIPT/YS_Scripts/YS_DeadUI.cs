using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YS_DeadUI : MonoBehaviour
{
    Image deadUI, black, deadTextUI, retryButton, returnButton, quitButton, mini;
    public float currentTime = 0;
    // UI ȸ�� ����
    public float rot;
    // UI Ʈ������
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

        // UI ȸ�� ����
        rot = rectT.rotation.eulerAngles.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (YS_PlayerHealth.Instance.HP <= 0)
        {
            currentTime += 0.008f;

            // �����۾� �ѱ�
            deadTextUI.enabled = true;
            // ��� �� �ѱ�
            black.enabled = true;

            // �����ð� �Ŀ� �۾� ���̵� �ƿ�
            if(currentTime >= 2f)
            {
                Color color = deadTextUI.color;
                color.a -= 0.005f;
                deadTextUI.color = color;
            }

            // ���̵� �ƿ� �Ǹ� ����UI �ѱ�
            if(deadTextUI.color.a <= 0)
            {
                deadUI.enabled = true;
                returnButton.enabled = true;
                retryButton.enabled = true;
                quitButton.enabled = true;
                mini.enabled = true;
                // ���� �ִϸ��̼�(ȸ��)
                if(rot > 1)
                {
                    rot -= 5f;
                    rectT.rotation = Quaternion.Euler(0, 0, rot);
                }
                // ���� �ִϸ��̼�(���̵�ƿ�)
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
