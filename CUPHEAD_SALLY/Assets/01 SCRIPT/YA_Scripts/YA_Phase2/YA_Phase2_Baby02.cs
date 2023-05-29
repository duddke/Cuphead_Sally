using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YA_Phase2_Baby02 : MonoBehaviour
{
    // �� â������ ������Ʈ ����(��)(��ġ�� �޾ƿ�)
    public GameObject W2_1;
    public GameObject W2_2;
    public GameObject W2_3;
    public GameObject W2_4;
    float cruTime;
    float mTime = 0.5f;
    float fadeTime = 1;
    bool milkCount;
    public int ran;
    public static bool baby;

    public GameObject milkFactory;

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
            ran = Random.Range(0, 4);
            //�� ���� �ش��ϴ� â����ġ�� ����
            if (ran == 0)
            {
                ran = 0;
                transform.position = W2_1.transform.position;
                baby = true;
            }
            else if (ran == 1)
            {
                ran = 0;
                transform.position = W2_2.transform.position;
                baby = true;
            }
            else if (ran == 2)
            {
                ran = 0;
                transform.position = W2_3.transform.position;
                baby = true;
            }
            else if (ran == 3)
            {
                ran = 0;
                transform.position = W2_4.transform.position;
                baby = true;
            }
        }
        cruTime += Time.deltaTime;
        if (cruTime >= mTime)
        {
            if (!milkCount)
            {
                GameObject milk = Instantiate(milkFactory);
                milk.transform.position = transform.position;
                milkCount = true;
            }
            if (cruTime >= fadeTime)
            {
                cruTime = 0;
                milkCount = false;
                gameObject.SetActive(false);
            }
        }
    }
}
