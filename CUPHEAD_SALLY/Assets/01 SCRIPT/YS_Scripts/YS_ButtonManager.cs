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
        // ª¯∏Æ æ¿
        SceneManager.LoadScene("MainScene");
        // Ω√∞£¿Ã ¥ŸΩ√ »Â∏£∞‘
        Time.timeScale = 1;
    }

    public void OnClickOption()
    {

    }

    public void OnClickReturn()
    {
        // ∞‘¿”Ω√¿€ æ¿
        SceneManager.LoadScene("StartScene");
        // Ω√∞£¿Ã ¥ŸΩ√ »Â∏£∞‘
        Time.timeScale = 1;
    }

    public void OnClickQuit()
    {
        Application.Quit();
    }
}
