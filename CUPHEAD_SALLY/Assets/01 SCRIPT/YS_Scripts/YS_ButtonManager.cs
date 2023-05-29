using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class YS_ButtonManager : MonoBehaviour
{
    YS_PauseUI ys_pUI;

    private void Start()
    {
        ys_pUI = GameObject.Find("PauseUI").GetComponent<YS_PauseUI>();
    }

    public void OnClickContinue()
    {
        ys_pUI.Off();
    }

    public void OnClickRetry()
    {
        // ���� ��
        SceneManager.LoadScene("MainScene");
        // �ð��� �ٽ� �帣��
        Time.timeScale = 1;
    }

    public void OnClickOption()
    {

    }

    public void OnClickReturn()
    {
        // ���ӽ��� ��
        SceneManager.LoadScene("StartScene");
        // �ð��� �ٽ� �帣��
        Time.timeScale = 1;
    }

    public void OnClickQuit()
    {
        Application.Quit();
    }
}
