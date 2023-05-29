using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YA_FanMoving : MonoBehaviour
{
    public float speed = 20;
    public float currentTime;

    Vector3 targetyre;
    Vector3 dir;
    Transform target;
    Vector3 targettran;
    public CharacterController cc;
    GameObject GroundFanFactory;

    bool FanDestroy;
    bool destroyTimeST;

    // Start is called before the first frame update
    void Start()
    {
        GroundFanFactory = Resources.Load<GameObject>("YA_Prefabs/GroundFan");
        target = GameObject.Find("Foot").transform;
        //targettran = new Vector3(target.position.x, 2, target.position.z);
        targetyre = target.position;
        targetyre.y = 2;
        target.position = targetyre;
        dir = target.position - transform.position;
        //dir = targettran - transform.position;
        cc = GetComponent<CharacterController>();
        cc.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        dir.Normalize();
        transform.rotation= Quaternion.LookRotation(targetyre);
        cc.Move( dir * speed * Time.deltaTime);
        currentTime += Time.deltaTime;
        if (cc.isGrounded)
        {
            GameObject GroundFan = Instantiate(GroundFanFactory);
            GroundFan.transform.position = transform.position;
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        YA_EnemyHP.Instance.EnemyTrigger(other);
        if (other.gameObject.name == "Player")
        {
            cc.enabled = false;
            Destroy(gameObject);
        }
    }

}

