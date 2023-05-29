using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YA_Mouse_Left : MonoBehaviour
{

    Transform sally;
    float speed = 12;
    Vector3 dir;
    Vector3 angles;
    public CharacterController cc;
    //스크린 x 저장값
    float xScreen;
    //스크린 y 저장값
    float yScreen;
    //타겟 위치 저장하는 곳
    public GameObject target;
    //라스트지점
    bool destroy;
    public SpriteRenderer MouseForwad;
    public SpriteRenderer MouseLeft;
    public GameObject MouseDestroy;

    public enum State
    {
        Down,
        Left,
        Up,
        Fly,
        Fall
    }
    public State state;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player");
        yScreen = Camera.main.orthographicSize * 2;
        xScreen = yScreen * Screen.width/Screen.height;
        sally = GameObject.Find("Sally_P2").transform;
        //정면을 보고 내려온다
        cc = GetComponent<CharacterController>();
        cc.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        /*//바닥에 닿으면
        if (cc.isGrounded)
        {
            //위치가 에너미보다 x가 작으면
            if (transform.position.x < sally.position.x)
            {
                //왼쪽으로(y90도)
                angles = new Vector3(0, 90, 0);
                //앞으로 간다 포어드
                dir = Vector3.left ;
                //x스크린 크기만큼 가면(카메라 값 가져와야할듯)
                if (transform.position.x <= -xScreen*0.5)
                {
                print(11111111111111);
                    //z-90도 돌기
                    angles = new Vector3(90, 90, 0);
                    dir = Vector3.up;
                    //x방향으로 진행중이기에 x값으로 비교해야함 y스크린 값만큼 가고
                    if (transform.position.x >= yScreen)
                    {
                        //다시 z-90도 돌기
                        angles = new Vector3(180, 90, 0);
                        dir = Vector3.right;
                        //타겟 위치받기
                        target = GameObject.Find("Player").transform.position;
                        if (target.x == transform.position.x)
                        {
                            //x위치가 타겟과 같다면 떨어지기
                            dir = Vector3.down;
                            destroy = true;
                        }
                    }
                }
            }
        }*/
        transform.eulerAngles = angles;
        if (cc != null)
        { cc.Move(dir * speed * Time.deltaTime); }
        switch (state)
        {
            case State.Down:
                UdDown();
                break;
            case State.Left:
                UpLeft();
                break;
            case State.Up:
                UpUp();
                break;
            case State.Fly:
                UpFly();
                break;
            case State.Fall:
                UpFall();
                break;

        }
        if (cc.isGrounded)
        {
            if (destroy)
            {
                currentTime += Time.deltaTime;
                    MouseForwad.enabled = false;
                    MouseLeft.enabled = false;
                MouseDestroy.SetActive(true);
                if (currentTime >= DestroyTime)
                {
                    cc.enabled = false;
                    Destroy(gameObject);
                }
            }
        }
    }
    void UdDown()
    {
        dir = Vector3.down;
        angles = new Vector3(0, 0, 0);
        if (cc.isGrounded)
        {
            MouseForwad.enabled = false;
            MouseLeft.enabled = true;
            if (transform.position.x < sally.position.x)
            { state = State.Left; }
        }
    }
    void UpLeft()
    {
        angles = new Vector3(0, 90, 0);
        dir = Vector3.left;
        if (transform.position.x <= -19)
        {
            state = State.Up;
        }
    }
    void UpUp()
    {
        angles = new Vector3(90, 90, 0);
        dir = Vector3.up;
        if (transform.position.y >= yScreen-2)
        {
            MouseForwad.enabled = true;
            MouseLeft.enabled = false;
            state = State.Fly;
        }
    }
    void UpFly()
    {
        angles = new Vector3(0, 0, 0);
        dir = Vector3.right;
        if (target.transform.position.x <= transform.position.x)
        {
            state = State.Fall;
        }

    }
    float currentTime = 0;
    public float DestroyTime = 1;
    void UpFall()
    {
        dir = Vector3.down;
        transform.position += dir * speed * Time.deltaTime;
        destroy = true;
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
