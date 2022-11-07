using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatusAndUI : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    int hp = 100;
    [SerializeField]
    int damage = 5;

    private Player status;
    
    void Start()
    {
        status = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HealPlayer(int value)
    {
        hp += value;
        if (hp > 100)
            hp = 100;
        
        status.HP = hp;
    }

    // �������� ���� �Լ� ����
    void AttackPlayer(int damage)
    {
        hp -= damage;
        
        if (hp <= 0) 
        { 
            hp = 0; 
        }
        
        status.HP = hp;
    }
}
