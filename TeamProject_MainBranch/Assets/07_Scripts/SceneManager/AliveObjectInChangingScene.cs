using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AliveObjectInChangingScene : MonoBehaviour
{
    void Awake()
    {
        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
        if (allObjects.Count(obj => obj.name == this.name) >= 2)
        {
            this.name = "objectWillDestroy";
            this.transform.position = Vector3.zero;
            Destroy(this.gameObject, 0.0f);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
