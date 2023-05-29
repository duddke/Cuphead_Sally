using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YA_Phase2_BabyMilk : MonoBehaviour
{
    CharacterController cc;
    GameObject BrokenMilkFactory;
    float speed = 10;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        cc.enabled = true;
        BrokenMilkFactory = Resources.Load<GameObject>("YA_Prefabs/MilkBroken");
    }

    // Update is called once per frame
    void Update()
    {
            //태어나면 밑으로 떨어진다
            cc.Move( Vector3.down * speed * Time.deltaTime);
        if(cc.isGrounded)
        {
            GameObject BrokenMilk = Instantiate(BrokenMilkFactory);
            BrokenMilk.transform.position = transform.position;
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
