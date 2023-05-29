using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class YA_StartUI : MonoBehaviour
{
    Color textA;
    float time = 0;

    public GameObject FadeOutObject;
    bool FooStart;

    bool fadein=true;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKey)
        {
            FooStart = true;
        }
        if (FooStart)
        {
            FadeOutObject.SetActive(true);
            FadeOutObject.GetComponent<YA_FadeOut>().FadeSceneCh(1);//게임매니저에 이거 복붙하고 씬번호 엔딩씬번호로 기입하기
        }
        if(fadein) FadeIn();
        else FadeOut();
    }
    void FadeIn()
    {
        textA.a = Mathf.Lerp(0, 1, time);
        time += Time.deltaTime * 0.8f;
        GetComponent<TextMeshProUGUI>().color = textA;
        if (textA.a >= 0.9)
        {
            time = 0;
            fadein = false;
        }
    }
    void FadeOut()
    {
        textA.a = Mathf.Lerp(1, 0, time);
        time += Time.deltaTime * 0.8f;
        GetComponent<TextMeshProUGUI>().color = textA;
        if (textA.a <= 0.1)
        {
            time = 0;
            fadein = true;
        }
    }
}
