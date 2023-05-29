using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YA_MeteoHP : MonoBehaviour
{
    //만약 HP0이 되면
    //메테오 오브젝 펄스
    //메테오b 오브젝 트루
    //별 오브젝 트루
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
    //함수 실행시
    //메테오의 매쉬 렌더러 끄기
    //콜라이더 끄기
    //브로큰 메테오 매쉬 렌더러 켜기
    //스타 매쉬 켜기
    //스타 콜라이더 켜기
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
