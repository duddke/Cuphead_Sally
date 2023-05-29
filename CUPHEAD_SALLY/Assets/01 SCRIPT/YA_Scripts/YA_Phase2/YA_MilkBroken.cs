using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YA_MilkBroken : MonoBehaviour
{
    public float currentTime;
    SpriteRenderer mes;
    Color c;
    float time = 0;



    // Start is called before the first frame update
    void Start()
    {
        mes = GetComponentInChildren<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        //StartCoroutine("Fadeout");
        c = mes.material.color;
        c.a = Mathf.Lerp(1, 0, time);
        time += Time.deltaTime * 0.5f;
        mes.material.color = c;
        if(c.a<=0)
        {
            Destroy(gameObject);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        YA_EnemyHP.Instance.EnemyTrigger(other);
        if (other.gameObject.name == "Player")
        {
            Destroy(gameObject);
        }
    }
}
