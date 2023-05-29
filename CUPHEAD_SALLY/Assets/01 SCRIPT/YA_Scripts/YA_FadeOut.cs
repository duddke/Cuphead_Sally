using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class YA_FadeOut : MonoBehaviour
{
    RectTransform rect;
    Vector3 rectVector;

    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<RectTransform>();
        rectVector = rect.sizeDelta;

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
        time += 0.7f * Time.deltaTime;
        rectVector.x = Mathf.Lerp(110, 0, time);
        rectVector.y = Mathf.Lerp(110, 0, time);
        rect.sizeDelta = rectVector;
        if (rect.sizeDelta.x <= 0)
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
