using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YS_PlayerMove : MonoBehaviour
{
    float speed = 13f;
    Vector3 dir;
    public float width;
    // ������ ���� ������ٵ�
    Rigidbody rigid;
    public float jumpPower = 45f;
    // ���� ����
    public bool jump = false;
    // �뽬 ����
    bool l_dash, r_dash, d_dash = false;
    float l_currentTime, r_currentTime, d_currentTime = 0;
    // �÷��̾� �� ������Ʈ
    YS_PlayerShoot ys_ps;
    // �÷��̾� ��� �ϰ� �Ǵ�
    float p_loc, temp_loc;
    // �ִϸ��̼�
    Animator anim, walk_anim, dash_anim, dash2_anim;
    // ����Ʈ
    public GameObject effectFactory, effectFactory2, effectFactory3, effectFactory4, walkdust_pos;
    float effectTime, dashTime;
    bool b_dashTime = false;
    // ��ų ������
    YS_SkillUI ys_skUI;
    int num;
    // �и� �ð�
    float parryTime = 0;
    // ����Ʈ�����
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

        // �ִϸ��̼�
        anim = GetComponentInChildren<Animator>();

        // ����Ʈ��ġ
        walkdust_pos = GameObject.Find("Walkdust");

        // ����Ʈ����
        sound = GameObject.Find("Player").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.S) == false) // �Ʒ��� ���� ������ ���� ������x
        {
            Vector3 myPos = transform.position;

            float h = Input.GetAxis("Horizontal");
            // �ִϸ��̼�
            anim.SetFloat("Move", h);
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack") || anim.GetCurrentAnimatorStateInfo(0).IsName("UpAttack"))
            {
                if(h != 0)
                {
                    anim.Play("Move");
                }
            }
            // ����Ʈ
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
                    // ����
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
        if(!Input.GetKey(KeyCode.S)) // ���̰� ���� ��, ���� �ȵǰ�
        {
            // ����
            if (Input.GetKeyDown(KeyCode.Space) && jump == false)
            {
                rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);

                // ��ų ����Ʈ ����(�и�)
                if(jumpPower == 20 || jumpPower == 60)
                {
                    ys_ps.skillPoint++;
                    YS_DataBox.data.p_parry++;
                    // �и� �ִϸ��̼�
                    anim.SetTrigger("Parry");
                    // ����Ʈ
                    GameObject parryHit = Instantiate(effectFactory4);
                    parryHit.transform.position = transform.position;
                    // ��ų ������
                    num = ys_skUI.num;
                    ys_skUI.skillimg[num].fillAmount = 1;
                }

                jump = true;
                //�ִϸ��̼�
                anim.SetBool("Jump", true);
                anim.Play("Jump");
            }
        }
    }

    void Dash()
    {
        // �뽬 (����)
        if (Input.GetKeyDown(KeyCode.A) && l_dash == false)
        {
            l_dash = true;
        }
        else if (Input.GetKeyDown(KeyCode.A) && l_dash == true)
        {
            if (l_currentTime < 0.2f)
            {
                // ���� �߿� �뽬 �ϸ�, ���� �����
                if (jump == true)
                {
                    rigid.isKinematic = true;
                    rigid.isKinematic = false;
                }
                // ����Ʈ
                GameObject dashDust = Instantiate(effectFactory2);
                GameObject dashDust2 = Instantiate(effectFactory3);
                Vector3 pos = walkdust_pos.transform.position;
                pos.y = gameObject.transform.position.y + 2f;
                dashDust.transform.position = pos;
                dashDust2.transform.position = pos;

                // �÷��̾� �������
                gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;

                // �뽬 �̵�
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

        // �뽬 (������)
        if (Input.GetKeyDown(KeyCode.D) && r_dash == false)
        {
            r_dash = true;
        }
        else if (Input.GetKeyDown(KeyCode.D) && r_dash == true)
        {
            if (r_currentTime < 0.2f)
            {
                // ���� �߿� �뽬 �ϸ�, ���� �����
                if (jump == true)
                {
                    rigid.isKinematic = true;
                    rigid.isKinematic = false;
                }
                // ����Ʈ
                GameObject dashDust = Instantiate(effectFactory2);
                GameObject dashDust2 = Instantiate(effectFactory3);
                Vector3 pos = walkdust_pos.transform.position;
                pos.y = gameObject.transform.position.y + 2f;
                dashDust.transform.position = pos;
                dashDust2.transform.position = pos;

                // �÷��̾� �������
                gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;

                // �뽬 �̵�
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

        // �뽬 (�Ʒ�)
        if (Input.GetKeyDown(KeyCode.S) && d_dash == false)
        {
            d_dash = true;
        }
        else if (Input.GetKeyDown(KeyCode.S) && d_dash == true)
        {
            if (d_currentTime < 0.2f)
            {
                // ���� �߿� �뽬 �ϸ�, ���� �����
                if (jump == true)
                {
                    rigid.isKinematic = true;
                    rigid.isKinematic = false;
                }
                // ����Ʈ
                GameObject dashDust = Instantiate(effectFactory2);
                GameObject dashDust2 = Instantiate(effectFactory3);
                Vector3 pos = walkdust_pos.transform.position;
                pos.y = gameObject.transform.position.y + 2f;
                dashDust.transform.position = pos;
                dashDust2.transform.position = pos;

                // �÷��̾� �������
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
            //�ִϸ��̼�
            anim.SetBool("Jump", false);
            // ���� �Ŀ��� �������
            jumpPower = 45f;
        }
    }

    // ��ų����Ʈ ���(�и�)
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Parry"))
        {
            if(IsDown() == true)
            {
                jump = false;
                // �и� ��, ���� �Ŀ�
                jumpPower = 60f;
            }
            else
            {
                jump = false;
                // �и� ��, ���� �Ŀ�
                jumpPower = 20f;
            }
        }
    }

    void DashEffect()
    {
        // �뽬 �ð�
        dashTime += Time.deltaTime;

        // �뽬�ð��� ������ �÷��̾� �ٽ� ������
        if (dashTime > 0.3f)
        {
            gameObject.GetComponentInChildren<SpriteRenderer>().enabled = true;

            // ����Ʈ
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
