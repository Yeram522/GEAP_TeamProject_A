using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public GameObject[] gameObjectsForMovingToScene;
    
    private Dictionary<string, ChangeSceneCollider> changeSceneColliders;

    public Transform playerTransform;
    
    // Start is called before the first frame update
    void Start()
    {
        changeSceneColliders = new Dictionary<string, ChangeSceneCollider>();
        foreach (ChangeSceneCollider sceneCollider in GetComponentsInChildren<ChangeSceneCollider>())
            changeSceneColliders.Add(sceneCollider.gameObject.name, sceneCollider);
        
        playerTransform = GameObject.FindWithTag("Player").transform;

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P)) clearBossStage("Stage1Boss");
        
    }

    void ActivateScene(string goName)
    {
        ChangeSceneCollider colliderInfo = changeSceneColliders[goName];
        string nextSceneName = colliderInfo.bossSceneName;
        Scene nextScene = SceneManager.GetSceneByName(nextSceneName);
        
        colliderInfo.gameObject.SetActive(false);

        if (colliderInfo.StartingPointInNextScene != Vector3.zero)
            playerTransform.position = colliderInfo.StartingPointInNextScene;

        foreach (GameObject go in gameObjectsForMovingToScene)
        {
            DontDestroyOnLoad(go);
        }

        SceneManager.LoadScene(nextSceneName);
    }

    void clearBossStage(string currentSceneName)
    {
        SceneManager.LoadScene("SecondMap");
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "SecondMap") return;

        foreach (var script in changeSceneColliders.Values)
        {
            if (script.gameObject.activeSelf && script.bossSceneName != "FinalBoss")
                return;
        }
        
        GameObject.Find("SecretDoor").SetActive(false);
    }
}

