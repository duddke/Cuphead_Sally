using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YA_Phase2_Baby01 : MonoBehaviour
{
    // �� â������ ������Ʈ ����(��)(��ġ�� �޾ƿ�)
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

    // �������� â���� ����
    // ���� ������
    // �����

    //����: �Ʒ��� ��������
    // �ٴڿ� ������ �������
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
            //������ ������
            //1�� �� �������
            //���� ���� �̾Ƽ� 
            ran = Random.Range(0, 5);
            //�� ���� �ش��ϴ� â����ġ�� ����
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
