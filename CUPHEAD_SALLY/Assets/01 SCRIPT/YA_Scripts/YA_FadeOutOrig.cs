using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class YA_FadeOutOrig : MonoBehaviour
{
    Image rect;

    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<Image>();

    }
    float currentTime = 0;
    public float fadeTime = 2;
    bool fadeout;
    float time;
    // Update is called once per frame
    void Update()
    {

    }

    public void FadeSceneCh(int num)
    {
        time += 0.2f * Time.deltaTime;
        Color rectVector = rect.color;
        rectVector.a = Mathf.Lerp(0, 1, time);
        rect.color = rectVector;
        if (rect.color.a >= 1)
        {
            currentTime += Time.deltaTime;
            if (currentTime >= fadeTime)
            {
                currentTime = 0;
                SceneManager.LoadScene(num);
                //gameObject.SetActive(false);
            }
        }
    }
}
