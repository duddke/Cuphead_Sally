using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YS_PlayerMove : MonoBehaviour
{
    float speed = 13f;
    Vector3 dir;
    public float width;
    // 점프를 위한 리지드바디
    Rigidbody rigid;
    public float jumpPower = 45f;
    // 점프 판정
    public bool jump = false;
    // 대쉬 판정
    bool l_dash, r_dash, d_dash = false;
    float l_currentTime, r_currentTime, d_currentTime = 0;
    // 플레이어 슛 컴포넌트
    YS_PlayerShoot ys_ps;
    // 플레이어 상승 하강 판단
    float p_loc, temp_loc;
    // 애니메이션
    Animator anim, walk_anim, dash_anim, dash2_anim;
    // 이펙트
    public GameObject effectFactory, effectFactory2, effectFactory3, effectFactory4, walkdust_pos;
    float effectTime, dashTime;
    bool b_dashTime = false;
    // 스킬 게이지
    YS_SkillUI ys_skUI;
    int num;
    // 패링 시간
    float parryTime = 0;
    // 이펙트오디오
    AudioSource sound;

    // Start is called before the first frame update
    void Start()
    {
        float worldSize = Camera.main.orthographicSize * 2;
        float meterPerPixel = worldSize / Screen.width;
        width = meterPerPixel * Screen.width;
        rigid = GetComponent<Rigidbody>();

        ys_ps = gameObject.GetComponent<YS_PlayerShoot>();
        ys_skUI = GameObject.Find("Canvas").GetComponent<YS_SkillUI>();

        // 애니메이션
        anim = GetComponentInChildren<Animator>();

        // 이펙트위치
        walkdust_pos = GameObject.Find("Walkdust");

        // 이펙트사운드
        sound = GameObject.Find("Player").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.S) == false) // 아래로 몸을 숙였을 때는 움직임x
        {
            Vector3 myPos = transform.position;

            float h = Input.GetAxis("Horizontal");
            // 애니메이션
            anim.SetFloat("Move", h);
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack") || anim.GetCurrentAnimatorStateInfo(0).IsName("UpAttack"))
            {
                if(h != 0)
                {
                    anim.Play("Move");
                }
            }
            // 이펙트
            if (h != 0 && jump == false)
            {
                effectTime += Time.deltaTime;
                if(effectTime > 0.5f)
                {
                    GameObject walkDust = Instantiate(effectFactory);
                    walkDust.transform.position = walkdust_pos.transform.position;
                    effectTime = 0;
                }
            }

            dir = Vector3.right * h;
            dir.Normalize();

            myPos += dir * speed * Time.deltaTime;
            myPos.x = Mathf.Clamp(myPos.x, -width + 2.45f, width - 2.45f);

            transform.position = myPos;

            Jump();

            Dash();

            IsDown();

            if(b_dashTime == true)
            {
                DashEffect();
            }

            if(jumpPower == 20 || jumpPower == 60)
            {
                parryTime += Time.deltaTime;
                if(parryTime > 0.1f)
                {
                    jumpPower = 45;
                    parryTime = 0;
                    // 사운드
                    sound.Play();
                }
            }
        }
    }

    bool IsDown()
    {
        temp_loc = p_loc;
        p_loc = transform.position.y;

        return temp_loc > p_loc;
    }

    void Jump()
    {
        if(!Input.GetKey(KeyCode.S)) // 숙이고 있을 땐, 점프 안되게
        {
            // 점프
            if (Input.GetKeyDown(KeyCode.Space) && jump == false)
            {
                rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);

                // 스킬 포인트 증가(패링)
                if(jumpPower == 20 || jumpPower == 60)
                {
                    ys_ps.skillPoint++;
                    YS_DataBox.data.p_parry++;
                    // 패링 애니메이션
                    anim.SetTrigger("Parry");
                    // 이펙트
                    GameObject parryHit = Instantiate(effectFactory4);
                    parryHit.transform.position = transform.position;
                    // 스킬 게이지
                    num = ys_skUI.num;
                    ys_skUI.skillimg[num].fillAmount = 1;
                }

                jump = true;
                //애니메이션
                anim.SetBool("Jump", true);
                anim.Play("Jump");
            }
        }
    }

    void Dash()
    {
        // 대쉬 (왼쪽)
        if (Input.GetKeyDown(KeyCode.A) && l_dash == false)
        {
            l_dash = true;
        }
        else if (Input.GetKeyDown(KeyCode.A) && l_dash == true)
        {
            if (l_currentTime < 0.2f)
            {
                // 점프 중에 대쉬 하면, 점프 사라짐
                if (jump == true)
                {
                    rigid.isKinematic = true;
                    rigid.isKinematic = false;
                }
                // 이펙트
                GameObject dashDust = Instantiate(effectFactory2);
                GameObject dashDust2 = Instantiate(effectFactory3);
                Vector3 pos = walkdust_pos.transform.position;
                pos.y = gameObject.transform.position.y + 2f;
                dashDust.transform.position = pos;
                dashDust2.transform.position = pos;

                // 플레이어 사라지게
                gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;

                // 대쉬 이동
                transform.position = new Vector3(transform.position.x - 8f, transform.position.y, transform.position.z);
                l_dash = false;

                b_dashTime = true;
            }
            else
            {
                l_dash = false;
            }
        }

        if (l_dash == true)
        {
            l_currentTime += Time.deltaTime;
        }
        else
        {
            l_currentTime = 0;
        }

        if (l_currentTime > 0.2f)
        {
            l_currentTime = 0;
            l_dash = false;
        }

        // 대쉬 (오른쪽)
        if (Input.GetKeyDown(KeyCode.D) && r_dash == false)
        {
            r_dash = true;
        }
        else if (Input.GetKeyDown(KeyCode.D) && r_dash == true)
        {
            if (r_currentTime < 0.2f)
            {
                // 점프 중에 대쉬 하면, 점프 사라짐
                if (jump == true)
                {
                    rigid.isKinematic = true;
                    rigid.isKinematic = false;
                }
                // 이펙트
                GameObject dashDust = Instantiate(effectFactory2);
                GameObject dashDust2 = Instantiate(effectFactory3);
                Vector3 pos = walkdust_pos.transform.position;
                pos.y = gameObject.transform.position.y + 2f;
                dashDust.transform.position = pos;
                dashDust2.transform.position = pos;

                // 플레이어 사라지게
                gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;

                // 대쉬 이동
                transform.position = new Vector3(transform.position.x + 8f, transform.position.y, transform.position.z);
                r_dash = false;

                b_dashTime = true;
            }
            else
            {
                r_dash = false;
            }
        }

        if (r_dash == true)
        {
            r_currentTime += Time.deltaTime;
        }
        else
        {
            r_currentTime = 0;
        }

        if (r_currentTime > 0.2f)
        {
            r_currentTime = 0;
            r_dash = false;
        }

        // 대쉬 (아래)
        if (Input.GetKeyDown(KeyCode.S) && d_dash == false)
        {
            d_dash = true;
        }
        else if (Input.GetKeyDown(KeyCode.S) && d_dash == true)
        {
            if (d_currentTime < 0.2f)
            {
                // 점프 중에 대쉬 하면, 점프 사라짐
                if (jump == true)
                {
                    rigid.isKinematic = true;
                    rigid.isKinematic = false;
                }
                // 이펙트
                GameObject dashDust = Instantiate(effectFactory2);
                GameObject dashDust2 = Instantiate(effectFactory3);
                Vector3 pos = walkdust_pos.transform.position;
                pos.y = gameObject.transform.position.y + 2f;
                dashDust.transform.position = pos;
                dashDust2.transform.position = pos;

                // 플레이어 사라지게
                gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;

                if (transform.position.y - 8f >= 2.53462362f)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y - 8f, transform.position.z);
                }
                else if(transform.position.y - 8f < 2.53462362f)
                {
                    transform.position = new Vector3(transform.position.x, 2.53462362f, transform.position.z);
                }
                d_dash = false;

                b_dashTime = true;
            }
            else
            {
                d_dash = false;
            }
        }

        if (d_dash == true)
        {
            d_currentTime += Time.deltaTime;
        }
        else
        {
            d_currentTime = 0;
        }

        if (d_currentTime > 0.2f)
        {
            d_currentTime = 0;
            d_dash = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Contains("Ground"))
        {
            jump = false;
            //애니메이션
            anim.SetBool("Jump", false);
            // 점프 파워를 원래대로
            jumpPower = 45f;
        }
    }

    // 스킬포인트 얻기(패링)
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Parry"))
        {
            if(IsDown() == true)
            {
                jump = false;
                // 패링 시, 점프 파워
                jumpPower = 60f;
            }
            else
            {
                jump = false;
                // 패링 시, 점프 파워
                jumpPower = 20f;
            }
        }
    }

    void DashEffect()
    {
        // 대쉬 시간
        dashTime += Time.deltaTime;

        // 대쉬시간이 지나면 플레이어 다시 켜지게
        if (dashTime > 0.3f)
        {
            gameObject.GetComponentInChildren<SpriteRenderer>().enabled = true;

            // 이펙트
            GameObject dashDust3 = Instantiate(effectFactory2);
            GameObject dashDust4 = Instantiate(effectFactory3);
            Vector3 pos2 = walkdust_pos.transform.position;
            pos2.y = gameObject.transform.position.y + 2f;
            dashDust3.transform.position = pos2;
            dashDust4.transform.position = pos2;

            dashTime = 0;

            b_dashTime = false;
        }
    }
}
