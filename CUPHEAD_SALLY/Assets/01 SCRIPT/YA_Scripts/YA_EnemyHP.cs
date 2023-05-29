using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YA_EnemyHP : MonoBehaviour
{
    // �÷��̾� �ִϸ��̼�(����)
    //Animator p_anim;
    //

    //�ǰ� �ٸ�
    //������������Ʈ�� �÷��� �ٲٰ� �ʹ�
    //-> ���� �Ͼ�� ������ִ� ���ӿ�����Ʈ �״ٰ� ����
    //SpriteRenderer SallySpri;
    //bool colorCh;
    public GameObject damageSally;
    public GameObject damageSally2;
    bool damage;

    float currentTime=0;
    float originTime=0.1f;

    public static YA_EnemyHP Instance;
    public int hp = 200;
    public int HP
    {
        get { return hp; }
        set
        {
            hp = value;
            if (hp <= 0)
            {
                YS_DataBox.data.p_hp = YS_PlayerHealth.Instance.hp;
            }
        }
    }
    CharacterController cc;
    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        //SallySpri = GetComponentInChildren<SpriteRenderer>();

        // �÷��̾� �ִϸ��̼� �Ҵ�
        //p_anim = GameObject.Find("Player").GetComponentInChildren<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        /*if(colorCh)
        {
            currentTime += Time.deltaTime;
            Color sallycol = SallySpri.color;
            sallycol.r = 0;
            SallySpri.color = sallycol;
            if (currentTime >= originTime)
            {
                sallycol.r = 255;
                SallySpri.color = sallycol;
                currentTime = 0;
                colorCh = false;
            }
        }*/
        if(damage)
        {
            currentTime += Time.deltaTime;
            Color sallycol = damageSally.GetComponent<SpriteRenderer>().color;
            sallycol.a = 0.5f;
            damageSally.GetComponent<SpriteRenderer>().color = sallycol;
            Color sallycol2 = damageSally2.GetComponent<SpriteRenderer>().color;
            sallycol2.a = 0.5f;
            damageSally2.GetComponent<SpriteRenderer>().color = sallycol2;
            if (currentTime>=originTime)
            {
                currentTime = 0;
                sallycol.a = 0;
                damageSally.GetComponent<SpriteRenderer>().color = sallycol;
                sallycol2.a = 0f;
                damageSally2.GetComponent<SpriteRenderer>().color = sallycol2;
                damage = false;
            }
        }
    }

    public void EnemyTrigger(Collider other) //���ʹ̿� �ε��� HP �� ���
    {
        if (other.gameObject.name=="Player")
        {
            //print("playerHP--");
            YS_PlayerHealth.Instance.HP--;
            //Destroy(gameObject.GetComponent<Rigidbody>());
            //�÷��̾� �Ǳ��

            /*// �÷��̾� �ִϸ��̼�
            p_anim.SetTrigger("Damaged");*/
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        //cc.enabled = false;
        EnemyTrigger(other);
        if(other.gameObject.name.Contains("Bullet"))
        {
            //colorCh = true;
            damage = true;
        }
        /*if (other.gameObject.name.Contains("Player"))//���̾�� ������ ����
        {
            HP--;
        }*/
    }

    /*private void OnTriggerExit(Collider other)
{
   cc.enabled = true;
}*/


}
