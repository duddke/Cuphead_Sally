using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YA_Star : MonoBehaviour
{
    // 패링 타이밍 (영수)
    float currentTime;
    bool b_parring = false;
    //
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 패링(영수)
        Parring();
        //
    }
    // 패링 (영수)
    void Parring()
    {
        if (b_parring == true)
        {
            currentTime += Time.deltaTime;

            // 패링 타이밍
            if (currentTime <= 0.1f)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    //Destroy(gameObject);
                    currentTime = 0;
                    b_parring = false;
                }
            }
            else
            {
                YS_PlayerHealth.Instance.HP--;
                //Destroy(gameObject);
                currentTime = 0;
                b_parring = false;
            }
        }
    }
    //
}
