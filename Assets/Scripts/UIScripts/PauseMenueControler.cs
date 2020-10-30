using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class PauseMenueControler : MonoBehaviour
{
    static bool gameIspaused = false;
    public GameObject PausedMenuScren;
    public GameObject MainPanel;
    public GameObject SettingsPanel;
    public GameObject QuitPanel;

    public AudioMixerGroup Mixer;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIspaused)
            { 
                Resume();
            }else
                Paused();
        }
    }   

    public void Paused()
    {
        PausedMenuScren.SetActive(true);
        MainPanel.SetActive(true);
        SettingsPanel.SetActive(false);
        QuitPanel.SetActive(false);
        Time.timeScale = 0f;
        gameIspaused = true;
    }

    public void Resume()
    {
        PausedMenuScren.SetActive(false);
        Time.timeScale = 1f;
        gameIspaused = false;
    }

    public void ShowQuitPanel()
    {
        MainPanel.SetActive(false);
        QuitPanel.SetActive(true);
    }

    public void ShowSettingsPanel()
    {
        MainPanel.SetActive(false);
        SettingsPanel.SetActive(true);
    }

    public void ShowMainPanel()
    {
        MainPanel.SetActive(true);
        SettingsPanel.SetActive(false);
        QuitPanel.SetActive(false);
    }

    
    public void ExitToMainMenue()
    {
        gameIspaused = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void ExitGame()
    {
        Time.timeScale = 1f;
        gameIspaused = false;
        Application.Quit();
    }

    public void ChangeVolume(float volume)
    {
        Mixer.audioMixer.SetFloat("Volume", Mathf.Lerp(-80, 0, volume));
    }

    public void ChangeMuiscVolume(float volume)
    {
        Mixer.audioMixer.SetFloat("MuiscVolume", Mathf.Lerp(-80, 0, volume));
    }
}
