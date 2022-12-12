using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManagerSystem : MonoBehaviour
{

    public GameObject QuestBoardPrefab;

    [SerializeField]
    private GameObject QuestListContents;

    public List<QuestInfo> ActiveQuestsList;
    void Start()
    {
        QuestListContents = this.transform.GetChild(0).GetChild(1).GetChild(0).GetChild(0).gameObject;
        ActiveQuestsList = new List<QuestInfo>(); //Initialize
    }

    public void AddQuest(QuestInfo questinfo)
    {
        this.ActiveQuestsList.Add(questinfo);
        questinfo.gameObject = Instantiate(QuestBoardPrefab, QuestBoardPrefab.transform.position, QuestBoardPrefab.transform.rotation);
        questinfo.gameObject.transform.SetParent(QuestListContents.transform);
        Debug.Log("AddQuest");
    }

    public void RemoveQuest(QuestInfo questinfo)
    {
        Destroy(questinfo.gameObject); 
        this.ActiveQuestsList.Remove(questinfo);
    }
    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i <ActiveQuestsList.Count;i++)
        {
            Text T_Destin = ActiveQuestsList[i].gameObject.transform.GetChild(0).GetComponent<Text>();
            Text T_PrgressCnt = ActiveQuestsList[i].gameObject.transform.GetChild(1).GetComponent<Text>();

            T_Destin.text = ActiveQuestsList[i].contents;
            T_PrgressCnt.text = "(" + ActiveQuestsList[i].completeCnt + "/" + ActiveQuestsList[i].totalCnt + ")";
        }
    }

    //몬스터 처치시 불러와진다.
    public void NPCFirstQuestMessage()
    {
        for (int i = 0; i < ActiveQuestsList.Count; i++)
        {
            //if (ActiveQuestsList[i].targetNPC.name != "NPC1") continue;
            if (ActiveQuestsList[i].questType != 0) continue;

            ActiveQuestsList[i].completeCnt++;

            Text T_PrgressCnt = ActiveQuestsList[i].gameObject.transform.GetChild(1).GetComponent<Text>();

            T_PrgressCnt.text = "(" + ActiveQuestsList[i].completeCnt + "/" + ActiveQuestsList[i].totalCnt + ")";
        }
        
    }

    public void NPCSecondtQuestMessage()
    {
        for (int i = 0; i < ActiveQuestsList.Count; i++)
        {
           // if (ActiveQuestsList[i].targetNPC.name != "NPC1") continue;
            if (ActiveQuestsList[i].questType != 1) continue;
            if(ActiveQuestsList[i].completeCnt>=1) continue;
            ActiveQuestsList[i].completeCnt++;

            Text T_PrgressCnt = ActiveQuestsList[i].gameObject.transform.GetChild(1).GetComponent<Text>();

            T_PrgressCnt.text = "(" + ActiveQuestsList[i].completeCnt + "/" + ActiveQuestsList[i].totalCnt + ")";
        }

    }
}
