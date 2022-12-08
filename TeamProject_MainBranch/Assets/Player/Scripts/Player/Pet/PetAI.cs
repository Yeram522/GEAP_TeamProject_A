using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum PET_CLASS
{
    HEAL = 0,
    ATTACK,
    DEFENCE
};

public class PetAI : MonoBehaviour
{

    Animator anim;
    Rigidbody rigid;
    Transform prevTransform;

    // ���� Ŭ������ ���� (����Ʈ�� ġ�����̴�)
    [SerializeField]
    PET_CLASS petClass = PET_CLASS.HEAL;

    PlayerStatus playerStatus;

    // ���� ���� ��� ��Ÿ��
    float ct_skillMax = 30.0f;

    [SerializeField]
    float ct_skill = 0.0f;
    [SerializeField]
    GameObject ef_DefenceSKill;

    [SerializeField]
    public bool RecognizeEnemy = false;


    [SerializeField]
    bool skillCoolTime = false;

    public bool isUsingSkill = false;

    [SerializeField]
    GameObject Potion;

    void Start()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        prevTransform = this.transform;

        playerStatus = GameObject.Find("Player").GetComponent<PlayerStatus>();
    }

    void Update()
    {
        SetAnimation();

        UpdateCoolTime();

        if (skillCoolTime) Skill();



    }

    void UpdateCoolTime()
    {
        if (ct_skill >= ct_skillMax)
        {
            skillCoolTime = true;
        }
        else
        {
            ct_skill += Time.deltaTime;
        }
    }

    void StartSkillMotion()
    {
        isUsingSkill = true;
        Debug.Log("StartSKillMotion");
    }
    void EndSkillMotion()
    {
        isUsingSkill = false;
        Debug.Log("EndSKillMotion");
    }

    void Skill()
    {
        if (petClass == PET_CLASS.HEAL)
        {
            // �÷��̾��� ü���� �޾��ִ� ��쿡�� ��ų�� ���
            if (playerStatus.getPlayerHP() < playerStatus.hp_max)
            {
                CreatePotion();
            }
        }
        else if (petClass == PET_CLASS.ATTACK)
        {

        }
        else if (petClass == PET_CLASS.DEFENCE)
        {
            if (RecognizeEnemy)
            {
                CreateDefenseArea();
            }
        }
    }

    void CreatePotion()
    {
        anim.SetTrigger("CreatePotion");

        skillCoolTime = false;
        ct_skill = 0.0f;
    }

    void CreateDefenseArea()
    {
        anim.SetTrigger("DeffenceAreaExpansion");

        skillCoolTime = false;
        ct_skill = 0.0f;
    }

    void InstancePotion()
    {
        GameObject potion = Instantiate(Potion);
        potion.transform.position = transform.position;

        Debug.Log("Instance Potion");
    }
    void DefenceAreaOn()
    {
        GameObject zone = Instantiate(ef_DefenceSKill);
        zone.transform.position = transform.position;

        Debug.Log("Active DefencArea");
    }


    void SetAnimation()
    {

    }
}