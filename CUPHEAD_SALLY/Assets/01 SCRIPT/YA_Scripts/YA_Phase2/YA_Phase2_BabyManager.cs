using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YA_Phase2_BabyManager : MonoBehaviour
{
    //���� ���� �޾Ƽ� 3�ʿ� �� �� �Ʊ� ���̰� �ϱ�
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
        //3�ʰ� ������
        currentTime += Time.deltaTime;
        if (currentTime >= creatTime)
        {
            //���� ���� �޴´�
            int ran = Random.Range(0, 2);
            if (ran == 0)
            {
            //0�̸� �Ʊ⿩�ھ��� �¾�Ƽ�� Ʈ��
                Baby01.SetActive(true);
                currentTime = 0;
                YA_Phase2_Baby01.baby = false;
            }
            else if (ran==1)
            {
                //1�̸� ���ھƱ� ���� ó��
                Baby02.SetActive(true);
                currentTime = 0;
                YA_Phase2_Baby02.baby = false;
            }
            //3�� �� �ٽ� �̴´�
        }
        
    }
}
