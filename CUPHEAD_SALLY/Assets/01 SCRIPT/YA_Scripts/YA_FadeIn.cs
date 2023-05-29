using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YA_FadeIn : MonoBehaviour
{
    RectTransform rect;

    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<RectTransform>();
        
    }
    float currentTime = 0;
    public float fadeTime = 2;
    bool fadein;
    float time;
    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= fadeTime)
        {
            if (!fadein)
            {
                time += 0.7f * Time.deltaTime;
                Vector3 rectVector = rect.sizeDelta;
                rectVector.x = Mathf.Lerp(-1, 110, time);
                rectVector.y = Mathf.Lerp(-1, 110, time);
                rect.sizeDelta = rectVector;
                if (rect.sizeDelta.x >= 110)
                {
                    currentTime = 0;
                    fadein = true;
                    gameObject.SetActive(false);
                }

            }
        }
         
    }
}
