using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkill : MonoBehaviour
{
    private int m_damage; //피격 데미지.
    private int m_coolTime; // 쿨 타임.
    private bool m_Active; // 스킬 활성 상태
    public delegate void m_Skill();

    public void startSkill(m_Skill _detail)
    {
        _detail();
        StartCoroutine(inCoolTime());
    }

    IEnumerator inCoolTime()
    {
        m_Active = true;
        yield return new WaitForSeconds(m_coolTime); //대기
        m_Active = false;
        yield return null;
    }
}
