using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class YA_FadeInOrig : MonoBehaviour
{
    Image rect;

    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<Image>();

    }

    public float fadeTime = 2;
    float time;
    // Update is called once per frame
    void Update()
    {
        time += 0.7f * Time.deltaTime;
        Color rectVector = rect.color;
        rectVector.a = Mathf.Lerp(1, 0, time);
        rect.color = rectVector;
        if (rect.color.a <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
