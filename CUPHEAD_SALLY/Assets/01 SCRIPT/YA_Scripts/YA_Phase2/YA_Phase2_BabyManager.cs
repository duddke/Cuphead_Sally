using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YA_Phase2_BabyManager : MonoBehaviour
{
    //랜덤 수를 받아서 3초에 한 번 아기 보이게 하기
    float currentTime;
    float creatTime = 5;
    GameObject Baby01;
    GameObject Baby02;

    private void Awake()
    {
        


    }
    // Start is called before the first frame update
    void Start()
    {
        Baby01 = GameObject.Find("Baby01");
        Baby02 = GameObject.Find("Baby02");
        Baby01.SetActive(false);
        Baby02.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //3초가 지나묜
        currentTime += Time.deltaTime;
        if (currentTime >= creatTime)
        {
            //랜덤 수를 받는다
            int ran = Random.Range(0, 2);
            if (ran == 0)
            {
            //0이면 아기여자아이 셋액티브 트루
                Baby01.SetActive(true);
                currentTime = 0;
                YA_Phase2_Baby01.baby = false;
            }
            else if (ran==1)
            {
                //1이면 남자아기 위에 처럼
                Baby02.SetActive(true);
                currentTime = 0;
                YA_Phase2_Baby02.baby = false;
            }
            //3초 뒤 다시 뽑는다
        }
        
    }
}
