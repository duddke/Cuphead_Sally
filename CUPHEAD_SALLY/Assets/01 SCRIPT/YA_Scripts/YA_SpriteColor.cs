using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteMask))]
public class YA_SpriteColor : MonoBehaviour
{

    SpriteMask spMask;
    SpriteRenderer spRen;

    // Start is called before the first frame update
    void Start()
    {
        spMask = GetComponent<SpriteMask>();
        spRen = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        spMask.sprite = spRen.sprite;
    }
}
