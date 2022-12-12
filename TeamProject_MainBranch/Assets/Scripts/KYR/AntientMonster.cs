using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


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


        if (GameObject.Find("UI").transform.GetChild(0).GetComponent<Slider>().value == 0.0f)
        {
            GameObject.FindWithTag("UIManager").GetComponent<QuestManagerSystem>().SendMessage("NPCSecondtQuestMessage");
            Destroy(this.gameObject);
            GameObject.Find("SceneLoader").GetComponent<GameSceneManager>().SendMessage("clearBossStage");
        }
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
        int MinDist = 10;

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

    IEnumerator runToPlayer()
    {
        int MinDist = 3;
        int MoveSpeed = 10;

        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        while (Vector3.Distance(transform.position, player.position) >= MinDist)
        {
            transform.position += transform.forward * MoveSpeed * Time.deltaTime;
        }


        transform.LookAt(player);
        m_Animator.SetBool("Walk Forward", true);
        m_Animator.SetTrigger("Stab Attack");

        yield return new WaitForSeconds(1.0f);
        m_isChasing = true;
        m_Animator.SetBool("Walk Forward", true);

        yield return null;
    }
    private void stabAttack()
    {

        m_isChasing = false;

       

        StartCoroutine(runToPlayer());

      //  StartCoroutine(deleyTime());
    }

    IEnumerator deleyTime()
    {
        yield return new WaitForSeconds(1.0f);
        Debug.Log("dfdfg");
        m_isChasing = true;
        m_Animator.SetBool("Walk Forward", true);
        yield return null;
    }

    public void Damage(AttackInfo damage)
    {
        GameObject ui = GameObject.Find("UI");

        ui.transform.GetChild(0).GetComponent<Slider>().value -= (damage.attackPower * 0.05f);
        ui.transform.GetChild(1).GetComponent<Text>().text = (ui.transform.GetChild(0).GetComponent<Slider>().value * 100).ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name != "Player") return;

        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>().SendMessage("AttackPlayer", 10.0f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }

    public void StartAttackHit()
    {
        this.gameObject.GetComponent<SphereCollider>().enabled = true;
    }

    public void EndAttackHit()
    {
        this.gameObject.GetComponent<SphereCollider>().enabled = false;
    }

    public void EndAttack()
    {
        this.gameObject.GetComponent<SphereCollider>().enabled = false;
    }
}
