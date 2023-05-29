using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YS_EffectRotate : MonoBehaviour
{
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");

        if (player.transform.rotation.eulerAngles.y == 180)
        {
            transform.eulerAngles = new Vector3(0, 0, 180);
        }
        else if (player.transform.rotation.eulerAngles.y == 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
