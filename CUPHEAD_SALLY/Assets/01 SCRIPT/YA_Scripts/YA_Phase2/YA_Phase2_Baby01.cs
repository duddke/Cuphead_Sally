using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YA_Phase2_Baby01 : MonoBehaviour
{
    // 각 창문마다 오브젝트 생성(빈)(위치만 받아올)
    public GameObject W1_1;
    public GameObject W1_2;
    public GameObject W1_3;
    public GameObject W1_4;
    public GameObject W1_5;
    float cruTime;
    float mTime = 0.5f;
    float fadeTime = 1;
    bool milkCount;
    int ran;
    public static bool baby;

    GameObject milkFactory;

    // 랜덤으로 창문에 생김
    // 젖병 날리가
    // 사라짐

    //젖병: 아래로 내려가기
    // 바닥에 닿으면 사라지기
    private void Awake()
    {
        milkFactory = Resources.Load<GameObject>("YA_Prefabs/Milk");
    }

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if(!baby)
        {
            //젖병을 날리고
            //1초 후 사라진다
            //랜덤 수를 뽑아서 
            ran = Random.Range(0, 5);
            //그 수에 해당하는 창문위치에 간다
            if (ran == 0)
            {
                ran = 0;
                transform.position = W1_1.transform.position;
                baby = true;
            }
            else if (ran == 1)
            {
                ran = 0;
                transform.position = W1_2.transform.position;
                baby = true;
            }
            else if (ran == 2)
            {
                ran = 0;
                transform.position = W1_3.transform.position;
                baby = true;
            }
            else if (ran == 3)
            {
                ran = 0;
                transform.position = W1_4.transform.position;
                baby = true;
            }
            else if (ran == 4)
            {
                ran = 0;
                transform.position = W1_5.transform.position;
                baby = true;
            }
        }
        cruTime += Time.deltaTime;
        if(cruTime>=mTime)
        {
            if(!milkCount)
            {
                GameObject milk = Instantiate(milkFactory);
                milk.transform.position = transform.position;
                milkCount = true;
            }
            if(cruTime>=fadeTime)
            {
                cruTime = 0;
                milkCount = false;
                gameObject.SetActive(false);
            }
        }
    }
}
