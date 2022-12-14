using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class SimpleRangeMobCtrl : MonoBehaviour
{
    private Movement movement;
    private ObjectStatus status;
    private SimpleRangeMobAnimation mobAnimation;
    
    public Transform attackTarget;

    public float range = 4.0f;
    
    public float waitBaseTime = 2.0f;

    private float waitTime;

    public float walkRange = 15.0f;

    public Vector3 basePosition;

    public GameObject[] dropItemPrefab;

    public GameObject bullet;

    public GameObject usingBullet;

    enum State
    {
        Walking,
        Chasing,
        Attacking,
        Escaping,
        Died,
    };

    [SerializeField]
    private State state = State.Walking;

    private State nextState = State.Walking;
    
    // Start is called before the first frame update
    void Start()
    {
	    usingBullet = Instantiate(
		    bullet,
		    Vector3.zero, 
		    Quaternion.identity
	    ) as GameObject;
	    usingBullet.SetActive(false);
        movement = GetComponent<Movement>();
        basePosition = transform.position;
        waitTime = waitBaseTime;
        status = GetComponent<ObjectStatus>();
        mobAnimation = GetComponent<SimpleRangeMobAnimation>();
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
            case State.Escaping:
	            Escaping();
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
		        case State.Escaping:
			        EscapeStart();
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
        // ?????? ????????? ?????? ????????????.
        if (waitTime > 0.0f)
        {
            // ?????? ????????? ?????????.
            waitTime -= Time.deltaTime;
            // ?????? ????????? ????????????.
            if (waitTime <= 0.0f)
            {
                // ?????? ?????? ?????????.
                Vector2 randomValue = Random.insideUnitCircle * walkRange;
                // ????????? ?????? ????????????.
                Vector3 destinationPosition = basePosition + new Vector3(randomValue.x, 0.0f, randomValue.y);
                // ???????????? ????????????.
                SendMessage("SetDestination", destinationPosition);
            }
        }
        else
        {
            // ???????????? ????????????.
            if (movement.Arrived())
            {
                // ?????? ????????? ????????????.
                waitTime = Random.Range(waitBaseTime, waitBaseTime * 2.0f);
            }
            // ????????? ???????????? ????????????.
            if (attackTarget)
            {
                ChangeState(State.Chasing);
            }
        }
    }
    // ?????? ??????.
    void ChaseStart()
    {
        StateStartCommon();
        status.battleMode = true;
    }
    // ?????? ???.
    void Chasing()
    {
	    if (attackTarget is null)
	    {
		    status.battleMode = false;
		    ChangeState(State.Walking);
		    return;
	    }

		    // ????????? ?????? ??????????????? ????????????.
	    SendMessage("SetDestination", attackTarget.position);
	    // 2?????? ????????? ???????????? ????????????.
	    if (Vector3.Distance( attackTarget.position, transform.position ) <= range
	        && !usingBullet.activeSelf)
	    {
		    ChangeState(State.Attacking);
	    }
	    else if (Vector3.Distance(attackTarget.position, transform.position) <= range
	             && usingBullet.activeSelf)
	    {
		    ChangeState(State.Walking);
	    }
    }

	// ?????? ??????????????? ???????????? ?????? ????????????.
	void AttackStart()
	{
		StateStartCommon();
		status.attacking = true;
		
		// ?????? ?????? ???????????? ????????????.
		Vector3 targetDirection = (attackTarget.position-transform.position).normalized;
		SendMessage("SetDirection",targetDirection);
		
		// ????????? ?????????.
		SendMessage("StopMove");
	}
	
	// ?????? ??? ??????.
	void Attacking()
	{
		if (mobAnimation.IsAttacked())
			ChangeState(State.Escaping);
        // ?????? ????????? ?????? ????????????.
        waitTime = Random.Range(waitBaseTime, waitBaseTime * 2.0f);
        // ????????? ????????????.
        //attackTarget = null;
    }

	void EscapeStart()
	{
		StateStartCommon();
	}

	void Escaping()
	{
		if(!usingBullet.activeSelf)
			ChangeState(State.Chasing);

		Vector3 directionForRunAway = this.transform.position - attackTarget.transform.position;
		
		movement.SendMessage("SetDestination", (directionForRunAway.normalized * 3));
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
        
        this.gameObject.SetActive(false);
	}
	
	void Damage(AttackInfo attackInfo)
	{
		status.damaged = true;
		status.HP -= attackInfo.attackPower;
		if (status.HP <= 0) {
			status.HP = 0;
			// ????????? 0????????? ?????? ??????????????? ????????????.
            ChangeState(State.Died);
		}
	}
	
	// ??????????????? ???????????? ?????? ?????????????????? ???????????????.
	void StateStartCommon()
	{
		status.attacking = false;
		status.died = false;
		status.damaged = false;
	}
    // ?????? ????????? ????????????.
    public void SetAttackTarget(Transform target)
    {
	    attackTarget = target;
    }

    void ShootBullet()
    {
	    usingBullet.transform.position = transform.position + transform.forward + Vector3.up * 1;
	    usingBullet.transform.rotation = Quaternion.Euler(transform.forward);
	    usingBullet.GetComponent<Bullet>().currentDirection = (attackTarget.position - transform.position).normalized;
	    usingBullet.SetActive(true);
	    
	    usingBullet.SendMessage("SetTarget", attackTarget);
    }
}
