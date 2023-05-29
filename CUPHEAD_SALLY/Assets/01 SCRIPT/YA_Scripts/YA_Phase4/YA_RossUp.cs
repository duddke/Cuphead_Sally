using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YA_RossUp : MonoBehaviour
{

    //�� 
    //y��ũ�� �� ���� ����

    // ������Ʈ �ٲٰ�

    public GameObject rossFactory;
    public GameObject rossPinkFactory;
    public float speed=8;

    //��ũ�� x ���尪
    float xScreen;
    //��ũ�� y ���尪
    float yScreen;

    GameObject ross;

    // Start is called before the first frame update
    void Start()
    {
        yScreen = Camera.main.orthographicSize * 2;
        xScreen = yScreen * Camera.main.aspect;
        rossFactory = Resources.Load<GameObject>("YA_Prefabs/Ross");
        rossPinkFactory = Resources.Load<GameObject>("YA_Prefabs/RossPink");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.up * speed * Time.deltaTime;
        int ran = Random.Range(0, 3);
        if(transform.position.y>=yScreen+2)
        {
            if(ran<2)
            {
                ross = Instantiate(rossFactory);
            }
            else
            {
                ross = Instantiate(rossPinkFactory);
            }
                ross.transform.position = transform.position;
            Destroy(gameObject);
        }
    }
}
