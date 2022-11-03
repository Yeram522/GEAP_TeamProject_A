using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetAI : MonoBehaviour
{
    Animator anim;
    Rigidbody rigid;
    Transform prevTransform;

    void Start()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        prevTransform = this.transform;
    }

    
    void Update()
    {
        if (prevTransform.position == this.transform.position)
            anim.SetBool("isMove", false);
        else
        {
            anim.SetBool("isMove", true);
            prevTransform.position = this.transform.position;
        }
    }
}
