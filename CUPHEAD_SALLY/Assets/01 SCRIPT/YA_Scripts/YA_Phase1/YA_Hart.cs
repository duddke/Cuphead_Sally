using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YA_Hart : MonoBehaviour
{
    GameObject hartFactory;

    private void Start()
    {
        hartFactory = Resources.Load<GameObject>("YA_Prefabs/Hart");
    }
    public void GetHart()
    {
        GameObject hart = Instantiate(hartFactory);
        hart.transform.position = GameObject.Find("HartFire").transform.position;
    }
}
