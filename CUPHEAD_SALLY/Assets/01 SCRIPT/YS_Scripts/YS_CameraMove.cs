using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 네모난 박스를 만들어서 박스를 기준으로 카메라 무빙
// 박스의 크기 -> timing
public class YS_CameraMove : MonoBehaviour
{
    Vector3 dir, camPos;
    GameObject player;
    float speed = 3f;
    // 움직이는 정도
    public float distance;
    // 움직이는 타이밍
    public float timing = 3f;
    // 움직임의 끝지점
    public float end = 1f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        camPos = transform.position;

        // 카메라가 플레이어를 따라가는게 아니라 일정 수치를 지나면 한계점까지 부드럽게 멈추게
        if (player.transform.position.x > timing) 
        {
            distance = end;
            // Lerp로 구현
            //camPos.x = Mathf.Lerp(transform.position.x, end, speed * Time.deltaTime);
        }
        else if (player.transform.position.x < -timing)
        {
            distance = -end;
            // Lerp로 구현
            //camPos.x = Mathf.Lerp(transform.position.x, -end, speed * Time.deltaTime);
        }

        // 카메라가 플레이어를 따라가는게 아니라 일정 수치(timing)를 지나면 한계점까지 부드럽게 멈추게
        // 일정 수치(timing)를 지나지 않았다면 플레이어가 이동한 비율만큼 부드럽게 멈추게
        if ((-timing < player.transform.position.x && player.transform.position.x < timing))
        {
            distance = player.transform.position.x * (1 / timing);
            // Lerp로 구현
            //camPos.x = Mathf.Lerp(transform.position.x, player.transform.position.x * (1 / timing), speed * Time.deltaTime);
        }

        dir.x = distance - transform.position.x;

        camPos += dir * speed * Time.deltaTime;
        camPos.x = Mathf.Clamp(camPos.x, -end, end);

        transform.position = camPos;
    }
}
