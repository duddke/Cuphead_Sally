using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YA_EnemyPhase4 : MonoBehaviour
{
    //4페이지 시작 시 모든 오브젝트 실행 시키기

    public enum State
    {
        Start,
        Idle,
        Die
    }
    public State state = State.Start;
    public GameObject Sally;
    public GameObject Umbrella;
    public GameObject Ross;

    //다이 이미지 변경
    public GameObject destroy;
    public GameObject sallySpri;

    YA_EnemyHP sallyhp;

    // Start is called before the first frame update
    void Start()
    {
        Sally.SetActive(true);
        Umbrella.SetActive(true);
        Ross.SetActive(true);
        sallyhp = Sally.GetComponent<YA_EnemyHP>();
    }

    // Update is called once per frame
    void Update()
    {
        if (sallyhp.HP <= 0)
        {
            state = State.Die;
        }
        switch (state)
        {
            case State.Start:
                UpStart();
                break;
            case State.Idle:
                UpIdle();
                break;
            case State.Die:
                Die();
                break;
        }
    }

    private void Die()
    {
        Sally.GetComponent<Collider>().enabled = false;
        Ross.SetActive(false);
        sallySpri.SetActive(false);
        destroy.SetActive(true);
    }

    private void UpStart()
    {
        state = State.Idle;
    }

    private void UpIdle()
    {
        
    }
}
