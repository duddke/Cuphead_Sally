using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YS_PlayerBullet : MonoBehaviour
{
    float speed = 30f;
    GameObject shootPos;
    Vector3 dir;
    // �÷��̾� ������Ʈ ������Ʈ
    YS_PlayerRotate ys_pr;
    // �⺻�Ѿ� �Ŀ�
    int power = 10;
    // �÷��̾�
    GameObject player;

    // ����Ʈ
    public GameObject effectFactory, effectFactory2;

    // ��ų ������
    YS_SkillUI ys_skUI;
    int num;

    // Start is called before the first frame update
    void Start()
    {
        shootPos = GameObject.Find("ShootPosition");
        ys_pr = GameObject.Find("Player").GetComponent<YS_PlayerRotate>();
        ys_skUI = GameObject.Find("Canvas").GetComponent<YS_SkillUI>();

        dir = shootPos.transform.right;
        // �� ������ ��
        if(ys_pr.b_down == true)
        {
            dir = shootPos.transform.up;
        }

        // �÷��̾� ���⿡ ���� �Ѿ� ���⵵ �ٲ��ֱ�
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

        // ����Ʈ
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
            // ��ų ������
            num = ys_skUI.num;
            ys_skUI.skillimg[num].fillAmount += 0.02f;
        }

        // ����Ʈ
        GameObject hit = Instantiate(effectFactory2);
        hit.transform.position = transform.position;
    }
}
