using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5f;
    public float MaxHp = 100f;

    public float HP = 100f;

    public bool isClicked_R_;
    public bool isClicked_L_;
    public bool Shift_3f_;
    public bool W_;
    public bool A_;
    public bool S_;
    public bool D_;
    public bool E_;
    public bool Space_;

    public bool firstQ;
    public bool senendQ;


    private void Start()
    {
        firstQ = false;
        W_ = false;
        A_ = false;
        S_ = false;
        D_ = false;
        Space_ = false;
        Shift_3f_ = false;
        senendQ= false;
        isClicked_R_ = false;
        isClicked_L_ = false;
        E_ = false;


        HP = MaxHp;
    }

    void Update()
    {
        Movement();
        firstQ_check();
        secendQ_check();
        if (firstQ_check())
        {
            if (Input.GetMouseButtonDown(0))
            {
                isClicked_R_ = true;
            }
            else if (Input.GetMouseButtonDown(1))
            {
                isClicked_L_ = true;
            }
            else if (Input.GetKey(KeyCode.E))
            {
                E_ = true;
            }
        }
    }

    public void Movement()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-speed * Time.deltaTime, 0, 0);
            A_ = true;

        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(speed * Time.deltaTime, 0, 0);
           D_ = true;

        }
        else if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(0, 0, speed * Time.deltaTime);
           W_ = true;

        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(0, 0, -speed * Time.deltaTime);
           S_ = true;
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            Space_ = true;
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            Shift_3f_ = true;
        }
    }

    public bool firstQ_check()
    {
        if (W_ && A_ && S_ && D_ && Space_ && Shift_3f_)
        {
            firstQ = true;
            Debug.Log("첫퀘스트완료");
        }
        return firstQ;
    }
    public void secendQ_check()
    {
        if (isClicked_L_ && isClicked_R_&&E_)
        {
            Debug.Log("두번째퀘스트완료");
            senendQ = true;
        }
    }

    private void FixedUpdate()
    {
        if (HP == 0)
            return;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Enemy")
        {
            if(HP>0)
            {
                HP -= 10;
            }
            else
            {
                Debug.Log("체력이 없습니다.");
            }
        }
    }
}