using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolTimeHndl : MonoBehaviour
{
    public List<BossSkill> m_NormalSkills; //보스가 가진 기본 패턴 

    bool isAttack()
    {
        foreach (BossSkill skill in m_NormalSkills)
        {
            if (skill.m_Active) return true;
        }

        return false;
    }

    void startAttack()
    {
        List<BossSkill> m_PSkills = new List<BossSkill>() ;
        foreach (BossSkill skill in m_NormalSkills)
        {
            if (!skill.m_Active)
                m_PSkills.Add(skill);
        }
        if (m_PSkills.Count == 0) return;
        int idx = Random.Range(0, m_PSkills.Count);
        m_PSkills[idx].startSkill(this);

    }
    void Update()
    {
        if (!isAttack())
        {
            startAttack();
        }
    }
}
