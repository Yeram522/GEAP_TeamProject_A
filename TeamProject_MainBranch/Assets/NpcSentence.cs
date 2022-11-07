using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcSentence : MonoBehaviour
{
    public string[] sentences;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        { DialogeManager.instance.Ondialogue(sentences); }
    }
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Player").GetComponent<Player>().firstQ == false)
        {
            sentences[0] = "NPC\n���Կ���!���� �����ֽ÷� �°ǰ���?";
            sentences[1] = "��...��...?";
            sentences[2] = "NPC\n�����̴�!! �����ؿ�!!";
            sentences[3] = "NPC\n�⺻�����̶� �� �׷��� �� �� �ƽ���?";
            sentences[4] = "��... ��....?";
            sentences[5] = "NPC\n�ƴ� ��~ �װ� ���ݾƿ�~";
            sentences[6] = "NPC\nWASDŰ�� �̵��ϰ�! LShiftŰ�� �޸���! �����̽��ٷ� �����ϰ�!";
            sentences[7] = "NPC\n�ϴ� �غ���!";
        }

        else if (GameObject.Find("Player").GetComponent<Player>().firstQ == true)
        {
            if (GameObject.Find("Player").GetComponent<Player>().senendQ == false)
            {
                sentences[0] = "NPC\n���Ͻó׿�!! ���� �츮 ������ ���� ����!";
                sentences[1] = "��..? �����̴°� ������ ���ݾƿ�..";
                sentences[2] = "NPC\n�׷� �����ϰ� �׷��͵� �˰ڳ׿�!";
                sentences[3] = "NPC\n�⺻�����̶� �� �׷��� �� �� �ƽ���?";
                sentences[4] = "��....?";
                sentences[5] = "NPC\n���� �� �𸣴� ô �Ͻó�!";
                sentences[6] = "NPC\n���콺 ��Ŭ������ ����! ���콺 ��Ŭ������ ���! EŰ ������ ȸ��!";
                sentences[7] = "NPC\n�� �� �� ����?";
            }
            else if(GameObject.Find("Player").GetComponent<Player>().senendQ == true)
            {
                System.Array.Resize(ref sentences, 4);
                sentences[0] = "NPC\n�ο� �غ�� ���� �� ���ƿ�! ���� ������ �����ּ���!!";
                sentences[1] = "��..? ��..����";
                sentences[2] = "NPC\n�� ������ ������ ������ ���Ե� ���ؿ�!! ���!!\n������ ����ִ����� �𸣰����� ���� ������ �ִ� ���͵��� ������ �ܼ��� �� �� �����ſ���!\n��Ź�ؿ�!!";
                sentences[3] = "��..������� �����ϱ� ��..�ϴ� �˰ڽ��ϴ�...!!!";
            }
        }
    }
}
