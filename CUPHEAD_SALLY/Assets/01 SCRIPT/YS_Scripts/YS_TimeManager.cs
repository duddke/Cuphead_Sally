using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YS_TimeManager : MonoBehaviour
{
    // 애니메이션
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        // 애니메이션
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TimeStop()
    {
        anim.updateMode = AnimatorUpdateMode.UnscaledTime;
        Time.timeScale = 0;
    }

    public void TimeContinue()
    {
        Time.timeScale = 1;
        anim.updateMode = AnimatorUpdateMode.Normal;
    }
}
