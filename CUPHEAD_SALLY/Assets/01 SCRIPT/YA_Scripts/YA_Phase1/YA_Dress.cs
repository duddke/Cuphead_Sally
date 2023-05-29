using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YA_Dress : MonoBehaviour
{

    public GameObject Dress;
    float currentTime = 0;
    public float dressTime = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= dressTime)
        {
            Dress.SetActive(true);
            if(currentTime >=dressTime+0.2f)
            {
                Dress.SetActive(false);
                GetComponent<YA_Dress>().enabled = false;
                currentTime = 0;
            }
        }
    }
}
