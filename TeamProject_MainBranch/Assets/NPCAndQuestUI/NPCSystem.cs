using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCSystem : MonoBehaviour
{
    //For UI Visualizing
    public GameObject UI_Dialog;
    public GameObject MainCam;
    public GameObject NPC_Cam;

    [System.Serializable]
    public class QuestInfo //외부에서 입력 가능
    {
        //Quest Progress text
        public string contents;
        public string questDialog;//퀘스트 주고 나서의 대사
        public string defaultDialog;//퀘스트 주고 나서의 대사
        public string ClearDialog;//퀘스트 주고 나서의 대사
        public int totalCnt;
        public int completeCnt;
    }

    public string name;
    public List<QuestInfo> m_QuestInfos;//퀘스트 정보.

    private int m_QuestCount;//총 퀘스트
    private bool DialogActive;
    private int m_DoneQuestCount;//완료한 퀘스트 숫자

    private GameObject UI_MANAGER;

    private bool isProgressingQuest;
    private bool PlayerLock;
    private Vector3 lastPlayerPos;
    //FirstQusetParam

    //SecondQuestParam

    //ThirdQuestParam
    void Start()
    {
        DialogActive = false;
        m_DoneQuestCount = 0;
        isProgressingQuest = false;
        PlayerLock = false;
        m_QuestCount = m_QuestInfos.Count;
        UI_MANAGER = GameObject.FindGameObjectWithTag("UIManager");
        MainCam = GameObject.FindGameObjectWithTag("MainCamera");

    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerLock)
        {
            //Player Lock
            GameObject.FindGameObjectWithTag("Player").transform.position
                = this.transform.position + Vector3.forward * 5.0f;
        }
        if (DialogActive && Input.GetKeyDown(KeyCode.E))
        {
            //SetActive Dialog Window
            Debug.Log("ActiveDialog");
            MainCam.SetActive(false);
            NPC_Cam.SetActive(true);
            SetDialog();
            DialogActive = false;

            PlayerLock = true;
            lastPlayerPos = GameObject.FindGameObjectWithTag("Player").transform.position;


        }
        else if (UI_Dialog.activeSelf && Input.GetKeyDown(KeyCode.E))
        {
            UI_Dialog.SetActive(false);
            NPC_Cam.SetActive(false);
            MainCam.SetActive(true);
            PlayerLock = false;
            GameObject.FindGameObjectWithTag("Player").transform.position = lastPlayerPos;
            if (!isProgressingQuest) return;

            //퀘스트 완료시
            if (m_QuestInfos[m_DoneQuestCount].totalCnt == m_QuestInfos[m_DoneQuestCount].completeCnt)
                isProgressingQuest = false;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            DialogActive = true;
        }

    }

    private void SetDialog()
    {
        UI_Dialog.SetActive(true);
        if (isProgressingQuest)//퀘스트 중일 떄 말걸었을ㄸ(ㅒ
        {
            //퀘스트 완료X
            if (m_QuestInfos[m_DoneQuestCount].totalCnt != m_QuestInfos[m_DoneQuestCount].completeCnt)
            {
                UI_Dialog.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = m_QuestInfos[m_DoneQuestCount].defaultDialog;
            }
            else//퀘스트 완료0
            {
                UI_Dialog.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = m_QuestInfos[m_DoneQuestCount].ClearDialog;
                isProgressingQuest = false;
                QuestUIUpdate(false);
                m_DoneQuestCount++;
            }

            return;
        }


        isProgressingQuest = true;

        QuestInfo currentQuest = m_QuestInfos[m_DoneQuestCount];

        Text T_Name = UI_Dialog.transform.GetChild(0).GetChild(0).GetComponent<Text>();
        Text T_Dialog = UI_Dialog.transform.GetChild(1).GetChild(0).GetComponent<Text>();

        T_Name.text = name;
        T_Dialog.text = currentQuest.questDialog;

        Text T_Destin = UI_MANAGER.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>();
        Text T_PrgressCnt = UI_MANAGER.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<Text>();

        T_Destin.text = currentQuest.contents;
        T_PrgressCnt.text = "(" + currentQuest.completeCnt + "/" + currentQuest.totalCnt + ")";

    }

    //몬스터 처치시 불러와진다.
    public void NPCFirstQuestMessage()
    {
        if (m_DoneQuestCount != 0) return;


        //UI Update
        QuestUIUpdate(true);

    }


    private void QuestUIUpdate(bool _trigger)
    {
        Text T_Destin = UI_MANAGER.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>();
        Text T_PrgressCnt = UI_MANAGER.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<Text>();


        if (!_trigger)
        {
            //UI Update
            T_Destin.text = " ";
            T_PrgressCnt.text = " ";
            return;
        }
        QuestInfo currentQuest = m_QuestInfos[m_DoneQuestCount];

        //UI Update
        T_PrgressCnt.text = "(" + currentQuest.completeCnt + "/" + currentQuest.totalCnt + ")";
    }
}
