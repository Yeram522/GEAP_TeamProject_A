using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillMove : MonoBehaviour
{
    private float time = 0.0f;
    [SerializeField]
    int speed = 10;
    GameObject player;

    Vector3 vec;
    private void Awake()
    {
        player = GameObject.Find("Player");

        vec = player.GetComponent<PlayerController>().moveVec;
        if (vec == new Vector3(0, 0, 0))
            vec = Vector3.forward;

        transform.position += new Vector3(0, 1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        // 스킬 지속시간 설정
        if (time > 5.0f)
        {
            Debug.Log("Destroy rangeSkill");
            Destroy(this.gameObject);
        }
        else
            time += Time.deltaTime;

        // 스킬을 플레이어가 사용시에 바라본 방향으로 날아간다.
        transform.position += speed * vec * Time.deltaTime;
    }
}
