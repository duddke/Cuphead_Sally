using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YS_DataBox : MonoBehaviour
{
    // ΩÃ±€≈Ê!
    public static YS_DataBox data;

    // µ•¿Ã≈ÕµÈ
    public double gameTime;
    public float p_hp;
    public float p_parry;
    public float p_super;
    public float p_skill;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if(data == null)
        {
            data = this;
        }
    }
}
