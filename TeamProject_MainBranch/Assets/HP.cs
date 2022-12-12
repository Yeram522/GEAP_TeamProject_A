using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HP : MonoBehaviour
{
    private PlayerStatus status;

    [SerializeField]
    public Slider hpBar;

    private void Awake()
    {
        status = GameObject.Find("Player").GetComponent<PlayerStatus>();
    }

    void Update()
    {
        hpBar.value = status.getPlayerHP() / 100;
    }
}
