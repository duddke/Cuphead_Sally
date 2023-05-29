using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YS_PlayerBullet2 : MonoBehaviour
{
    public float speed = 15f;
    public Vector3 dir;
    // �Ѿ� �����Ÿ� (�ð����� ���)
    float currentTime;
    public float destroyTime = 0.2f;
    // Ȯ���� �Ŀ�
    int power = 5;
    // �÷��̾�
    GameObject player;

    // ����Ʈ
    public GameObject effectFactory, effectFactory2, effectFactory3;

    // ��ų ������
    YS_SkillUI ys_skUI;
    int num;

    // Start is called before the first frame update
    void Start()
    {
        ys_skUI = GameObject.Find("Canvas").GetComponent<YS_SkillUI>();

        // �÷��̾� ���⿡ ���� �Ѿ� ���⵵ �ٲ��ֱ�
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

        // ����Ʈ
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

            // ����Ʈ
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

            // ��ų ������
            num = ys_skUI.num;
            ys_skUI.skillimg[num].fillAmount += 0.02f;
        }

        // ����Ʈ
        GameObject hit = Instantiate(effectFactory2);
        hit.transform.position = transform.position;
    }
}
