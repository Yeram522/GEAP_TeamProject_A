using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDamage : MonoBehaviour
{
    int power;

    private void Awake()
    {
        power = GameObject.Find("Player").GetComponent<PlayerStatus>().getPlayerDamage();
    }

    private void OnTriggerEnter(Collider other)
    {
        AttackInfo attack = new AttackInfo();

        attack.attackPower = power + 5;
        attack.attacker = this.transform;

        if (other.gameObject.tag.Equals("Enemy"))
        {
            other.SendMessage("Damage", attack);
            Debug.Log(other.name + "을 스킬로 때림");
        }
    }
}
