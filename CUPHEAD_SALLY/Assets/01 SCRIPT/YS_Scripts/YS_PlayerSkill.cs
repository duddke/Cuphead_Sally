using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YS_PlayerSkill : MonoBehaviour
{
    float speed = 10f;
    Vector3 dir;
    GameObject skillPos, shootPos;
    int power = 1;
    // 이펙트
    public GameObject skilleffectFactory;
    // 스킬 제어
    YS_PlayerShoot ys_ps;

    // Start is called before the first frame update
    void Start()
    {
        skillPos = GameObject.Find("SkillPosition");
        shootPos = GameObject.Find("ShootPosition");
        //dir = skillPos.transform.up;
        dir = shootPos.transform.right;

        // 스킬 제어
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
                // 이펙트
                GameObject hit = Instantiate(skilleffectFactory);
                hit.transform.position = transform.position;

                ys_ps.skillCount++;
            }
        }
    }
}
