using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YS_SkillUI : MonoBehaviour
{
    public Image[] skillimg = new Image[5];
    public int num = 0;

    // 스킬 사용
    YS_PlayerShoot ys_ps;

    // Start is called before the first frame update
    void Start()
    {
        ys_ps = GameObject.Find("Player").GetComponent<YS_PlayerShoot>();
    }

    // Update is called once per frame
    void Update()
    {
        if(num < 5)
        {
            //skillimg[num].fillAmount += 0.001f;
        
            if(skillimg[num].fillAmount == 1)
            {
                num++;
                YS_DataBox.data.p_super++;
                ys_ps.skillPoint++;
            }
        }

        // 스킬 사용하면 초기화
        if (ys_ps.b_skill == true)
        {
            for(int i = 0; i < 5; i++)
            {
                skillimg[i].fillAmount = 0;
            }

            num = 0;
        }
    }
}
