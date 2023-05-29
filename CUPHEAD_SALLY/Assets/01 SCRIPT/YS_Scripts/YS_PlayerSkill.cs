using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YS_PlayerSkill : MonoBehaviour
{
    float speed = 10f;
    Vector3 dir;
    GameObject skillPos, shootPos;
    int power = 1;
    // ����Ʈ
    public GameObject skilleffectFactory;
    // ��ų ����
    YS_PlayerShoot ys_ps;

    // Start is called before the first frame update
    void Start()
    {
        skillPos = GameObject.Find("SkillPosition");
        shootPos = GameObject.Find("ShootPosition");
        //dir = skillPos.transform.up;
        dir = shootPos.transform.right;

        // ��ų ����
        ys_ps = GameObject.Find("Player").GetComponent<YS_PlayerShoot>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += dir * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Sally"))
        {
            YA_EnemyHP.Instance.HP -= power;
            
            if(ys_ps.skillCount < 1)
            {
                // ����Ʈ
                GameObject hit = Instantiate(skilleffectFactory);
                hit.transform.position = transform.position;

                ys_ps.skillCount++;
            }
        }
    }
}
