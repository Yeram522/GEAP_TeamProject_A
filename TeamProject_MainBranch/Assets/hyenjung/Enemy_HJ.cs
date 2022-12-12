using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_HJ : MonoBehaviour
{
    public int maxHealth;
    public int curHealth;

    Rigidbody rigid;
    BoxCollider boxCollider;
    Material mat;

    void Awake()
    {
        mat = transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material;
        curHealth = maxHealth;
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
    }

    void Update()
    {
        //if(curHealth>=0)
        //{ gameObject.SetActive(false); }

    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag =="sword")
        {
            curHealth -= 5;
            Debug.Log("적 맞음");
            if(curHealth<=0)
            { gameObject.SetActive(false); }
            //StartCoroutine("OnDamage");
        }
    }

    IEnumerator OnDamage()
    {

        mat.color = Color.red;
        yield return new WaitForSeconds(0.1f);

        if (curHealth > 0)
        {
            mat.color = Color.white;
            Debug.Log("적 맞음");
        }
        else
        {
            Debug.Log("적 죽음");
            mat.color = Color.gray;
            Destroy(gameObject, 2);
        }


    }
}
