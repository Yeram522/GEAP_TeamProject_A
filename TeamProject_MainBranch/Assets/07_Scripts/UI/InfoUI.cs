using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoUI : MonoBehaviour
{
    public GameObject gameObject;
    int show = 0;
    // Update is called once per frame
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player" && show == 0)
        {
            Debug.Log("�浹 ó���� ����");
            gameObject.SetActive(true);
            Debug.Log("�� ��Ƽ�� ������");
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if(other.tag=="Player")
        {
            gameObject.SetActive(false);
            show = 1;
        }
    }
}
