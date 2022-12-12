using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementRainCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.name != "Player") return;

        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>().SendMessage("AttackPlayer", 5.0f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name != "Player") return;

        Destroy(this.gameObject);
    }
}
