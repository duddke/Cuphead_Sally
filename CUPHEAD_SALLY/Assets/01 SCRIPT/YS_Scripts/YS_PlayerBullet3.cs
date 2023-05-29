using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YS_PlayerBullet3 : MonoBehaviour
{
    // 속도
    float speed = 0.5f;
    GameObject shootPos;
    Vector3 dir;
    // 타겟 받아오기
    public GameObject target;
    // 플레이어 로테이트 컴포넌트
    YS_PlayerRotate ys_pr;
    // 유도탄 파워
    int power = 2;
    // 위치 비교를 위한 임시값
    Vector3 posTemp, posTemp2;

    // 베지어 곡선 점 4개
    Vector3[] points = new Vector3[4];
    float endTime, currentTime;
    public float startCurve = 15f; // 시작하면서 꺾이는 정도 (에디터로 보면서 조절)
    public float endCurve = 3f; // 끝에서 꺾이는 정도 (에디터로 보면서 조절)

    // 이펙트
    public GameObject effectFactory, effectFactory2;

    // 스킬 게이지
    YS_SkillUI ys_skUI;
    int num;

    // Start is called before the first frame update
    void Start()
    {
        shootPos = GameObject.Find("ShootPosition");
        ys_skUI = GameObject.Find("Canvas").GetComponent<YS_SkillUI>();

        // 페이즈별 샐리 넣기
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

        // 몸 숙였을 때
        if (ys_pr.b_down == true)
        {
            dir = shootPos.transform.up;
        }

        // 도착시간을 랜덤으로
        endTime = Random.Range(0.8f, 1f);

        if(target)
        {
            // 시작점 (첫번째 점)
            points[0] = shootPos.transform.position;
            // 두번째 점 (시작점 주변)
            points[1] = shootPos.transform.position + (startCurve * Random.Range(1f, 2f) * shootPos.transform.right);
            // 세번째 점 (끝점 주변)
            points[2] = target.transform.position + (endCurve * Random.Range(-1f, 1f) * target.transform.right) + (endCurve * Random.Range(-1f, 1f) * target.transform.up);
            // 끝점 (네번째 점) -> 타겟이 계속 움직이기 때문에 여기서 지정X
            //points[3] = target.transform.position;

            // 몸 숙였을 때
            if (ys_pr.b_down == true)
            {
                points[1] = shootPos.transform.position + (15 * Random.Range(1f, 2f) * shootPos.transform.up);
            }
        }

        // 이펙트
        GameObject shooteffect = Instantiate(effectFactory);
        shooteffect.transform.position = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(target)
        {
            // 끝점 (네번째 점) -> 타겟이 계속 움직이기 때문에 여기서 지정
            points[3] = target.transform.position;

            if (currentTime > endTime)
            {
                return;
            }

            currentTime += Time.deltaTime * speed;

            transform.position = new Vector3(Bezier(points[0].x, points[1].x, points[2].x, points[3].x), Bezier(points[0].y, points[1].y, points[2].y, points[3].y), -0.0799999982f);
            // 총알이 샐리를 바라보게끔
            transform.up = target.transform.position - transform.position;

            // 샐리가 사라지면 타겟도 null로
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

        // 3차 베지어 곡선 방정식 : P = (1 - t)^3 * P0 + 3(1 - t)^2 * t * P1 + 3(1 - t) * t^2 * P2 + t^3 * P3
        return Mathf.Pow((1 - t), 3) * point0 + 3 * Mathf.Pow((1 - t), 2) * t * point1 + 3 * (1 - t) * Mathf.Pow(t, 2) * point2 + Mathf.Pow(t, 3) * point3;
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
