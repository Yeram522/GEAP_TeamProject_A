using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkill : MonoBehaviour
{
    private int m_damage; //�ǰ� ������.
    private int m_coolTime; // �� Ÿ��.
    private bool m_Active; // ��ų Ȱ�� ����
    public delegate void m_Skill();

    public void startSkill(m_Skill _detail)
    {
        _detail();
        StartCoroutine(inCoolTime());
    }

    IEnumerator inCoolTime()
    {
        m_Active = true;
        yield return new WaitForSeconds(m_coolTime); //���
        m_Active = false;
        yield return null;
    }
}
