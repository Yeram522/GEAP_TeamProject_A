using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoUI : MonoBehaviour
{
    public GameObject gameObject;
  
    // Update is called once per frame
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
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
        }
    }
}
