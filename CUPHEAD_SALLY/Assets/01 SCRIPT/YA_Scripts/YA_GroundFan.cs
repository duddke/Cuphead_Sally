using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YA_GroundFan : MonoBehaviour
{
    public float currentTime;
    float destroyTime = 15;

    // Start is called before the first frame update
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= destroyTime)
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
