using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIpumpkin : MonoBehaviour
{
    public Transform player;
    public Slider hpbar;
    public float maxHp;
    public float currenthp;

    void Update()
    {
        transform.position = player.position + new Vector3(0, 0, 0);
        hpbar.value = currenthp / maxHp;
    }
}
