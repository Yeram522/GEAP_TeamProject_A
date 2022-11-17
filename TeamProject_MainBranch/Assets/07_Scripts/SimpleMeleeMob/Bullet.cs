using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private HitArea hit;
    
    public GameObject bullet;

    public Transform attackTarget;

    private Quaternion currentDirection;
    
    public float velocity = 2.0f;
    public float maxRotate = 0.3f;
    public float maxAliveTime = 5.0f;
    
    private float aliveTime = 0.0f;
    

    // Start is called before the first frame update
    void Start()
    {
        currentDirection.eulerAngles = (attackTarget.position - transform.position);
        currentDirection.y = 0;
        currentDirection = currentDirection.normalized;
        hit = GetComponent<HitArea>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += currentDirection.eulerAngles * (velocity * Time.deltaTime);
        
        Quaternion goalDirection = Quaternion.Euler(attackTarget.position - transform.position);
        goalDirection.y = 0;
        goalDirection = goalDirection.normalized;
        
        currentDirection = Quaternion.RotateTowards(currentDirection, goalDirection, maxRotate * Time.deltaTime);
        
        aliveTime += Time.deltaTime;
        if ( hit.damageTrigger == true)
        {
            this.gameObject.SetActive(false);
        }
    }

    void SetTarget(Transform target)
    {
        attackTarget = target;
    }
    
}
