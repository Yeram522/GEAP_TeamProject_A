using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;

public class FollowPlayer : MonoBehaviour
{
    NavMeshAgent nav;
    [SerializeField]
    GameObject  range;
    Animator anim;
    GameObject target;

    bool isOutofrange;
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        target = GameObject.Find("Player");
        isOutofrange = true;
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        isOutofrange = range.GetComponent<check>().isOut;

        // 스킬을 사용중인 경우에는 움직이지 않는다.
        if (GetComponent<PetAI>().isUsingSkill)
        {
            nav.speed = 0.0f;
        }
        else
        {
            nav.speed = 10.0f;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (isOutofrange)
        {
            try
            {
                nav.SetDestination(target.transform.position);
                anim.SetBool("isMove", true);
            }
            catch { }
        }
        else
        {
            try
            {
                nav.SetDestination(transform.position);
                anim.SetBool("isMove", false);
            }
            catch { }


        }

    }
}
