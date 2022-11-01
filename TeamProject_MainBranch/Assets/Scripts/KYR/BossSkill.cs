using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BossSkill 
{
    public int m_damage; //피격 데미지.
    public int m_coolTime; // 쿨 타임.
    public bool m_Active; // 스킬 활성 상태
    public delegate void m_Skill();
    m_Skill skill;
    
    public BossSkill(int damage, int coolTime, m_Skill _detail)
    {
        m_damage = damage;
        m_coolTime = coolTime;
        skill = _detail;

    }

    public void startSkill(CoolTimeHndl hndl)
    {
        skill();
        hndl.StartCoroutine(inCoolTime());
    }

    IEnumerator inCoolTime()
    {
        m_Active = true;
        yield return new WaitForSeconds(m_coolTime); //대기
        m_Active = false;
        yield return null;
    }
}
