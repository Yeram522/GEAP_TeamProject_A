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

        // ��ų�� ������� ��쿡�� �������� �ʴ´�.
        if (GetComponent<PetAI>().isUsingSkill)
        {
            nav.speed = 0.0f;
        }
        else
        {
            nav.speed = 5.0f;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(isOutofrange)
        {
            nav.SetDestination(target.transform.position);
            anim.SetBool("isMove", true);
        }
        else
        {
            nav.SetDestination(transform.position);
            anim.SetBool("isMove", false);
        }

    }
}
