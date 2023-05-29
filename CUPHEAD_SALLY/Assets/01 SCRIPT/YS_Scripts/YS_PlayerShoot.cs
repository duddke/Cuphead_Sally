using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YS_PlayerShoot : MonoBehaviour
{
    // state를 사용하여 1키 일 때 기본 총, 2키 일 때 확산포, 3키 일 때 유도탄 으로
    // 1키 누르면 1키 state, 2키 누르면 2키 state, 3키 누르면 3키 state
    int shootState = 1;

    public GameObject bulletFactory, bulletFactory2, bulletFactory3, skillFactory, skilleffectFactory, skilleffectFactory2, skilleffectFactory3;
    GameObject shootPosition, shootPosition2, shootPosition3, shootPosition4, shootPosition5, skillPosition;
    // 스킬 시간, 스킬 중 판단, 스킬 게이지
    float currentTime;
    public bool b_skill = false;
    public float skillPoint = 0;
    public int skillCount = 0;

    YS_PlayerRotate ys_pr;
    YS_PlayerMove ys_pm;

    // 최초 포지션 저장
    Vector3 init_shoot, init_skill;

    // 애니메이션
    Animator anim;
    GameObject sprite;

    // 이펙트
    float effectTime;

    // 배경색
    Image black;

    // Start is called before the first frame update
    void Start()
    {
        shootPosition = GameObject.Find("ShootPosition");
        shootPosition2 = GameObject.Find("ShootPosition2");
        shootPosition3 = GameObject.Find("ShootPosition3");
        shootPosition4 = GameObject.Find("ShootPosition4");
        shootPosition5 = GameObject.Find("ShootPosition5");
        skillPosition = GameObject.Find("SkillPosition");

        ys_pr = gameObject.GetComponent<YS_PlayerRotate>();
        ys_pm = gameObject.GetComponent<YS_PlayerMove>();

        // 애니메이션
        anim = GetComponentInChildren<Animator>();
        sprite = GameObject.Find("Sprite");

        // 배경색
        black = GameObject.Find("BlackImage2").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        init_shoot = shootPosition.transform.position;
        init_skill = skillPosition.transform.position;

        ChangeWeapon();

        Shoot();

        if(Input.GetKeyUp(KeyCode.T))
        {
            anim.SetBool("RunAttack", false);
            anim.SetBool("RunAttack60", false);
        }
    }

    void ChangeWeapon()
    {
        // 키로 총알 바꾸기
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            shootState = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            shootState = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            shootState = 3;
        }
    }

    public void Shoot()
    {
        // 기본 공격
        if (Input.GetKeyDown(KeyCode.T) && shootState == 1)
        {
            // 애니메이션
            if (Input.GetKey(KeyCode.W))
            {
                if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
                {
                    if(ys_pm.jump == false)
                    {
                        anim.SetTrigger("RunAttack60");
                        if(!anim.GetCurrentAnimatorStateInfo(0).IsName("RunAttack60"))
                        {
                            anim.Play("RunAttack60");
                        }
                    }
                }
                else
                {
                    anim.SetTrigger("UpAttack");
                    anim.Play("UpAttack");

                }
            }
            else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                if(ys_pm.jump == false)
                {
                    anim.SetTrigger("RunAttack");
                    if (!anim.GetCurrentAnimatorStateInfo(0).IsName("RunAttack"))
                    {
                        anim.Play("RunAttack");
                    }
                }
            }
            else
            {
                if(ys_pm.jump == false)
                {
                    anim.SetTrigger("Attack");
                    anim.Play("Attack");
                }
            }

            GameObject bullet = Instantiate(bulletFactory);

            // 몸 숙이고 슛
            if (ys_pr.b_down == true)
            {
                bullet.transform.position = skillPosition.transform.position;
            }
            else
            {
                bullet.transform.position = shootPosition.transform.position;
            }
        }
        // 확산포
        if (Input.GetKeyDown(KeyCode.T) && shootState == 2)
        {
            // 애니메이션
            if (Input.GetKey(KeyCode.W))
            {
                if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
                {
                    anim.SetTrigger("RunAttack60");
                    if (!anim.GetCurrentAnimatorStateInfo(0).IsName("RunAttack60"))
                    {
                        anim.Play("RunAttack60");
                    }
                }
                else
                {
                    anim.SetTrigger("UpAttack");
                    anim.Play("UpAttack");
                }
            }
            else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                anim.SetTrigger("RunAttack");
                if (!anim.GetCurrentAnimatorStateInfo(0).IsName("RunAttack"))
                {
                    anim.Play("RunAttack");
                }
            }
            else
            {
                anim.SetTrigger("Attack");
                anim.Play("Attack");
            }

            if (ys_pr.b_down == true)
            {
                shootPosition.transform.position = init_skill;
            }
            else
            {
                shootPosition.transform.position = init_shoot;
            }

            GameObject bullet2 = Instantiate(bulletFactory2);
            YS_PlayerBullet2 ys_pb2 = bullet2.GetComponent<YS_PlayerBullet2>();
            ys_pb2.speed = 30f;
            ys_pb2.destroyTime = 0.3f;
            ys_pb2.dir = shootPosition.transform.right;
            // 몸 숙였을 때
            if (ys_pr.b_down == true)
            {
                ys_pb2.dir = shootPosition.transform.up;
            }
            bullet2.transform.position = shootPosition.transform.position;

            GameObject bullet2_1 = Instantiate(bulletFactory2);
            YS_PlayerBullet2 ys_pb2_1 = bullet2_1.GetComponent<YS_PlayerBullet2>();
            ys_pb2_1.speed = 20f;
            ys_pb2_1.destroyTime = 0.4f;
            ys_pb2_1.dir = shootPosition2.transform.right;
            // 몸 숙였을 때
            if (ys_pr.b_down == true)
            {
                ys_pb2_1.dir = shootPosition2.transform.up;
            }
            bullet2_1.transform.position = shootPosition2.transform.position;

            GameObject bullet2_2 = Instantiate(bulletFactory2);
            YS_PlayerBullet2 ys_pb2_2 = bullet2_2.GetComponent<YS_PlayerBullet2>();
            ys_pb2_2.speed = 15f;
            ys_pb2_2.destroyTime = 0.5f;
            ys_pb2_2.dir = shootPosition3.transform.right;
            // 몸 숙였을 때
            if (ys_pr.b_down == true)
            {
                ys_pb2_2.dir = shootPosition3.transform.up;
            }
            bullet2_2.transform.position = shootPosition3.transform.position;

            GameObject bullet2_3 = Instantiate(bulletFactory2);
            YS_PlayerBullet2 ys_pb2_3 = bullet2_3.GetComponent<YS_PlayerBullet2>();
            ys_pb2_3.speed = 20f;
            ys_pb2_3.destroyTime = 0.4f;
            ys_pb2_3.dir = shootPosition4.transform.right;
            // 몸 숙였을 때
            if (ys_pr.b_down == true)
            {
                ys_pb2_3.dir = shootPosition4.transform.up;
            }
            bullet2_3.transform.position = shootPosition4.transform.position;

            GameObject bullet2_4 = Instantiate(bulletFactory2);
            YS_PlayerBullet2 ys_pb2_4 = bullet2_4.GetComponent<YS_PlayerBullet2>();
            ys_pb2_4.speed = 15f;
            ys_pb2_4.destroyTime = 0.5f;
            ys_pb2_4.dir = shootPosition5.transform.right;
            // 몸 숙였을 때
            if (ys_pr.b_down == true)
            {
                ys_pb2_4.dir = shootPosition5.transform.up;
            }
            bullet2_4.transform.position = shootPosition5.transform.position;
        }
        // 유도탄
        if (Input.GetKeyDown(KeyCode.T) && shootState == 3)
        {
            // 애니메이션
            if (Input.GetKey(KeyCode.W))
            {
                if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
                {
                    anim.SetTrigger("RunAttack60");
                    if (!anim.GetCurrentAnimatorStateInfo(0).IsName("RunAttack60"))
                    {
                        anim.Play("RunAttack60");
                    }
                }
                else
                {
                    anim.SetTrigger("UpAttack");
                    anim.Play("UpAttack");
                }
            }
            else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                anim.SetTrigger("RunAttack");
                if (!anim.GetCurrentAnimatorStateInfo(0).IsName("RunAttack"))
                {
                    anim.Play("RunAttack");
                }
            }
            else
            {
                anim.SetTrigger("Attack");
                anim.Play("Attack");
            }

            if (ys_pr.b_down == true)
            {
                shootPosition.transform.position = init_skill;
            }
            else
            {
                shootPosition.transform.position = init_shoot;
            }

            GameObject bullet3 = Instantiate(bulletFactory3);

            bullet3.transform.position = shootPosition.transform.position;
        }

        // 필살기
        if (Input.GetKeyDown(KeyCode.Y) && skillPoint >= 5)
        {
            b_skill = true;
            skillPoint = 0;
            // 플레이어 콜라이더 꺼주기(데미지 안받도록)
            CapsuleCollider col = GetComponent<CapsuleCollider>();
            col.enabled = false;

            // 애니메이션
            anim.SetTrigger("Skill");
            GameObject skillIntro = Instantiate(skilleffectFactory2);
            skillIntro.transform.position = gameObject.transform.position;
            GameObject line = Instantiate(skilleffectFactory3);
            black.enabled = true;
        }

        // 이펙트
        if(effectTime > 1f && effectTime < 2)
        {
            GameObject skilleffect = Instantiate(skilleffectFactory);
            //skilleffect.transform.position = shootPosition.transform.position;
            Vector3 pos = shootPosition.transform.position;
            pos.y -= 0.4f;
            skilleffect.transform.position = pos;
            effectTime = 3;

            // 배경색 끄기
            black.enabled = false;
        }

        // 필살기 발사
        if (b_skill == true)
        {
            currentTime += Time.deltaTime;
            effectTime += Time.deltaTime;

            if (currentTime < 2.7f)
            {
                GameObject skill = Instantiate(skillFactory);
                //skill.transform.position = skillPosition.transform.position;
                skill.transform.position = shootPosition.transform.position;
            }
            else
            {
                b_skill = false;
                ys_pr.rigid.isKinematic = false;
                currentTime = 0;
                effectTime = 0;
                // 플레이어 콜라이더 켜주기(데미지 다시 받도록)
                CapsuleCollider col = GetComponent<CapsuleCollider>();
                col.enabled = true;

                /*b_skill = false;
                currentTime = 0;
                ys_pr.rigid.isKinematic = false;
                // 플레이어 원위치
                if (ys_pr.transform.eulerAngles.y == 0)
                {
                    ys_pr.transform.rotation = new Quaternion(0, 0, 0, 0);
                }
                else if (ys_pr.transform.eulerAngles.y == 1)
                {
                    ys_pr.transform.rotation = new Quaternion(0, 180, 0, 0);
                }*/
            }
        }
    }
}
