using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class YA_StartManager : MonoBehaviour
{

    public GameObject startText;
    public float currentTime = 0;
    public float textTime=5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("FadeIn") == null)
        {
            currentTime += Time.deltaTime;
            if (currentTime >= textTime)
            {
                startText.SetActive(true);
                currentTime = 0;
            }
        }
    }


}
