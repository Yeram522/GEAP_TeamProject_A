using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntientMonster : MonoBehaviour
{
    public CoolTimeHndl skillHndl;
    private Animator m_Animator;
    private bool m_isChasing;
    // Start is called before the first frame update
    void Start()
    {
        m_Animator = this.transform.GetComponent<Animator>();
        setBossSkillSets();
        m_isChasing = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_isChasing)
            Normal_Chasing();
        else
            m_Animator.SetBool("Walk Forward", false);
    }

    private void setBossSkillSets()
    {
        skillHndl.m_Skills = new List<BossSkill>();
        skillHndl.m_Skills.Add(new BossSkill(10, 5, smashAttack));
        skillHndl.m_Skills.Add(new BossSkill(10, 5, stabAttack));
    }

    private void Normal_Chasing()
    {
        int MoveSpeed = 5;
        int MinDist = 3;

        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        transform.LookAt(player);
        m_Animator.SetBool("Walk Forward",true);
        
        if (Vector3.Distance(transform.position, player.position) >= MinDist)
        {
            transform.position += transform.forward * MoveSpeed * Time.deltaTime;
        }
        else
            m_Animator.SetBool("Walk Forward", false);
    }

    private void castSpell()
    {
        m_isChasing = false;

        m_Animator.SetTrigger("Cast Spell");
        StartCoroutine(deleyTime());
    }

    private void smashAttack()
    {
        m_isChasing = false;

        int MinDist = 3;

        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        transform.LookAt(player);
        m_Animator.SetBool("Walk Forward", true);

        if (Vector3.Distance(transform.position, player.position) >= MinDist)
        {
            castSpell();
        }
        else
            m_Animator.SetTrigger("Smash Attack");


        StartCoroutine(deleyTime());
    }

    private void stabAttack()
    {

        m_isChasing = false;

        int MinDist = 3;

        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        transform.LookAt(player);
        m_Animator.SetBool("Walk Forward", true);

        if (Vector3.Distance(transform.position, player.position) >= MinDist)
        {
            castSpell();
        }
        else
            m_Animator.SetTrigger("Stab Attack");


        StartCoroutine(deleyTime());
    }

    IEnumerator deleyTime()
    {
        yield return new WaitForSeconds(1.0f);
        Debug.Log("dfdfg");
        m_isChasing = true;
        m_Animator.SetBool("Walk Forward", true);
        yield return null;
    }
}
