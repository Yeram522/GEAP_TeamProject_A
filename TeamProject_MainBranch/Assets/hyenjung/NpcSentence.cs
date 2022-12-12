using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class NpcSentence : MonoBehaviour
{
    public string[] sentences;
    public UnityEvent onPlayerEntered;

    public bool Is_D_Finish;
    public bool Quest_Clear;

    public bool a;



    Player player;



    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            onPlayerEntered.Invoke();
            if (DialogeManager.instance.dialoguegroup.alpha == 0)
            {
                DialogeManager.instance.Ondialogue(sentences);
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        Is_D_Finish = true;
        Quest_Clear = false;
        a = true;


    }

    // Update is called once per frame



    void Update()
    {
        Text();

        if (a ==false)
        {
            System.Array.Resize(ref sentences, 2);
            sentences[0] = "������ ���� �غ�� �����Ͱ��ƿ�! �� ������ �����ּ���!";
            sentences[1] = "��..������� �����ϱ� ��..�ϴ� �˰ڽ��ϴ�...!!!";
            Invoke("Load_scene", 5f);
        }
    }
    void Text()
    {

        if (GameObject.Find("Player").GetComponent<Player>().firstQ == false && Quest_Clear == false&&a)
        {
            Is_D_Finish = false;
            Time.timeScale = 0;
            sentences[0] = "NPC \n���Կ���!���� �����ֽ÷� �°ǰ���?";
            sentences[1] = "��...��...?";
            sentences[2] = "NPC \n�����̴�!! �����ؿ�!!";
            sentences[3] = "NPC \n�⺻�����̶� �� �׷��� �� �� �ƽ���?";
            sentences[4] = "��... ��....?";
            sentences[5] = "NPC \n�ƴ� ��~ �װ� ���ݾƿ�~";
            sentences[6] = "NPC \nWASDŰ�� �̵��ϰ�! LShiftŰ�� �޸���! �����̽��ٷ� �����ϰ�!";
            sentences[7] = "NPC \n�ϴ� �غ���!";
            Is_D_Finish = true;
            Time.timeScale = 1;
        }

        else if (GameObject.Find("Player").GetComponent<Player>().firstQ == true && Quest_Clear == false && a)
        {
            Is_D_Finish = false;
            if (GameObject.Find("Player").GetComponent<Player>().senendQ == false && Quest_Clear == false && a)
            {

                sentences[0] = "NPC\n���Ͻó׿�!! ���� �츮 ������ ���� ����!";
                sentences[1] = "��..? �����̴°� ������ ���ݾƿ�..";
                sentences[2] = "NPC\n�׷� �����ϰ� �׷��͵� �˰ڳ׿�!";
                sentences[3] = "NPC\n�⺻�����̶� �� �׷��� �� �� �ƽ���?";
                sentences[4] = "��....?";
                sentences[5] = "NPC\n���� �� �𸣴� ô �Ͻó�!";
                sentences[6] = "NPC\n���콺 ��Ŭ������ ����! ���콺 ��Ŭ������ ���! EŰ ������ ȸ��!";
                sentences[7] = "NPC\n�� �� �� ����?";
                Is_D_Finish = true;

            }
            else if (GameObject.Find("Player").GetComponent<Player>().senendQ == true && Quest_Clear == false &&a)
            {
                System.Array.Resize(ref sentences, 4);
                sentences[0] = "NPC\n�ο� �غ�� ���� �� ���ƿ�! ���� ������ �����ּ���!!";
                sentences[1] = "��..? ��..����";
                sentences[2] = "NPC\n�� ������ ������ ������ ���Ե� ���ؿ�!! ���!!\n������ ����ִ����� �𸣰����� ���� ������ �ִ� ���͵��� ������ �ܼ��� �� �� �����ſ���!\n��Ź�ؿ�!!";
                sentences[3] = "��..������� �����ϱ� ��..�ϴ� �˰ڽ��ϴ�...!!!";
                Is_D_Finish = true;
                a = false;
            }

        }
    }

    void Load_scene()
    {
        SceneManager.LoadScene("SecondMap");
    }
}



