using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YA_MeteoBroken : MonoBehaviour
{
    //��ũ�� x ���尪
    float xScreen;
    //��ũ�� y ���尪
    float yScreen;

    // Start is called before the first frame update
    void Start()
    {
        yScreen = Camera.main.orthographicSize * 2;
        xScreen = yScreen * Camera.main.aspect;
    }

    public float speed = 10;
    float currentTime = 0;
    public float upTime = 0.7f;
    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= upTime)
        {
            transform.position += Vector3.up * speed * Time.deltaTime;
            if (transform.position.y - 10.1f >= yScreen)
            {
                currentTime = 0;
                gameObject.SetActive(false);
            }
        }
    }
}
