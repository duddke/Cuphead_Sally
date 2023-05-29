using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YS_PlayerBullet : MonoBehaviour
{
    float speed = 30f;
    GameObject shootPos;
    Vector3 dir;
    // 플레이어 로테이트 컴포넌트
    YS_PlayerRotate ys_pr;
    // 기본총알 파워
    int power = 10;
    // 플레이어
    GameObject player;

    // 이펙트
    public GameObject effectFactory, effectFactory2;

    // 스킬 게이지
    YS_SkillUI ys_skUI;
    int num;

    // Start is called before the first frame update
    void Start()
    {
        shootPos = GameObject.Find("ShootPosition");
        ys_pr = GameObject.Find("Player").GetComponent<YS_PlayerRotate>();
        ys_skUI = GameObject.Find("Canvas").GetComponent<YS_SkillUI>();

        dir = shootPos.transform.right;
        // 몸 숙였을 때
        if(ys_pr.b_down == true)
        {
            dir = shootPos.transform.up;
        }

        // 플레이어 방향에 따라 총알 방향도 바꿔주기
        player = GameObject.Find("Player");

        if (Input.GetKey(KeyCode.W))
        {
            transform.eulerAngles = new Vector3(0, 0, 90);
            if(Input.GetKey(KeyCode.A))
            {
                transform.eulerAngles = new Vector3(0, 0, 135);
            }
            else if(Input.GetKey(KeyCode.D))
            {
                transform.eulerAngles = new Vector3(0, 0, 45);
            }
        }
        else if (player.transform.rotation.eulerAngles.y == 180)
        {
            transform.eulerAngles = new Vector3(0, 0, 180);
        }
        else if (player.transform.rotation.eulerAngles.y == 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }

        // 이펙트
        GameObject shooteffect = Instantiate(effectFactory);
        shooteffect.transform.position = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += dir * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);

        if(other.gameObject.name.Contains("Sally"))
        {
            YA_EnemyHP.Instance.HP -= power;
            // 스킬 게이지
            num = ys_skUI.num;
            ys_skUI.skillimg[num].fillAmount += 0.02f;
        }

        // 이펙트
        GameObject hit = Instantiate(effectFactory2);
        hit.transform.position = transform.position;
    }
}
