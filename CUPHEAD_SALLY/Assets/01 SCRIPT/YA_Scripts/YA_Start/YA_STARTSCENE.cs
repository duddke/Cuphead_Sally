using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class YA_STARTSCENE : MonoBehaviour
{
    public void BossScene()
    {
        SceneManager.LoadScene(1);
    }
    public void OnClickQUIT()
    {
        Application.Quit();
        Debug.Log("종료버튼");
    }
}
