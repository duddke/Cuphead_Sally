using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YA_Thunder : MonoBehaviour
{
    //선더공장 만들기
    public GameObject thunderFactory;
    float currentTime = 0;
    public float creTime = 0.5f;
    float thunderCount=0;

    public int minThunderCount = 2;
    public int maxThunderCount = 4;

    //선더 만들기
    //이걸 한 번에 2~4 개 시간차 0.5초 정도

    // Start is called before the first frame update
    void Start()
    {
        thunderFactory=Resources.Load<GameObject>("YA_Prefabs/Thunder");

    }

    GameObject thunder;
    // Update is called once per frame
    void Update()
    {
        
        int ran = Random.Range(minThunderCount, maxThunderCount + 1);
        currentTime += Time.deltaTime;
        if(currentTime>= creTime)
        {
            if(thunderCount<ran)
            {
                currentTime = 0;
                thunder = Instantiate(thunderFactory);
                //x값을 x스크린 값 범위내에서 랜덤 값 받아서 나오기
                int ran1 = Random.Range(1, 11);
                thunder.transform.position = GameObject.Find("TS" + ran1).transform.position;
                thunderCount++;
            }
        }
        if(thunderCount>=ran)
        {
            thunderCount = 0;
            YA_EnemyPhase3.Instance.thunderEnd = true;
            gameObject.SetActive(false);
        }
        /*currentTime += Time.deltaTime;
        if (currentTime >= creTime)
        {
            YA_EnemyPhase3.Instance.thunderEnd = true;
            currentTime = 0;
        }*/
    }
}
