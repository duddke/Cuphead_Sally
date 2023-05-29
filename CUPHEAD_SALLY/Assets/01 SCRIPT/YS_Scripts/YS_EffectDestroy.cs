using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 이펙트가 일정시간이 지나면 저절로 사라지게끔
public class YS_EffectDestroy : MonoBehaviour
{
    float destroyTime;
    // 스킬 제어
    YS_PlayerShoot ys_ps;

    // Start is called before the first frame update
    void Start()
    {
        // 스킬 제어
        ys_ps = GameObject.Find("Player").GetComponent<YS_PlayerShoot>();
    }

    // Update is called once per frame
    void Update()
    {
        destroyTime += Time.deltaTime;

        if(gameObject.name.Contains("Shoot") || gameObject.name.Contains("Hit"))
        {
            if(destroyTime > 0.2f)
            {
                Destroy(gameObject);
            }
        }
        else if(gameObject.name.Contains("Skill"))
        {
            if(destroyTime > 2.1f)
            {
                Destroy(gameObject);
            }
        }
        else if(gameObject.name.Contains("Parry"))
        {
            if (destroyTime > 0.3f)
            {
                Destroy(gameObject);
            }
        }
        else if(gameObject.name.Contains("Damaged"))
        {
            if (destroyTime > 0.4f)
            {
                Destroy(gameObject);
            }
        }
        else if(gameObject.name.Contains("Line"))
        {
            if (destroyTime > 1f)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            if(destroyTime > 0.6f)
            {
                Destroy(gameObject);
                if(gameObject.name.Contains("end"))
                {
                    ys_ps.skillCount = 0;
                }
            }
        }
    }
}
