using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class SimpleMeleeMobCtrl : MonoBehaviour
{
    private Movement movement;
    private ObjectStatus status;
    private SimpleMeleeMobAnimation mobAnimation;
    private AttackArea attackArea;
    
    public Transform attackTarget;
    
    public float range = 4.0f;
    
    public float waitBaseTime = 2.0f;

    private float waitTime;

    public float walkRange = 15.0f;

    public Vector3 basePosition;

    public GameObject[] dropItemPrefab;

    public GameObject bullet;

    public GameObject usingBullet;

    public AudioClip sound;
    
    enum State
    {
        Walking,
        Chasing,
        Attacking,
        Died,
    };

    [SerializeField]
    private State state = State.Walking;

    private State nextState = State.Walking;
    
    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<Movement>();
        basePosition = transform.position;
        waitTime = waitBaseTime;
        status = GetComponent<ObjectStatus>();
        mobAnimation = GetComponent<SimpleMeleeMobAnimation>();
        attackArea = GetComponentInChildren<AttackArea>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (state) {
            case State.Walking:
                Walking();
                break;
            case State.Chasing:
                Chasing();
                break;
            case State.Attacking:
                Attacking();
                break;
        }
        if (state != nextState)
        {
	        state = nextState;
	        switch (state) {
		        case State.Walking:
			        WalkStart();
			        break;
		        case State.Chasing:
			        ChaseStart();
			        break;
		        case State.Attacking:
			        AttackStart();
			        break;
		        case State.Died:
			        Died();
			        break;
	        }
        }
    }
    
    void ChangeState(State nextState)
	{
		this.nextState = nextState;
	}
	
	void WalkStart()
	{
		StateStartCommon();
		status.battleMode = false;
	}

    void Walking()
    {
        // 대기 시간이 아직 남았다면.
        if (waitTime > 0.0f)
        {
            // 대기 시간을 줄인다.
            waitTime -= Time.deltaTime;
            // 대기 시간이 없어지면.
            if (waitTime <= 0.0f)
            {
                // 범위 내의 어딘가.
                Vector2 randomValue = Random.insideUnitCircle * walkRange;
                // 이동할 곳을 설정한다.
                Vector3 destinationPosition = basePosition + new Vector3(randomValue.x, 0.0f, randomValue.y);
                // 목적지를 지정한다.
                SendMessage("SetDestination", destinationPosition);
            }
        }
        else
        {
            // 목적지에 도착한다.
            if (movement.Arrived())
            {
                // 대기 상태로 전환한다.
                waitTime = Random.Range(waitBaseTime, waitBaseTime * 2.0f);
            }
            // 타겟을 발견하면 추적한다.
            if (attackTarget)
            {
                ChangeState(State.Chasing);
            }
        }
    }
    // 추적 시작.
    void ChaseStart()
    {
        StateStartCommon();
        status.battleMode = true;
    }
    // 추적 중.
    void Chasing()
    {
	    if (attackTarget is null)
	    {
		    status.battleMode = false;
		    ChangeState(State.Walking);
		    return;
	    }
	    
	    // 이동할 곳을 플레이어에 설정한다.
	    SendMessage("SetDestination", attackTarget.position);
	    // Debug.Log(gameObject.name + " send Message : setDestination");
	    // 2미터 이내로 접근하면 공격한다.
	    if (Vector3.Distance( attackTarget.position, transform.position ) <= range)
	    {
		    ChangeState(State.Attacking);
	    }
    }

	// 공격 스테이트가 시작되기 전에 호출된다.
	void AttackStart()
	{
		StateStartCommon();
		status.attacking = true;
		
		// 적이 있는 방향으로 돌아본다.
		Vector3 targetDirection = (attackTarget.position-transform.position).normalized;
		SendMessage("SetDirection",targetDirection);
		
		// 이동을 멈춘다.
		SendMessage("StopMove");
	}
	
	// 공격 중 처리.
	void Attacking()
	{
		if (mobAnimation.IsAttacked())
			ChangeState(State.Chasing);
        // 대기 시간을 다시 설정한다.
        waitTime = Random.Range(waitBaseTime, waitBaseTime * 2.0f);
        // 타겟을 리셋한다.
        //attackTarget = null;
    }

    void DropItem()
    {
        if (dropItemPrefab.Length == 0) { return; }
        GameObject dropItem = dropItemPrefab[Random.Range(0, dropItemPrefab.Length)];
        Instantiate(dropItem, transform.position, Quaternion.identity);
    }

    void Died()
	{
        status.died = true;
        DropItem();

        GameObject.FindWithTag("UIManager").GetComponent<QuestManagerSystem>().SendMessage("NPCFirstQuestMessage");

        AudioSource.PlayClipAtPoint (sound, transform.position);
        
        gameObject.SetActive(false);
	}
	
	void Damage(AttackInfo attackInfo)
	{
		status.damaged = true;
		status.HP -= attackInfo.attackPower;
		if (status.HP <= 0) {
			status.HP = 0;
			// 체력이 0이므로 사망 스테이트로 전환한다.
            ChangeState(State.Died);
		}
	}
	
	// 스테이트가 시작되기 전에 스테이터스를 초기화한다.
	void StateStartCommon()
	{
		status.attacking = false;
		status.died = false;
		status.damaged = false;
		attackArea.SendMessage("OnAttackTermination");
	}
    // 공격 대상을 설정한다.
    public void SetAttackTarget(Transform target)
    {
	    attackTarget = target;
    }

    void ShootBullet()
    {

	    usingBullet.transform.position = transform.position + transform.forward;
	    usingBullet.transform.rotation = Quaternion.Euler(transform.forward);
	    usingBullet.SetActive(true);
	    
	    usingBullet.SendMessage("SetTarget", attackTarget);
    }
}
