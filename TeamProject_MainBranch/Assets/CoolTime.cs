using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoolTime : MonoBehaviour
{
    public Image img_Skill;
    public Image skillFilter;

    private void Start()
    {
        skillFilter.fillAmount = 0;
    }

    void FixedUpdate()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(Cool_Time(3f));
        }
    }

    IEnumerator Cool_Time(float cool)
    {
        {
            while (cool > 1.0f)
            {
                cool -= Time.deltaTime;
                img_Skill.fillAmount = (1.0f / cool);

                yield return new WaitForFixedUpdate();
            }
        }
    }
}
