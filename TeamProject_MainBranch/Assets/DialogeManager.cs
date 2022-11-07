using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DialogeManager : MonoBehaviour, IPointerDownHandler
{
    public Text dialogueText;
    public GameObject nextText;
    public CanvasGroup dialoguegroup;
    public Queue<string> sentences;

    private string currentSentence;

    public float typingSpeed = 0.1f;

    private bool istyping;

    public static DialogeManager instance;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void Ondialogue(string[] lines)
    {
        sentences.Clear();
        foreach(string line in lines)
        {
            sentences.Enqueue(line);
        }
        dialoguegroup.alpha = 1;
        dialoguegroup.blocksRaycasts = true;

        NextSentence();
    }

    public void NextSentence()
    {
        if(sentences.Count!=0)
        {
            currentSentence = sentences.Dequeue();
            istyping = true;
            nextText.SetActive(false);
            StartCoroutine(Typing(currentSentence));
        }

        else
        {
            dialoguegroup.alpha = 0;
            dialoguegroup.blocksRaycasts = false;
        }
    }

    IEnumerator Typing(string line)
    {
        dialogueText.text = "";
        foreach(char letter in line.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);


        }
    }

    // Update is called once per frame
    void Update()
    {
        if(dialogueText.text.Equals(currentSentence))
        {
            nextText.SetActive(true);
            istyping = false;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!istyping)
        {
            NextSentence();
        }
    }
}
