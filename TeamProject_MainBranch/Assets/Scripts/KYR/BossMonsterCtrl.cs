using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class BossMonsterCtrl : MonoBehaviour
{
    public GameObject[] m_bossPref = new GameObject[4];

    public GameObject currentBoss;
    public Slider BossHP;
    public Text HPtxt;
    //Interaction UI
    public Slider m_SpaceBarSlider;

    private float[] targetHP = new float[3];
    void Start()
    {
        currentBoss = Instantiate(m_bossPref[0], transform.position, transform.rotation);
        currentBoss.GetComponent<BossMonster>().Bossmonsterctrl = this;
        BossHP.value = 1.0f;

        targetHP[0] = 0.7f;
        targetHP[1] = 0.5f;
        targetHP[2] = 0.3f;
    }

    void SpawnBoss(float BossHP)
    {
        int idx = ((int)currentBoss.GetComponent<BossMonster>().m_type);
        if (idx >= m_bossPref.Length-1) return;
        if (BossHP < targetHP[idx])
        {
            Vector3 cPos = currentBoss.transform.position;
            Quaternion cRot = currentBoss.transform.rotation;
            Destroy(currentBoss);
            idx++;
            currentBoss = Instantiate(m_bossPref[idx], cPos, cRot);
            currentBoss.GetComponent<BossMonster>().Bossmonsterctrl = this;
        }

    }
    // Update is called once per frame
    void Update()
    {

        SpawnBoss(BossHP.value);

        if (BossHP.value == 0.0f)
        {
            Destroy(currentBoss);
            Destroy(this.gameObject);

            SceneManager.LoadScene("SuccessScene");
        }
    }
}
