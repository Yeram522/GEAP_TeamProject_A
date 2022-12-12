using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LAVAUI : MonoBehaviour
{
    Slider slHP;
    float fSliderBarTime;
    public float hpLAVA;
    public GameObject gameObject;
    void Start()
    {
        slHP = GetComponent<Slider>();
    }


    void Update()
    {
        if (slHP.value <= 0)
            transform.Find("Fill Area").gameObject.SetActive(false);
        else
            transform.Find("Fill Area").gameObject.SetActive(true);

        if (Input.GetKeyDown(KeyCode.Space))// 스페이스바 누를 때
        {
            slHP.value -= hpLAVA;
        }

        if (slHP.value <= 0.001)
        {
            GameObject.FindWithTag("UIManager").GetComponent<QuestManagerSystem>().SendMessage("NPCSecondtQuestMessage");
            Destroy(gameObject);
        }
    }
}
