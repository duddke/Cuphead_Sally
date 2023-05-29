using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YA_RossPhase : MonoBehaviour
{
    //��̰� ������ 5�� �� �ٽ� ��� 
    float currentTime;
    public float rossDellayTime=5;
    public GameObject rossFactory;
    bool rossTime=true;

    Transform target;
    Vector3 targetPos;

    //��ũ�� x ���尪
    float xScreen;
    //��ũ�� y ���尪
    float yScreen;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player").transform;
        yScreen = Camera.main.orthographicSize * 2;
        xScreen = yScreen * Camera.main.aspect;
        rossFactory =Resources.Load<GameObject>("YA_Prefabs/RossNoTrigger");
        targetPos = target.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (YA_EnemyHP.Instance.hp <= 0)
        {
            GetComponent<Collider>().enabled = false;
        }
        if (rossTime)
        {
            // Ÿ���� ��ġ x���� �ް�
            // y�� ���� ���� ������ ��ġ ���
            GameObject ross = Instantiate(rossFactory);
            ross.transform.position = new Vector3(targetPos.x, -5, -5);
            rossTime = false;
        }
        currentTime += Time.deltaTime;
        if(currentTime>=rossDellayTime)
        {
            targetPos = target.position;
            rossTime = true;
            currentTime = 0;
        }
    }
}
