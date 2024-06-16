using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainmenustartPanel;
    public GameObject gamemenuPanel;
    public GameObject settingsPanel;
    public GameObject startGamePanel;
    public GameObject exitGamePanel;
    public GameObject settingsbuttonPanel;
    public GameObject emptysettingsbuttonPanel;
    public GameObject eventSystem;
    public GameObject eventSystem1;
    public GameObject eventSystem2;
    public GameObject emptystartPanel;

    public void OpenMenu()
    {
        mainmenustartPanel.SetActive(false);
        gamemenuPanel.SetActive(true);
        eventSystem.SetActive(false);
        eventSystem1.SetActive(true);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
        emptysettingsbuttonPanel.SetActive(true);
        startGamePanel.SetActive(false);
        exitGamePanel.SetActive(false);
        settingsbuttonPanel.SetActive(false);
        emptystartPanel.SetActive(false);
        eventSystem1.SetActive(false);
        eventSystem2.SetActive(true);
        
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
        emptysettingsbuttonPanel.SetActive(false);
        startGamePanel.SetActive(true);
        exitGamePanel.SetActive(true);
        settingsbuttonPanel.SetActive(true);
        emptystartPanel.SetActive(true);
        eventSystem2.SetActive(false);
        eventSystem1.SetActive(true);
    }

    public void ExitGame()
    {
        Debug.Log("Quitting");

        Application.Quit();
    }
}