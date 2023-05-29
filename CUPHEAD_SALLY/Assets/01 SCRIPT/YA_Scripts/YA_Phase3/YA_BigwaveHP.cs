using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YA_BigwaveHP : MonoBehaviour
{
    //닿은 애가 플레이어면 플레이어 피깍기
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
