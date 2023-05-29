using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YS_ShootRotate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.S) == false) // 아래로 몸을 숙였을 때는 움직임x
        {
            if (Input.GetKey(KeyCode.W))
            {
                transform.localPosition = new Vector3(0.6f, 1.85000002f, 0);
                transform.localEulerAngles = new Vector3(0, 0, 90);
            }

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.anyKey == false)
            {
                transform.localPosition = new Vector3(1.3f, 0.5f, 0);
                transform.localEulerAngles = new Vector3(0, 0, 0);
            }

            if ((Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W)) || (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.W)))
            {
                transform.localPosition = new Vector3(1.41f, 1.22f, 0);
                transform.localEulerAngles = new Vector3(0, 0, 30);
            }
        }
    }
}
