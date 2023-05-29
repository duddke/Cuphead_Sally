using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YA_Fan : MonoBehaviour
{
    public Transform Sally;
    GameObject fanFactory;
    Transform target;
    //��ũ�� x ���尪
    float xScreen;
    //��ũ�� y ���尪
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
        //���� �߻�! ��������Ʈ�� �����̰� ���ʹ̰� �������� ����!
        //�ذ�!
        // ���� �ٴ����� �������� ����(cc�� �����鼭 ���ع޴µ�!
        // Ʈ���ŷ� ���߱�� ����...cc�׿�
        //�׷��� cc��...,.,.,�ٴ� �վ����
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
