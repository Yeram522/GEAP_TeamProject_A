using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    public Canvas QuitWindow;

    private void Awake()
    {
        if(QuitWindow.gameObject.activeSelf == true)
            QuitWindow.gameObject.SetActive(false);
    }

    void OnClickStartButton()
    {
        SceneManager.LoadScene("Tutorial");
    }

    void OnClickQuitButton()
    {
        QuitWindow.gameObject.SetActive(true);
    }

    void OnClickBackButton()
    {
        QuitWindow.gameObject.SetActive(false);
    }

    void OnClickShutDownButton()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit(); // 어플리케이션 종료
        #endif
    }
}
