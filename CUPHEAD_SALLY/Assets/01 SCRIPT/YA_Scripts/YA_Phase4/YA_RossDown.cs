using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YA_RossDown : MonoBehaviour
{
    //다운
    // 아래로 떨어지기 
    // 그라운드에 닿으면 사라지기

    public float speed = 8;
    //스크린 x 저장값
    float xScreen;
    //스크린 y 저장값
    float yScreen;
    bool isGround;

    SpriteRenderer mes;
    Color c;
    float time = 0;

    // Start is called before the first frame update
    void Start()
    {
        yScreen = Camera.main.orthographicSize * 2;
        xScreen = yScreen * Camera.main.aspect;
        mes = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;
        if(isGround)
        {
            Quaternion dir = Quaternion.Euler(0, 0,270);
            transform.rotation = Quaternion.Slerp(transform.rotation, dir, 0.9f * Time.deltaTime);
            FadeDestroy();
        }
    }

    //bool isGround;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Ground")
        {
            Vector3 currentPos = transform.position;
            transform.position = currentPos;
            speed = 0;
            isGround = true;
        }
        YA_EnemyHP.Instance.EnemyTrigger(other);//플레이어 피 깍기
        if (other.gameObject.name == "Player")
        {
            Destroy(gameObject);
        }
    }

    void FadeDestroy()
    {
        c = mes.color;
        c.a = Mathf.Lerp(1, 0, time);
        time += Time.deltaTime * 0.5f;
        mes.color = c;
        if (c.a <= 0)
        {
            print(11);
            Destroy(gameObject);
        }
    }
}
