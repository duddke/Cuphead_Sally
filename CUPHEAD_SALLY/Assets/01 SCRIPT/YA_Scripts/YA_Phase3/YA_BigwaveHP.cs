using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YA_BigwaveHP : MonoBehaviour
{
    //���� �ְ� �÷��̾�� �÷��̾� �Ǳ��
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        YA_EnemyHP.Instance.EnemyTrigger(other);
    }
}
