using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YS_PlayerHealth : MonoBehaviour
{
    // �÷��̾� �ִϸ��̼�
    Animator p_anim;
    // ����Ʈ
    public GameObject effectFactory;
    // �ִϸ��̼� �� ����Ʈ �Ѱ� ����
    int hp_temp;
    // �°� ���� �����ð� ����
    bool b_hp;
    float hpTime = 0;
    Color alpha;
    SpriteRenderer sprite;

    public Text curHP;

    public int hp = 3;

    public int HP
    {
        get
        {
            return hp;
        }
        set
        {
            hp = value;
            // ���� hp ���ŵ� ��, ȭ�鿡 ǥ��
            curHP.text = "HP. " + hp;

            if(hp <= 0)
            {
                //Destroy(gameObject);
                gameObject.SetActive(false);
            }
        }
    }
    
    public static YS_PlayerHealth Instance = null;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        curHP.text = "HP. " + hp;

        // �÷��̾� �ִϸ��̼� �Ҵ�
        p_anim = GameObject.Find("Player").GetComponentInChildren<Animator>();

        hp_temp = hp;

        sprite = GameObject.Find("Sprite").GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if(hp != hp_temp)
        {
            AnimationEffect();

            hp_temp = hp;

            b_hp = true;
        }

        if(b_hp == true)
        {
            Blink();
        }
    }

    void AnimationEffect()
    {
        // �÷��̾� �ִϸ��̼�
        p_anim.SetTrigger("Damaged");

        // ����Ʈ
        GameObject dust = Instantiate(effectFactory);
        dust.transform.position = transform.position;
    }

    void Blink()
    {
        hpTime += Time.deltaTime;

        if(hpTime < 2)
        {
            if(sprite.color.a >= 1f)
            {
                alpha = sprite.color;
                alpha.a = 0.5f;
                sprite.color = alpha;
            }
            else if(sprite.color.a <= 0.5f)
            {
                alpha = sprite.color;
                alpha.a = 1f;
                sprite.color = alpha;
            }
        }
        else
        {
            alpha = sprite.color;
            alpha.a = 1f;
            sprite.color = alpha;

            b_hp = false;
            hpTime = 0;
        }
    }
}
