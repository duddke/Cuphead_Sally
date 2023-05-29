using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YA_Bigwave : MonoBehaviour
{
    GameObject meteo;
    //�¾�� ���ʿ��� ���������� �̵��Ѵ�
    //��ũ�� x ���尪
    float xScreen;
    //��ũ�� y ���尪
    float yScreen;
    bool end;
    // Start is called before the first frame update
    void Start()
    {
        yScreen = Camera.main.orthographicSize * 2;
        xScreen = yScreen * Camera.main.aspect;
        meteo = GameObject.Find("MeteoPhase(Clone)");
        YA_EnemyPhase3.Instance.phaseEnd += () => { Destroy(gameObject); };
    }

    float speed = 8;
    // Update is called once per frame
    void Update()
    {

        transform.position += Vector3.right * speed * Time.deltaTime;
        if (meteo)
        {
            if (transform.position.x >= meteo.transform.position.x - 3)
            {
                YA_Meteo.bigwave = true;
            }
        }
            if (transform.position.x >= xScreen * 0.5 +5)
            {
            print(1111);
                //speed = 0;
                YA_EnemyPhase3.Instance.waveEnd = true;
            YA_Meteo.bigwave = false;
                Destroy(gameObject);
                //gameObject.SetActive(false);
            }
        
        /*currentTime += Time.deltaTime;
        if (currentTime >= creTime)
        {
            YA_EnemyPhase3.Instance.waveEnd = true;
            currentTime = 0;
        }*/
    }

    private void OnDestroy()
    {
        YA_EnemyPhase3.Instance.phaseEnd -= () => { Destroy(gameObject); };
    }
}
