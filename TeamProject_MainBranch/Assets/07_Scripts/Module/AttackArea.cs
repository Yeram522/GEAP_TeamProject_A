using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackInfo
{
    public int attackPower; // 이 공격의 공격력.
    public Transform attacker; // 공격자.
}

public class AttackArea : MonoBehaviour
{
    
    private ObjectStatus status;

    private Collider attackCollider;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        status = transform.root.GetComponent<ObjectStatus>();
        attackCollider = GetComponent<Collider>();
        animator = GetComponent<Animator>();

        attackCollider.enabled = false;
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
    
    // 공격 정보를 가져온다.
    AttackInfo GetAttackInfo()
    {			
        AttackInfo attackInfo = new AttackInfo();
        // 공격력 계산.
        attackInfo.attackPower = status.Power;
        attackInfo.attacker = transform.root;
		
        return attackInfo;
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player") return;
        
        other.SendMessage("AttackPlayer",GetAttackInfo().attackPower);

        status.lastAttackTarget = other.transform.root.gameObject;
    }
    
    // 공격 판정을 유효로 한다.
    void OnAttack()
    {
        attackCollider.enabled = true;
    }
    
    // 공격 판정을 무효로 한다.
    void OnAttackTermination()
    {
        attackCollider.enabled = false;
    }
}
