using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP_bar : MonoBehaviour
{
    [SerializeField]
    private Player_HJ player;

    [SerializeField]
    public Slider hpBar;

    private void Start()
    {
    }

    void Update()
    {
        hpBar.value = player.HP/player.MaxHp;    
    }
}


//https://b1ackhand.tistory.com/5 참고