using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    int hp = 100;
    [SerializeField]
    int damage = 5;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // �������� ���� �Լ� ����
    void AttackPlayer(int damage)
    {
        hp -= damage;
        if (hp <= 0) 
        { 
            hp = 0; 
        }
    }
}
