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
    PET_CLASS petClass = PET_CLASS.HEAL;

    PlayerStatus playerStatus;

    // 포션 생성 기술 쿨타임
    float ct_skillMax = 30.0f;
    [SerializeField]
    float ct_skill = 0.0f;

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

        if(skillCoolTime) Skill();



    }

    void UpdateCoolTime()
    {
        if(ct_skill >= ct_skillMax)
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
            // 플레이어의 체력이 달아있는 경우에만 스킬을 사용
            if (playerStatus.getPlayerHP() < playerStatus.hp_max)
                CreatePotion();
        }
        else if (petClass == PET_CLASS.ATTACK)
        { }
        else if (petClass == PET_CLASS.DEFENCE)
        { }
    }

    void CreatePotion()
    {
        anim.SetTrigger("CreatePotion");

        skillCoolTime = false;
        ct_skill = 0.0f;
    }

    void InstancePotion()
    {
        GameObject potion = Instantiate(Potion);
        potion.transform.position = transform.position;

        Debug.Log("Instance Potion");
    }


    void SetAnimation()
    {

    }
}
