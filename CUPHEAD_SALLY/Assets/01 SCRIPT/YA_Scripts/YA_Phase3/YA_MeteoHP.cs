using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YA_MeteoHP : MonoBehaviour
{
    //���� HP0�� �Ǹ�
    //���׿� ������ �޽�
    //���׿�b ������ Ʈ��
    //�� ������ Ʈ��
    public GameObject METEO;
    public GameObject METEOBROKEN;
    public GameObject STAR;
    public bool Broken;
    public bool Star;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public int hp = 5;
    public int HP
    {
        get
        {
            return hp;
        }
        set
        {
            hp = value;
            if(hp<=0)
            {
                MeteoBroken();
            }
        }
    }
    //�Լ� �����
    //���׿��� �Ž� ������ ����
    //�ݶ��̴� ����
    //���ū ���׿� �Ž� ������ �ѱ�
    //��Ÿ �Ž� �ѱ�
    //��Ÿ �ݶ��̴� �ѱ�
    public void MeteoBroken()
    {
        METEO.SetActive(false);
        METEOBROKEN.SetActive( true);
        STAR.SetActive(true);
    }
    private void OnTriggerEnter(Collider other)
    {
        YA_EnemyHP.Instance.EnemyTrigger(other);
        if (other.gameObject.layer==8)
        {
            HP--;
        }
    }
    
}
