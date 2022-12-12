using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene("SecondMap");
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR  
            UnityEditor.EditorApplication.isPlaying=false;  
        #else  
            Application.Quit();
        #endif  
    }

    public void MoveToMainMenu()
    {
        SceneManager.LoadScene("TitleScene");
    }

    public void subMenu(GameObject go)
    {
        go.SetActive(true);
    }
    
    public void BackButton(GameObject go)
    {
        go.SetActive(false);
    }
}
