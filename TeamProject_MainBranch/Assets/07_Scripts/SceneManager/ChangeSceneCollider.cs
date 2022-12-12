using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneCollider : MonoBehaviour
{
    private GameSceneManager gameSceneManager;

    public string bossSceneName;

    public Vector3 StartingPointInNextScene = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        gameSceneManager = transform.parent.GetComponent<GameSceneManager>();

        GetComponent<Renderer>().enabled = false;
        GetComponent<Collider>().enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        GameObject player = GameObject.Find("Player");
        GameObject pet = GameObject.Find("Pet");

        pet.transform.position = player.transform.position;

        gameSceneManager.SendMessage("ActivateScene", gameObject.name);
    }
}
