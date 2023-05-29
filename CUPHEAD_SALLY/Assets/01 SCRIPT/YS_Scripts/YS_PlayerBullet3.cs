using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YS_PlayerBullet3 : MonoBehaviour
{
    // �ӵ�
    float speed = 0.5f;
    GameObject shootPos;
    Vector3 dir;
    // Ÿ�� �޾ƿ���
    public GameObject target;
    // �÷��̾� ������Ʈ ������Ʈ
    YS_PlayerRotate ys_pr;
    // ����ź �Ŀ�
    int power = 2;
    // ��ġ �񱳸� ���� �ӽð�
    Vector3 posTemp, posTemp2;

    // ������ � �� 4��
    Vector3[] points = new Vector3[4];
    float endTime, currentTime;
    public float startCurve = 15f; // �����ϸ鼭 ���̴� ���� (�����ͷ� ���鼭 ����)
    public float endCurve = 3f; // ������ ���̴� ���� (�����ͷ� ���鼭 ����)

    // ����Ʈ
    public GameObject effectFactory, effectFactory2;

    // ��ų ������
    YS_SkillUI ys_skUI;
    int num;

    // Start is called before the first frame update
    void Start()
    {
        shootPos = GameObject.Find("ShootPosition");
        ys_skUI = GameObject.Find("Canvas").GetComponent<YS_SkillUI>();

        // ����� ���� �ֱ�
        if (GameObject.Find("Phases").transform.Find("Phase 1").gameObject.activeSelf == true)
        {
            target = GameObject.Find("Sally_P1");
        }
        else if(GameObject.Find("Phases").transform.Find("Phase 2").gameObject.activeSelf == true)
        {
            target = GameObject.Find("Sally_P2");
        }
        else if (GameObject.Find("Phases").transform.Find("Phase 3").gameObject.activeSelf == true)
        {
            target = GameObject.Find("Sally_P3");
        }
        else if (GameObject.Find("Phases").transform.Find("Phase 4").gameObject.activeSelf == true)
        {
            target = GameObject.Find("Sally_P4");
        }

        ys_pr = GameObject.Find("Player").GetComponent<YS_PlayerRotate>();

        dir = shootPos.transform.right;

        // �� ������ ��
        if (ys_pr.b_down == true)
        {
            dir = shootPos.transform.up;
        }

        // �����ð��� ��������
        endTime = Random.Range(0.8f, 1f);

        if(target)
        {
            // ������ (ù��° ��)
            points[0] = shootPos.transform.position;
            // �ι�° �� (������ �ֺ�)
            points[1] = shootPos.transform.position + (startCurve * Random.Range(1f, 2f) * shootPos.transform.right);
            // ����° �� (���� �ֺ�)
            points[2] = target.transform.position + (endCurve * Random.Range(-1f, 1f) * target.transform.right) + (endCurve * Random.Range(-1f, 1f) * target.transform.up);
            // ���� (�׹�° ��) -> Ÿ���� ��� �����̱� ������ ���⼭ ����X
            //points[3] = target.transform.position;

            // �� ������ ��
            if (ys_pr.b_down == true)
            {
                points[1] = shootPos.transform.position + (15 * Random.Range(1f, 2f) * shootPos.transform.up);
            }
        }

        // ����Ʈ
        GameObject shooteffect = Instantiate(effectFactory);
        shooteffect.transform.position = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(target)
        {
            // ���� (�׹�° ��) -> Ÿ���� ��� �����̱� ������ ���⼭ ����
            points[3] = target.transform.position;

            if (currentTime > endTime)
            {
                return;
            }

            currentTime += Time.deltaTime * speed;

            transform.position = new Vector3(Bezier(points[0].x, points[1].x, points[2].x, points[3].x), Bezier(points[0].y, points[1].y, points[2].y, points[3].y), -0.0799999982f);
            // �Ѿ��� ������ �ٶ󺸰Բ�
            transform.up = target.transform.position - transform.position;

            // ������ ������� Ÿ�ٵ� null��
            if (target.gameObject.activeSelf == false)
            {
                target = null;
            }
        }
        else
        {
            transform.position += dir * speed * 50 * Time.deltaTime;
        }
    }

    float Bezier(float point0, float point1, float point2, float point3)
    {
        float t = currentTime / endTime;

        // 3�� ������ � ������ : P = (1 - t)^3 * P0 + 3(1 - t)^2 * t * P1 + 3(1 - t) * t^2 * P2 + t^3 * P3
        return Mathf.Pow((1 - t), 3) * point0 + 3 * Mathf.Pow((1 - t), 2) * t * point1 + 3 * (1 - t) * Mathf.Pow(t, 2) * point2 + Mathf.Pow(t, 3) * point3;
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
