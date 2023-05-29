using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YA_Foot : MonoBehaviour
{
    public GameObject Player;
    Transform playerPos;
    GameObject Sally;
    // Start is called before the first frame update
    void Start()
    {
        playerPos = Player.transform;
        if(GameObject.Find("Sally_P1"))
        Sally = GameObject.Find("Sally_P1");
        else if (GameObject.Find("Sally_P2"))
            Sally = GameObject.Find("Sally_P2");
        Vector3 pos = transform.position;
        pos.y = Sally.transform.position.y;
        transform.position = pos;

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        pos.x = playerPos.position.x;
        transform.position = pos;
    }
}
