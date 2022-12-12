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



    Player_HJ  player;



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
        player = GameObject.Find("Player").GetComponent<Player_HJ>();
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
            sentences[0] = "마을을 구할 준비는 끝난것같아요! 얼른 마을을 구해주세요!";
            sentences[1] = "ㅁ..무서운건 싫으니까 이..일단 알겠습니다...!!!";
            player.GetComponent<Player_HJ>().enabled = false;

        }
    }
    void Text()
    {

        if (GameObject.Find("Player").GetComponent<Player_HJ>().firstQ == false && Quest_Clear == false&&a)
        {
            Is_D_Finish = false;
            Time.timeScale = 0;
            sentences[0] = "NPC \n용사님용사님!저희를 구해주시러 온건가요?";
            sentences[1] = "어...네...?";
            sentences[2] = "NPC \n다행이다!! 감사해요!!";
            sentences[3] = "NPC \n기본공격이랑 뭐 그런거 할 줄 아시죠?";
            sentences[4] = "어... 네....?";
            sentences[5] = "NPC \n아니 왜~ 그거 있잖아요~";
            sentences[6] = "NPC \nWASD키로 이동하고! LShift키로 달리고!";
            sentences[7] = "NPC \n일단 해봐요!";
            Is_D_Finish = true;
            Time.timeScale = 1;
        }

        else if (GameObject.Find("Player").GetComponent<Player_HJ>().firstQ == true && Quest_Clear == false && a)
        {
            Is_D_Finish = false;
            if (GameObject.Find("Player").GetComponent<Player_HJ>().senendQ == false && Quest_Clear == false && a)
            {

                sentences[0] = "NPC\n잘하시네요!! 역시 우리 마을을 구할 용사님!";
                sentences[1] = "네..? 움직이는건 누구나 하잖아요..";
                sentences[2] = "NPC\n그럼 공격하고 그런것도 알겠네요!";
                sentences[3] = "NPC\n기본공격이랑 뭐 그런거 할 줄 아시죠?";
                sentences[4] = "네....?";
                sentences[5] = "NPC\n아이 또 모르는 척 하시네!";
                sentences[6] = "NPC\n마우스 좌클릭으로 공격! 마우스 우클릭으로 방어! E키 눌러서 회피!";
                sentences[7] = "NPC\n잘 할 수 있죠?";
                Is_D_Finish = true;

            }
            else if (GameObject.Find("Player").GetComponent<Player_HJ>().senendQ == true && Quest_Clear == false &&a)
            {
                System.Array.Resize(ref sentences, 4);
                sentences[0] = "NPC\n싸울 준비는 끝난 것 같아요! 이제 마을을 구해주세요!!";
                sentences[1] = "네..? ㅈ..저는";
                sentences[2] = "NPC\n이 마을을 구하지 않으면 용사님도 당해요!! 어서요!!\n보스가 어디있는지는 모르겠지만 지역 곳곳에 있는 몬스터들을 잡으면 단서를 알 수 있을거에요!\n부탁해요!!";
                sentences[3] = "ㅁ..무서운건 싫으니까 이..일단 알겠습니다...!!!";
                Is_D_Finish = true;
                a = false;
            }

        }
    }

}



