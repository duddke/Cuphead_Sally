using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YS_PlayerRotate : MonoBehaviour
{
    public Rigidbody rigid;
    // 플레이어 무브 컴포넌트
    YS_PlayerMove ys_pm;
    // 플레이어 슛 컴포넌트
    YS_PlayerShoot ys_ps;
    // 몸 숙였을 때
    public bool b_down = false;

    // Start is called before the first frame update
    void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody>();
        ys_pm = gameObject.GetComponent<YS_PlayerMove>();
        ys_ps = gameObject.GetComponent<YS_PlayerShoot>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            if(Input.GetKey(KeyCode.S)) // 몸 숙인 채로 방향전환
            {
                transform.eulerAngles = new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z + 90);
                b_down = true;
            }
            else
            {
                transform.rotation = new Quaternion(0, 180, 0, 0);
                b_down = false;
            }
        }
        if (Input.GetKey(KeyCode.D))
        {
            if (Input.GetKey(KeyCode.S)) // 몸 숙인 채로 방향전환
            {
                transform.eulerAngles = new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z - 90);
                b_down = true;
            }
            else
            {
                transform.rotation = new Quaternion(0, 0, 0, 0);
                b_down = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.S) && ys_pm.jump == false)
        {
            if (transform.eulerAngles.y == 0)
            {
                transform.eulerAngles = new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z - 90);
                b_down = true;
            }
            else if (transform.eulerAngles.y == 180)
            {
                transform.eulerAngles = new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z + 90);
                b_down = true;
            }
        }
        // 플레이어 원위치
        else if(Input.GetKeyUp(KeyCode.S))
        {
            if (transform.eulerAngles.y == 0)
            {
                transform.rotation = new Quaternion(0, 0, 0, 0);
                b_down = false;
            }
            else if (transform.eulerAngles.y == 1)
            {
                transform.rotation = new Quaternion(0, 180, 0, 0);
                b_down = false;
            }
        }

        // 스킬 방향
        if (Input.GetKeyDown(KeyCode.Y) && ys_ps.skillPoint >= 5)
        {
            rigid.isKinematic = true;

            /*if(transform.eulerAngles.y == 0)
            {
                transform.eulerAngles = new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z - 90);
                rigid.isKinematic = true;
            }
            else if(transform.eulerAngles.y == 180)
            {
                transform.eulerAngles = new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z + 90);
                rigid.isKinematic = true;
            }*/
        }
    }
}
