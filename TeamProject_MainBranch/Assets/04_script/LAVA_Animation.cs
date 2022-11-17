using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LAVA_Animation : MonoBehaviour
{
    public Animator anim;

    public void Awake()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("attack1", false);
        anim.SetBool("fireattack", false);
    }
    
    void animUpdate()
    {
        if (Input.GetKeyDown("Space"))
        {
            anim.SetBool("attack1", true);
        }
        if (Input.GetKeyDown("W"))
        {
            anim.SetBool("fireattack", true);
        }
        
    }
    

}
