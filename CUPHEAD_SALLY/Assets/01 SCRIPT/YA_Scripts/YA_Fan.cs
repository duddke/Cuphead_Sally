using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YA_Fan : MonoBehaviour
{
    public Transform Sally;
    GameObject fanFactory;
    Transform target;
    //스크린 x 저장값
    float xScreen;
    //스크린 y 저장값
    float yScreen;
    public CharacterController cc;

    public float jumppower = 20;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponentInParent<CharacterController>();
        yScreen = Camera.main.orthographicSize * 2;
        xScreen = yScreen * Camera.main.aspect;
        fanFactory = Resources.Load<GameObject>("YA_Prefabs/Fan");
    }

    private void Update()
    {
        //문제 발생! 스프라이트만 움직이고 에너미가 움직이지 않음!
        //해결!
        // 가끔 바닥으로 내려오질 않음(cc가 켜지면서 방해받는듯!
        // 트리거로 멈추기로 하자...cc죽여
        //그런데 cc가...,.,.,바닥 뚫어버림
        if (FanJump)
        {
            //cc.enabled=false;
            Sally.position += Vector3.up * jumppower * Time.deltaTime;
            if (transform.position.y >= yScreen - 3)
                FanJump = false;
        }
        
    }

    bool FanJump;
    public void GetFanJump()
    {
        FanJump = true;
    }
    // Update is called once per frame
    public void GetFan()
    {
        GameObject fan = Instantiate(fanFactory);
        fan.transform.position = Sally.position;
    }

    public static bool FanDown;
    public void GetFanDown()
    {
        FanDown = true;
    }
}
