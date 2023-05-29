using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �׸� �ڽ��� ���� �ڽ��� �������� ī�޶� ����
// �ڽ��� ũ�� -> timing
public class YS_CameraMove : MonoBehaviour
{
    Vector3 dir, camPos;
    GameObject player;
    float speed = 3f;
    // �����̴� ����
    public float distance;
    // �����̴� Ÿ�̹�
    public float timing = 3f;
    // �������� ������
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

        // ī�޶� �÷��̾ ���󰡴°� �ƴ϶� ���� ��ġ�� ������ �Ѱ������� �ε巴�� ���߰�
        if (player.transform.position.x > timing) 
        {
            distance = end;
            // Lerp�� ����
            //camPos.x = Mathf.Lerp(transform.position.x, end, speed * Time.deltaTime);
        }
        else if (player.transform.position.x < -timing)
        {
            distance = -end;
            // Lerp�� ����
            //camPos.x = Mathf.Lerp(transform.position.x, -end, speed * Time.deltaTime);
        }

        // ī�޶� �÷��̾ ���󰡴°� �ƴ϶� ���� ��ġ(timing)�� ������ �Ѱ������� �ε巴�� ���߰�
        // ���� ��ġ(timing)�� ������ �ʾҴٸ� �÷��̾ �̵��� ������ŭ �ε巴�� ���߰�
        if ((-timing < player.transform.position.x && player.transform.position.x < timing))
        {
            distance = player.transform.position.x * (1 / timing);
            // Lerp�� ����
            //camPos.x = Mathf.Lerp(transform.position.x, player.transform.position.x * (1 / timing), speed * Time.deltaTime);
        }

        dir.x = distance - transform.position.x;

        camPos += dir * speed * Time.deltaTime;
        camPos.x = Mathf.Clamp(camPos.x, -end, end);

        transform.position = camPos;
    }
}
