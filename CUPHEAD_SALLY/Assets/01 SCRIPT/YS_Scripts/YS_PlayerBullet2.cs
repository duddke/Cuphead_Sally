using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YS_PlayerBullet2 : MonoBehaviour
{
    public float speed = 15f;
    public Vector3 dir;
    // 총알 사정거리 (시간으로 계산)
    float currentTime;
    public float destroyTime = 0.2f;
    // 확산포 파워
    int power = 5;
    // 플레이어
    GameObject player;

    // 이펙트
    public GameObject effectFactory, effectFactory2, effectFactory3;

    // 스킬 게이지
    YS_SkillUI ys_skUI;
    int num;

    // Start is called before the first frame update
    void Start()
    {
        ys_skUI = GameObject.Find("Canvas").GetComponent<YS_SkillUI>();

        // 플레이어 방향에 따라 총알 방향도 바꿔주기
        player = GameObject.Find("Player");

        if (Input.GetKey(KeyCode.W))
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            if (Input.GetKey(KeyCode.A))
            {
                transform.eulerAngles = new Vector3(0, 0, 45);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                transform.eulerAngles = new Vector3(0, 0, -45);
            }
        }
        else if (player.transform.rotation.eulerAngles.y == 180)
        {
            transform.eulerAngles = new Vector3(0, 0, 90);
        }
        else if (player.transform.rotation.eulerAngles.y == 0)
        {
            transform.eulerAngles = new Vector3(0, 0, -90);
        }

        // 이펙트
        GameObject shooteffect = Instantiate(effectFactory);
        shooteffect.transform.position = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;

        transform.position += dir * speed * Time.deltaTime;

        if(currentTime > destroyTime)
        {
            Destroy(gameObject);

            // 이펙트
            GameObject nohit = Instantiate(effectFactory3);
            nohit.transform.position = transform.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);

        if (other.gameObject.name.Contains("Sally"))
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
