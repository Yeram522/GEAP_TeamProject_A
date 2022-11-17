using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    float lifeTime = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (lifeTime > 10.0f)
            Destroy(this.gameObject);
        else
            lifeTime += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag.Equals("Player"))
        {
            other.gameObject.SendMessage("HealPlayer", 30);

            Debug.Log("Get Heal Potion");

            Destroy(this.gameObject);
        }
    }
}
