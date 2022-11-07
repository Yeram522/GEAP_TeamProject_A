using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BossSkill 
{
    public int m_damage; //�ǰ� ������.
    public int m_coolTime; // �� Ÿ��.
    public bool m_Active; // ��ų Ȱ�� ����
    public delegate void m_Skill();
    m_Skill skill;
    
    public BossSkill(int damage, int coolTime, m_Skill _detail)
    {
        m_Active = false;
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
        yield return new WaitForSeconds(m_coolTime); //���
        m_Active = false;
        yield return null;
    }
}
