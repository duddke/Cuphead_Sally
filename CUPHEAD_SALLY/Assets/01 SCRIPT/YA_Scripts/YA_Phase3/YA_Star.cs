using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YA_Star : MonoBehaviour
{
    // �и� Ÿ�̹� (����)
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
        // �и�(����)
        Parring();
        //
    }
    // �и� (����)
    void Parring()
    {
        if (b_parring == true)
        {
            currentTime += Time.deltaTime;

            // �и� Ÿ�̹�
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
