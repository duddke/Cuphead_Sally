using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YA_TuCamera : MonoBehaviour
{
    public GameObject Target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        pos.x = Target.transform.position.x;
        //나중에 도착 지점 나오면 어쩌고 하기
        pos.x = Mathf.Clamp(pos.x,-5.7f,5.7f);
        transform.position = pos;
    }
}
