using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollider : MonoBehaviour
{
    private void OnParticleCollision(GameObject other)
    {
        Debug.Log(other.name);
        if (other.name != "Player") return;

        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>().SendMessage("AttackPlayer", 1.0f);
    }
}
